// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace GraphZen.Infrastructure
{
    public interface INullableTestClass
    {
        string? NullableReferenceTypeProperty { get; }
        int? NullableValueTypeProperty { get; }
        string NonNullableReferenceTypeProperty { get; }
        int NonNullableValueTypeProperty { get; }
        string? NullableReferenceTypeMethod();
        int? NullableValueTypeMethod();
        int NonNullableValueTypeMethod();
        string? NullableReferenceTypeMethodWithNonNullableParameter(string nonNullable);
        string NonNullableReferenceTypeMethodWithNonNullableParameter(string nonNullable);
        string? NullableReferenceTypeMethodWithNullableParameter(string? nullable);
        string? NullableReferenceTypeMethodWithNullableAndNonNullableParameters(string? nullable, string nonNullable);
        string NonNullableReferenceTypeMethodWithNullableParameter(string? nullable);
        string NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters(string? nullable, string nonNullable);
        string NonNullableReferenceTypeMethod();
    }

    public static class TestHelpers
    {
        public static IDictionary<string, object> ToDictionary(dynamic value)
        {
            var dict = value != null
                ? JObjectExtensions.ToDictionary(JObject.FromObject(value, Json.Serializer))
                : new Dictionary<string, object>();
            return dict;
        }
    }
}