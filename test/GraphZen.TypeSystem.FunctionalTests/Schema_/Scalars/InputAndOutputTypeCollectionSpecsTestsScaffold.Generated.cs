// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars
{
    [NoReorder]
    public abstract class InputAndOutputTypeCollectionSpecsTests
    {
        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_output_type_identity))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_renamed_if_name_matches_output_type_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_renamed_to_name_with_input_type_identity))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_renamed_to_name_with_input_type_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_output_type_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_added_if_name_matches_input_type_identity))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_if_name_matches_input_type_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_renamed_to_name_with_output_type_identity))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_renamed_to_name_with_output_type_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs.named_item_can_be_added_if_name_matches_output_type_identity))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_if_name_matches_output_type_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity))]
        [Fact(Skip = "TODO")]
        public void
            clr_typed_item_with_name_attribute_can_be_added_if_name_attribute_matches_with_input_type_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(InputAndOutputTypeCollectionSpecs
            .clr_typed_item_can_be_renamed_if_name_matches_input_type_identity))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_renamed_if_name_matches_input_type_identity_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move InputAndOutputTypeCollectionSpecsTests into a separate file to start writing tests
    [NoReorder]
    public class InputAndOutputTypeCollectionSpecsTestsScaffold
    {
    }
}
// Source Hash Code: 4184696932203168915