#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
// ReSharper disable All
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Enums.EnumType {
[NoReorder]
public abstract  class SdlExtensionTests {


// SpecId: item_can_be_defined_by_sdl_extension
// isTestImplemented: False
// subject.Path: Schema_.Enums.EnumType
[Spec(nameof(SdlExtensionSpec.item_can_be_defined_by_sdl_extension))]
[Fact(Skip="TODO")]
public void item_can_be_defined_by_sdl_extension_() {
    // var schema = Schema.Create(_ => { });
}


}
// Move SdlExtensionTests into a separate file to start writing tests
[NoReorder] 
public  class SdlExtensionTestsScaffold {
}
}
// Source Hash Code: 10554311763202371398