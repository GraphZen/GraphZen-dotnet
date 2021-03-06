// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects.Fields.Arguments
{
    // ReSharper disable once InconsistentNaming
    public class Object_Field_Arguments_ViaClrMethodParameters : Object_Field_Arguments,
        ICollectionConventionConfigurationFixture
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);

        [GraphQLName(Grandparent)]
        public class SomeObject
        {
            public string SomeField(
                string arg1,
                [GraphQLIgnore] string ignoreMe,
                [GraphQLName(DataAnnotationName)] string arg2) =>
                throw new NotImplementedException();
        }

        public CollectionConventionContext GetContext() =>
            new CollectionConventionContext
            {
                ParentName = nameof(SomeObject.SomeField).FirstCharToLower(),
                ItemIgnoredByDataAnnotation = "ignoreMe",
                ItemNamedByConvention = "arg1",
                ItemNamedByDataAnnotation = DataAnnotationName,
                ItemIgnoredByConvention = "na"
            };

        public void ConfigureContextConventionally(SchemaBuilder sb)
        {
            sb.Object<SomeObject>();
        }

        public void ConfigureClrContext(SchemaBuilder sb, string parentName)
        {
            sb.Object<SomeObject>();
        }
    }
}