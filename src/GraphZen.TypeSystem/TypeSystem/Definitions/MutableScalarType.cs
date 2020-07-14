// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MutableScalarType : MutableNamedTypeDefinition, IMutableScalarType
    {
        public MutableScalarType(TypeIdentity identity, MutableSchema schema,
            ConfigurationSource configurationSource)
            : base(identity, schema, configurationSource)
        {
            InternalBuilder = new InternalScalarTypeBuilder(this);
            Builder = new ScalarTypeBuilder(InternalBuilder);
        }

        public override IEnumerable<IMember> Children()
        {
            yield break;
        }

        private string DebuggerDisplay => $"scalar {Name}";


        internal new InternalScalarTypeBuilder InternalBuilder { get; }
        public new ScalarTypeBuilder Builder { get; }
        protected override INamedTypeDefinitionBuilder GetBuilder() => Builder;

        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;

        public LeafSerializer<object>? Serializer { get; set; }

        public bool SetSerializer(LeafSerializer<object>? serializer, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public ConfigurationSource? GetSerializerConfigurationSource() => throw new NotImplementedException();

        public LeafLiteralParser<object, ValueSyntax>? LiteralParser { get; set; }
        public ConfigurationSource? GetLiteralParserConfigurationSource() => throw new NotImplementedException();

        public bool SetLiteralParser(LeafLiteralParser<object, ValueSyntax>? literalParser,
            ConfigurationSource configurationSource) => throw new NotImplementedException();

        public LeafValueParser<object>? ValueParser { get; set; }
        public ConfigurationSource? GetValueParserConfigurationSource() => throw new NotImplementedException();

        public bool SetValueParser(LeafValueParser<object>? valueParser, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.Scalar;
        public override TypeKind Kind { get; } = TypeKind.Scalar;
    }
}