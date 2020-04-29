#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects.ObjectType.Description {
[NoReorder]
public abstract  class ObjectTypeDescriptionTests {

[Spec(nameof(UpdateableSpecs.updateable_item_can_be_updated))]
[Fact]
public void it_can_be_updated() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(OptionalSpecs.optional_item_can_be_removed))]
[Fact]
public void optional_item_can_be_removed() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(OptionalSpecs.parent_can_be_created_without_optional_item))]
[Fact]
public void parent_can_be_created_without() {
    // Priority: High
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move ObjectTypeDescriptionTests into a separate file to start writing tests
[NoReorder] 
public  class ObjectTypeDescriptionTestsScaffold {
}
}
