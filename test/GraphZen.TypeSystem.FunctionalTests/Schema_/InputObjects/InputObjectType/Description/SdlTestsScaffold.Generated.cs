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
namespace GraphZen.TypeSystem.FunctionalTests.Schema_.InputObjects.InputObjectType.Description {
[NoReorder]
public abstract  class SdlTests {


// SpecId: item_can_be_defined_by_sdl
// isTestImplemented: False
// subject.Path: Schema_.InputObjects.InputObjectType.Description
[Spec(nameof(SdlSpec.item_can_be_defined_by_sdl))]
[Fact(Skip="TODO")]
public void item_can_be_defined_by_sdl_() {
    // var schema = Schema.Create(_ => { });
}


}
// Move SdlTests into a separate file to start writing tests
[NoReorder] 
public  class SdlTestsScaffold {
}
}
// Source Hash Code: 9238633794266606344