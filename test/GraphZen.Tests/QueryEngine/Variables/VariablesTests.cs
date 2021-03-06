// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using GraphZen.QueryEngine;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

#nullable disable


// ReSharper disable UnusedMember.Local
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace GraphZen.Tests.QueryEngine.Variables
{
    public abstract class VariablesTests
    {
        private static EnumType TestEnum { get; }
        private static ScalarType TestComplexScalar { get; }
        private static InputObjectType TestInputObject { get; }
        private static InputObjectType TestNestedInputObject { get; }
        private static ObjectType TestType { get; }
        protected static Schema StaticDslSchema { get; }

        public abstract Schema Schema { get; }

        public static Schema SchemaBuilderSchema => GraphQLContext.Schema;


        public static VariablesTestsGraphQLContext GraphQLContext => new VariablesTestsGraphQLContext();

        public static object[] Array(params object[] values) => values;


        protected Task<ExecutionResult> ExecuteAsync(string gql, dynamic variableValues = null)
        {
            var varValues = variableValues != null
                ? TestHelpers.ToDictionary(JObject.FromObject(variableValues))
                : new Dictionary<string, object>();

            var doc = Parser.ParseDocument(gql);
            return ExecutionFunctions.ExecuteAsync(Schema, doc, null, null, varValues);
        }
    }
}