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
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields {
[NoReorder]
public abstract  class FieldsTests {

[Spec(nameof(NamedCollectionSpecs.named_item_can_be_added))]
[Fact]
public void named_item_can_be_added() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl))]
[Fact]
public void named_item_can_be_added_via_sdl() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_can_be_added_via_sdl_extension))]
[Fact]
public void named_item_can_be_added_via_sdl_extension() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_can_be_removed))]
[Fact]
public void named_item_can_be_removed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_can_be_renamed))]
[Fact]
public void named_item_can_be_renamed() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_invalid_name))]
[Fact]
public void named_item_cannot_be_added_with_invalid_name() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_added_with_null_value))]
[Fact]
public void named_item_cannot_be_added_with_null_value() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_invalid_name))]
[Fact]
public void named_item_cannot_be_removed_with_invalid_name() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_removed_with_null_value))]
[Fact]
public void named_item_cannot_be_removed_with_null_value() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_if_name_already_exists))]
[Fact]
public void named_item_cannot_be_renamed_if_name_already_exists() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_an_invalid_name))]
[Fact]
public void named_item_cannot_be_renamed_with_an_invalid_name() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}



[Spec(nameof(NamedCollectionSpecs.named_item_cannot_be_renamed_with_null_value))]
[Fact]
public void named_item_cannot_be_renamed_with_null_value() {
    // Priority: Low
    var schema = Schema.Create(_ => {

    });
    throw new NotImplementedException();
}


}
// Move FieldsTests into a separate file to start writing tests
[NoReorder] 
public  class FieldsTestsScaffold {
}
}