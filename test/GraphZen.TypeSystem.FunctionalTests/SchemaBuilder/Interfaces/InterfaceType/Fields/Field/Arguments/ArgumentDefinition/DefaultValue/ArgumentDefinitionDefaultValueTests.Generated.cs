#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Interfaces.InterfaceType.Fields.Field.Arguments.ArgumentDefinition.DefaultValue {
public partial class ArgumentDefinitionDefaultValueTests {

[Spec("parent_can_be_created_without")]
[Fact]
public void parent_can_be_created_without() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: parent_can_be_created_without
// Priority: Low




[Spec("it_can_be_removed")]
[Fact]
public void it_can_be_removed() {
    var schema = Schema.Create(_ => {

    });
}

// SpecId: it_can_be_removed
// Priority: Low



}
}
