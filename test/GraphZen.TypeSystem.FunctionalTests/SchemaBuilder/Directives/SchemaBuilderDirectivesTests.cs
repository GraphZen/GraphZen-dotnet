// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Directives
{
    [NoReorder]
    public class SchemaBuilderDirectivesTests {

        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_added))]
        [Fact(Skip = "generated")]
        public void named_item_can_be_added() {
            // Priority: High
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_removed))]
        [Fact(Skip = "generated")]
        public void named_item_can_be_removed() {
            // Priority: High
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_can_be_renamed))]
        [Fact(Skip = "generated")]
        public void named_item_can_be_renamed() {
            // Priority: High
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact(Skip = "generated")]
        public void named_item_cannot_be_renamed_if_name_already_exists() {
            // Priority: High
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }



        [Spec(nameof(TypeSystemSpecs.NamedCollectionSpecs.named_item_name_must_be_valid_name))]
        [Fact(Skip = "generated")]
        public void named_item_name_must_be_valid_name() {
            // Priority: High
            var schema = Schema.Create(_ => {

            });
            throw new NotImplementedException();
        }


    }
}