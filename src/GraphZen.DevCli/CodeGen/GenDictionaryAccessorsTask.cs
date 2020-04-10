// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    internal class GenDictionaryAccessorsTask : ReflectionCodeGenTask
    {
        public GenDictionaryAccessorsTask(PropertyInfo property,
            GenDictionaryAccessorsAttribute attribute) :
            base(property.DeclaringType ?? throw new NotImplementedException())
        {
            Property = property;
            Attribute = attribute;
        }

        public PropertyInfo Property { get; }
        public GenDictionaryAccessorsAttribute Attribute { get; }

        public static IEnumerable<GenDictionaryAccessorsTask> FromTypes(IReadOnlyList<Type> types)
        {
            foreach (var property in types.SelectMany(t => t.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)))
            {
                var genAccessors = property.GetCustomAttribute<GenDictionaryAccessorsAttribute>();
                if (genAccessors != null) yield return new GenDictionaryAccessorsTask(property, genAccessors);
            }
        }

        public override void Apply(StringBuilder csharp)
        {
            var propertyName = Property.Name;
            var keyName = Attribute.KeyName;
            var keyNameCamelized = Attribute.KeyName.FirstCharToLower();
            var keyType = Property.PropertyType.GetGenericArguments()[0].Name;
            var valueType = Property.PropertyType.GetGenericArguments()[1].Name;
            var valueName = Attribute.ValueName;
            var valueNameCamelized = valueName.FirstCharToLower();
            var valueTypeCamelized = valueType.FirstCharToLower();

            csharp.AppendLine($@"
        public {valueType}? Find{valueName}({keyType} {keyNameCamelized}) 
            => {propertyName}.TryGetValue(Check.NotNull({keyNameCamelized},nameof({keyNameCamelized})), out var {valueNameCamelized[0]}) ? {valueNameCamelized[0]} : null;

        public bool Has{valueName}({keyType} {keyNameCamelized}) 
            => {propertyName}.ContainsKey(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized})));
        
        public {valueType} Get{valueName}({keyType} {keyNameCamelized}) 
            => Find{valueName}(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized}))) ?? throw new Exception($""{{this}} does not contain a {{nameof({valueType})}} with {keyNameCamelized} '{{{keyNameCamelized}}}'."");


        public bool TryGet{valueName}({keyType} {keyNameCamelized}, [NotNullWhen(true)] out {valueType}? {valueTypeCamelized})
             => {propertyName}.TryGetValue(Check.NotNull({keyNameCamelized}, nameof({keyNameCamelized})), out {valueTypeCamelized});
");
        }
    }
}