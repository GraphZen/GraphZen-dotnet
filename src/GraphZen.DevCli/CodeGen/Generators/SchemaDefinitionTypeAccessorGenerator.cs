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
    public class SchemaDefinitionTypeAccessorGenerator : PartialTypeGenerator<SchemaDefinition>
    {
        public override void Apply(StringBuilder csharp)
        {
            foreach (var (kind, type) in TypeSystemCodeGen.NamedTypes)
            {
                csharp.Region($"{kind} type accessors", region =>
                {
                    region.AppendLine($@"
        public {type}Definition Get{kind}(string name) => GetType<{type}Definition>(name);

        public {type}Definition Get{kind}(Type clrType) =>
                GetType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));

        public {type}Definition Get{kind}<TClrType>() => GetType<{type}Definition>(typeof(TClrType));

        public {type}Definition? Find{kind}(string name) => FindType<{type}Definition>(name);

        public {type}Definition? Find{kind}<TClrType>() => FindType<{type}Definition>(typeof(TClrType));

        public {type}Definition? Find{kind}(Type clrType) => 
            FindType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));

        public bool TryGet{kind}(Type clrType, out {type}Definition type) =>
            TryGetType(Check.NotNull(clrType, nameof(clrType)), out type);

        public bool TryGet{kind}<TClrType>(out {type}Definition type) =>
            TryGetType(typeof(TClrType), out type);

        public bool TryGet{kind}(string name, out {type}Definition type) =>
            TryGetType(Check.NotNull(name, nameof(name)), out type);

        public bool Has{kind}(Type clrType) => HasType<{type}Definition>(Check.NotNull(clrType, nameof(clrType)));

        public bool Has{kind}<TClrType>() => HasType<{type}Definition>(typeof(TClrType));

        public bool Has{kind}(string name) => HasType<{type}Definition>(Check.NotNull(name, nameof(name)));
");
                });
            }
        }
    }
}