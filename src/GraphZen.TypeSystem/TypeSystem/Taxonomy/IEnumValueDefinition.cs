// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMember : IMemberDefinition
    {
    }


    [GraphQLIgnore]
    public interface IEnumValueDefinition : IAnnotatableDefinition, INamed, IDeprecation, IInputDefinition,
        IOutputDefinition
    {
        [CanBeNull]
        object Value { get; }

        [NotNull]
        [GraphQLIgnore]
        IEnumTypeDefinition DeclaringType { get; }
    }
}