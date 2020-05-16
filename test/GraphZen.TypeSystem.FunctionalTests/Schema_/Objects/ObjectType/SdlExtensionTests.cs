// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.SdlExtensionSpec;


namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Objects.ObjectType
{
    [NoReorder]
    public abstract class SdlExtensionTests
    {

        [Spec(nameof(item_can_be_defined_by_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend type Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }
    }
}