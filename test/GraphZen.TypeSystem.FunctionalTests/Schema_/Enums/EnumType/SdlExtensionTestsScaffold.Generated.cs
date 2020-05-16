// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

// ReSharper disable All
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.SdlExtensionSpec;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums.EnumType
{
    [NoReorder]
    public abstract class SdlExtensionTests
    {
        [Spec(nameof(item_can_be_defined_by_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void item_can_be_defined_by_sdl_extension_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

    // Move SdlExtensionTests into a separate file to start writing tests
    [NoReorder]
    public class SdlExtensionTestsScaffold
    {
    }
}
// Source Hash Code: 13349405417979613020