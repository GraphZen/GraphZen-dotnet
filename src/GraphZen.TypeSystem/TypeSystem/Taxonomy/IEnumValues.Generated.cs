// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public partial interface IEnumValues
    {
        public EnumValue? FindValue(string name)
            => Values.TryGetValue(Check.NotNull(name, nameof(name)), out var nameValue) ? nameValue : null;

        public bool HasValue(string name)
            => Values.ContainsKey(Check.NotNull(name, nameof(name)));


        public EnumValue GetValue(string name)
            => FindValue(Check.NotNull(name, nameof(name))) ??
               throw new Exception($"{this} does not contain a value named '{name}'.");


        public bool TryGetValue(string name, [NotNullWhen(true)] out EnumValue? enumValue)
            => Values.TryGetValue(Check.NotNull(name, nameof(name)), out enumValue);
    }
}