// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;

// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Scalars.ScalarType
{
    [NoReorder]
    public abstract class ScalarTypeSdlExtensionSpec
    {
        [Spec(nameof(SdlExtensionSpec.item_can_be_defined_by_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void item_can_be_defined_by_sdl_extension_()
        {
            // var schema = Schema.Create(_ => { });
        }
    }

// Move ScalarTypeSdlExtensionSpec into a separate file to start writing tests
    [NoReorder]
    public class ScalarTypeSdlExtensionSpecScaffold
    {
    }
}
// Source Hash Code: 4118891384487093073