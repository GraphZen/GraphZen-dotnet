#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.Fields.InputField.DefaultValue {
[NoReorder]
public abstract  class InputFieldDefaultValueTests {

[Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
[Fact]
public void optional_item_can_be_removed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(OptionalSpecs.parent_can_be_created_without))]
[Fact]
public void parent_can_be_created_without() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move InputFieldDefaultValueTests into a separate file to start writing tests
[NoReorder] 
public  class InputFieldDefaultValueTestsScaffold {
}
}
