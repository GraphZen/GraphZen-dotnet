// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.CodeGen.CodeGenFx;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen.Generators
{
    public class SchemaTypeAccessorGenerator : PartialTypeGenerator<Schema>
    {
        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, type) in TypeSystemCodeGen.NamedTypes)
            {
                csharp.Region($"{kind} type accessors", region =>
                {
                    region.AppendLine($@"
[GraphQLIgnore]        
public {type} Get{kind}(string name) => GetType<{type}>(name);

[GraphQLIgnore]        
        public {type} Get{kind}(Type clrType) => GetType<{type}>(Check.NotNull(clrType, nameof(clrType)));
        
[GraphQLIgnore]        
        public {type} Get{kind}<TClrType>() => GetType<{type}>(typeof(TClrType));

[GraphQLIgnore]        
        public {type}? Find{kind}(string name) => FindType<{type}>(name);

[GraphQLIgnore]        
        public {type}? Find{kind}<TClrType>() => FindType<{type}>(typeof(TClrType));

[GraphQLIgnore]        
        public {type}? Find{kind}(Type clrType) => FindType<{type}>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool TryGet{kind}(Type clrType, [NotNullWhen(true)] out {type}? type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

[GraphQLIgnore]        
        public bool TryGet{kind}<TClrType>([NotNullWhen(true)] out {type}? type) =>
            TryGetType(typeof(TClrType), out type);

[GraphQLIgnore]        
        public bool TryGet{kind}(string name, [NotNullWhen(true)] out {type}? type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

[GraphQLIgnore]        
        public bool Has{kind}(Type clrType) => HasType<{type}>(Check.NotNull(clrType, nameof(clrType)));

[GraphQLIgnore]        
        public bool Has{kind}<TClrType>() => HasType<{type}>(typeof(TClrType));

[GraphQLIgnore]        
        public bool Has{kind}(string name) => HasType<{type}>(Check.NotNull(name, nameof(name)));

");
                });
            }
        }
    }
}