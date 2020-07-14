// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public partial interface IMutableFields
    {
        #region DictionaryAccessorGenerator

        [GraphQLIgnore]
        public MutableField? FindField(string name)
            => FieldMap.TryGetValue(Check.NotNull(name, nameof(name)), out var field) ? field : null;

        [GraphQLIgnore]
        public bool HasField(string name)
            => FieldMap.ContainsKey(Check.NotNull(name, nameof(name)));

        [GraphQLIgnore]
        public MutableField GetField(string name)
            => FindField(Check.NotNull(name, nameof(name))) ??
               throw new ItemNotFoundException(
                   $"{this} does not contain a {nameof(MutableField)} with name '{name}'.");

        [GraphQLIgnore]
        public bool TryGetField(string name, [NotNullWhen(true)] out MutableField? fieldDefinition)
            => FieldMap.TryGetValue(Check.NotNull(name, nameof(name)), out fieldDefinition);

        #endregion
    }
}
// Source Hash Code: 6844039866164987830