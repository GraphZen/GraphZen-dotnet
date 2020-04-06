﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.TestSubjectAssemblyB
{
    public class NullableTestClassB : INullableTestClass
    {
        public NullableTestClassB(string? nullableReferenceTypeProperty, string nonNullableReferenceTypeProperty)
        {
            NullableReferenceTypeProperty = nullableReferenceTypeProperty;
            NonNullableReferenceTypeProperty = nonNullableReferenceTypeProperty;
        }

        public string? NullableReferenceTypeProperty { get; }
        public int? NullableValueTypeProperty { get; } = null;
        public string NonNullableReferenceTypeProperty { get; }
        public int NonNullableValueTypeProperty { get; } = 0;
        public string? NullableReferenceTypeMethod() => throw new NotImplementedException();
        public int? NullableValueTypeMethod() => throw new NotImplementedException();

        public int NonNullableValueTypeMethod() => throw new NotImplementedException();

        public string? NullableReferenceTypeMethodWithNonNullableParameter(string nonNullable) =>
            throw new NotImplementedException();

        public string NonNullableReferenceTypeMethodWithNonNullableParameter(string nonNullable) =>
            throw new NotImplementedException();

        public string? NullableReferenceTypeMethodWithNullableParameter(string? nullable) =>
            throw new NotImplementedException();

        public string? NullableReferenceTypeMethodWithNullableAndNonNullableParameters(string? nullable,
            string nonNullable) => throw new NotImplementedException();

        public string NonNullableReferenceTypeMethodWithNullableParameter(string? nullable) =>
            throw new NotImplementedException();

        public string NonNullableReferenceTypeMethodWithNullableAndNonNullableParameters(string? nullable,
            string nonNullable) => throw new NotImplementedException();

        public string NonNullableReferenceTypeMethod() => throw new NotImplementedException();
    }
}