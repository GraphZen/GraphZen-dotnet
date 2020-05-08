// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.Fields.InputField.DefaultValue
{
    [NoReorder]
    public abstract class DefaultValueTests
    {
        [Spec(nameof(SdlSpec.element_can_be_defined_via_sdl))]
        [Fact(Skip = "TODO")]
        public void element_can_be_defined_via_sdl_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(SdlSpec.element_can_be_defined_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void element_can_be_defined_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
        [Fact(Skip = "TODO")]
        public void optional_item_can_be_removed_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(OptionalSpecs.parent_can_be_created_without_optional_item))]
        [Fact(Skip = "TODO")]
        public void parent_can_be_created_without_optional_item_()
        {
            var schema = Schema.Create(_ => { });
        }
    }

// Move DefaultValueTests into a separate file to start writing tests
    [NoReorder]
    public class DefaultValueTestsScaffold
    {
    }
}
// Source Hash Code: 11810946080709534900