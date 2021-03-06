// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public static class PropertyInfoExtensions
    {
        public static string GetGraphQLTypeNameForProperty(PropertyInfo property)
        {
            Check.NotNull(property, nameof(property));

            var type = property.PropertyType;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
            }


            if (typeof(ICollection).IsAssignableFrom(type))
            {
            }

            throw new NotImplementedException();
        }

        public static bool CanBeNull(this PropertyInfo property) =>
            property.GetCustomAttribute<GraphQLCanBeNullAttribute>(true) != null;

        public static bool ItemCanBeNull(this PropertyInfo property) =>
            property.GetCustomAttribute<GraphQLListItemCanBeNullAttribute>(true) != null;

        public static bool CanBeNull(this ParameterInfo property) =>
            property.GetCustomAttribute<GraphQLCanBeNullAttribute>(true) != null;

        public static bool ItemCanBeNull(this ParameterInfo property) =>
            property.GetCustomAttribute<GraphQLListItemCanBeNullAttribute>(true) != null;

        public static bool CanBeNull(this MethodInfo property) =>
            property.GetCustomAttribute<GraphQLCanBeNullAttribute>(true) != null;

        public static bool ItemCanBeNull(this MethodInfo property) =>
            property.GetCustomAttribute<GraphQLListItemCanBeNullAttribute>(true) != null;
    }
}