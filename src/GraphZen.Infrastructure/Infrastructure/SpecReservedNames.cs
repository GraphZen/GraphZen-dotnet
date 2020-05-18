// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    internal static class SpecReservedNames
    {
        public static ImmutableHashSet<string> ScalarTypeNames { get; } =
            ImmutableHashSet.Create("String", "Int", "Float", "Boolean", "ID");


        public static ImmutableHashSet<string> DirectiveNames { get; } =
            ImmutableHashSet.Create("deprecated", "include", "skip");


        public static ImmutableHashSet<string> IntrospectionTypeNames { get; } =
            ImmutableHashSet.Create("__Type", "__Field", "__Schema", "__Directive", "__InputValue", "__EnumValue",
                "__DirectiveLocation",
                "__TypeKind");
    }
}