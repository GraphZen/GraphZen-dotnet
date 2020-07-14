// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [DisplayName("field")]
    public partial class MutableField : MutableAnnotatableMember, IMutableField
    {
        private string? _deprecationReason;
        private bool _isDeprecated;
        private ConfigurationSource _nameConfigurationSource;

        private readonly ArgumentsDefinition _args;

        public override MutableSchema Schema { get; }
        public IEnumerable<IMember> Children() => Arguments;

        public MutableField(string name, ConfigurationSource nameConfigurationSource,
            TypeIdentity fieldTypeIdentity,
            TypeSyntax fieldTypeSyntax,
            MutableSchema schema,
            MutableFields declaringType,
            ConfigurationSource configurationSource, MemberInfo? clrInfo) : base(configurationSource)
        {
            Schema = schema;
            ClrInfo = clrInfo;
            _nameConfigurationSource = nameConfigurationSource;
            DeclaringType = Check.NotNull(declaringType, nameof(declaringType));
            InternalBuilder = new InternalFieldBuilder(this);
            Name = name;
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptions.InvalidNameException.CannotCreateField(name, this));
            }

            TypeReference = new FieldTypeReference(fieldTypeIdentity, fieldTypeSyntax, this);
            _args = new ArgumentsDefinition(this);
        }


        private string DebuggerDisplay => $"field {Name}";


        internal new InternalFieldBuilder InternalBuilder { get; }
        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;


        public MutableFields DeclaringType { get; }

        public bool SetFieldType(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource) =>
            TypeReference.Update(identity, syntax, configurationSource);

        public bool SetFieldType(string type, ConfigurationSource configurationSource) =>
            TypeReference.Update(type, configurationSource);

        public MemberInfo? ClrInfo { get; }

        public bool RenameArgument(MutableArgument argument, string name, ConfigurationSource configurationSource) =>
            _args.RenameArgument(argument, name, configurationSource);


        public TypeReference FieldType => TypeReference;
        IGraphQLTypeReference IField.FieldType => FieldType;
        public Resolver<object, object?>? Resolver { get; set; }

        IFields IField.DeclaringType => DeclaringType;

        public string Name { get; private set; }

        public bool IsDeprecated
        {
            get => _isDeprecated || DeprecationReason != null;
            set
            {
                _isDeprecated = value;
                if (!_isDeprecated)
                {
                    DeprecationReason = null;
                }
            }
        }

        public string? DeprecationReason
        {
            get => _deprecationReason;
            set
            {
                _deprecationReason = value;
                if (_deprecationReason != null)
                {
                    IsDeprecated = true;
                }
            }
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.FieldDefinition;

        [GenDictionaryAccessors(nameof(MutableArgument.Name), "Argument")]
        public IReadOnlyDictionary<string, MutableArgument> ArgumentMap => _args.ArgumentMap;

        public IReadOnlyCollection<MutableArgument> Arguments => _args.Arguments;

        public MutableArgument?
            GetOrAddArgument(string name, Type clrType, ConfigurationSource configurationSource) =>
            _args.GetOrAddArgument(name, clrType, configurationSource);

        public MutableArgument?
            GetOrAddArgument(string name, string type, ConfigurationSource configurationSource) =>
            _args.GetOrAddArgument(name, type, configurationSource);

        IMutableFields IMutableField.DeclaringType => DeclaringType;

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
            if (!name.IsValidGraphQLName())
            {
                throw InvalidNameException.ForRename(this, name);
            }

            if (!configurationSource.Overrides(_nameConfigurationSource))
            {
                return false;
            }

            if (Name != name)
            {
                DeclaringType.RenameField(this, name, configurationSource);
            }

            Name = name;
            _nameConfigurationSource = configurationSource;
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;


        object? IClrInfo.ClrInfo => ClrInfo;

        public bool MarkAsDeprecated(string reason, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public bool RemoveDeprecation(ConfigurationSource configurationSource) => throw new NotImplementedException();

        public ConfigurationSource? FindIgnoredArgumentConfigurationSource(string name)
            => _args.FindIgnoredArgumentConfigurationSource(name);

        public bool IgnoreArgument(string name, ConfigurationSource configurationSource) =>
            _args.IgnoreArgument(name, configurationSource);


        public MutableArgument? FindArgument(ParameterInfo member)
        {
            var memberMatch = Arguments.SingleOrDefault(_ => _.ClrInfo == member);
            if (memberMatch != null)
            {
                return memberMatch;
            }

            var (argumentName, _) = member.GetGraphQLArgumentName();
            return FindArgument(argumentName);
        }

        public bool RemoveArgument(MutableArgument argument) => _args.RemoveArgument(argument);

        public void UnignoreArgument(string name) => _args.UnignoreArgument(name);


        public MutableArgument AddArgument(ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var (argName, nameConfigurationSource) = parameter.GetGraphQLArgumentName();

            if (!parameter.TryGetGraphQLTypeInfo(out var typeSyntax, out var innerClrType))
            {
                throw new Exception($"Unable to get type info from parameter {parameter}");
            }

            var typeIdentity = Schema.GetOrAddInputTypeIdentity(innerClrType);
            var argument = new MutableArgument(argName, nameConfigurationSource, typeIdentity, typeSyntax,
                configurationSource, this, parameter);
            var ab = argument.InternalBuilder;
            ab.DefaultValue(parameter, configurationSource);
            if (parameter.TryGetDescriptionFromDataAnnotation(out var description))
            {
                ab.Description(description, ConfigurationSource.DataAnnotation);
            }

            AddArgument(argument);
            return argument;
        }


        public bool AddArgument(MutableArgument argument) => _args.AddArgument(argument);

        public override string ToString() => $"{DeclaringType.GetTypeDisplayName()} field {DeclaringType.Name}.{Name}";

        public ConfigurationSource GetTypeReferenceConfigurationSource() =>
            TypeReference.GetTypeReferenceConfigurationSource();

        public TypeReference TypeReference { get; }

        public bool SetTypeReference(TypeIdentity identity, TypeSyntax syntax,
            ConfigurationSource configurationSource) =>
            TypeReference.Update(identity, syntax, configurationSource);

        public bool SetTypeReference(string type, ConfigurationSource configurationSource) =>
            TypeReference.Update(type, configurationSource);

        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => TypeReference;
        IReadOnlyCollection<IArgument> IArguments.Arguments => Arguments;
    }
}