// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface
        // ReSharper disable once PossibleInterfaceMemberAmbiguity
        IScalarTypeBuilder<TScalar, TValueNode> :
            IInfrastructure<InternalScalarTypeBuilder>,
            IInfrastructure<ScalarTypeDefinition>,
            IDescriptionBuilder<ScalarTypeBuilder<TScalar, TValueNode>>,
            IAnnotableBuilder<ScalarTypeBuilder<TScalar, TValueNode>>,
            INamedBuilder<ScalarTypeBuilder<TScalar, TValueNode>>,
            IClrTypeBuilder<ScalarTypeBuilder<object, TValueNode>>
        where TValueNode : ValueSyntax
        where TScalar : notnull
    {
        ScalarTypeBuilder<T, TValueNode> ClrType<T>(bool inferName = false) where T : notnull;
        ScalarTypeBuilder<T, TValueNode> ClrType<T>(string name) where T : notnull;
        ScalarTypeBuilder<TScalar, TValueNode> Serializer(LeafSerializer serializer);
        ScalarTypeBuilder<TScalar, TValueNode> LiteralParser(LeafLiteralParser<object, TValueNode> literalParser);
        ScalarTypeBuilder<TScalar, TValueNode> ValueParser(LeafValueParser<object> valueParser);
    }
}