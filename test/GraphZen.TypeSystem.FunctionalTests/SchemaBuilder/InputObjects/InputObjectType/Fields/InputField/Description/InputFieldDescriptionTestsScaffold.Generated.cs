#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects.InputObjectType.Fields.InputField.Description {
[NoReorder]
public  class InputFieldDescriptionTests {

// Priority: Low
// Subject Name: Description
[Spec(nameof(UpdateableSpecs.it_can_be_updated))]
[Fact(Skip = "generated")]
public void it_can_be_updated() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: Description
[Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
[Fact(Skip = "generated")]
public void optional_item_can_be_removed() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: Description
[Spec(nameof(OptionalSpecs.parent_can_be_created_without))]
[Fact(Skip = "generated")]
public void parent_can_be_created_without() {
    var schema = Schema.Create(_ => {

    });
}


}
// Move InputFieldDescriptionTests into a separate file to start writing tests
[NoReorder] 
public  class InputFieldDescriptionTestsScaffold {
}
}