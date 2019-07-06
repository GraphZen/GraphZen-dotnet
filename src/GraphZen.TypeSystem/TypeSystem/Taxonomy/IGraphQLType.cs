// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLName("__Type")]
    public interface IGraphQLType : IGraphQLTypeReference, ISyntaxConvertable
    {
        TypeKind Kind { get; }
    }
}