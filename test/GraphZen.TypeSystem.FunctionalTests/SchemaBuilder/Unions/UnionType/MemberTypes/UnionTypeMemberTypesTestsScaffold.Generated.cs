#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Unions.UnionType.MemberTypes {
[NoReorder]
public  class UnionTypeMemberTypesTests {

// Priority: Low
// Subject Name: MemberTypes
[Spec(nameof(NamedTypeSetSpecs.set_item_can_be_added))]
[Fact(Skip = "generated")]
public void set_item_can_be_added() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: MemberTypes
[Spec(nameof(NamedTypeSetSpecs.set_item_can_be_removed))]
[Fact(Skip = "generated")]
public void set_item_can_be_removed() {
    var schema = Schema.Create(_ => {

    });
}



// Priority: Low
// Subject Name: MemberTypes
[Spec(nameof(NamedTypeSetSpecs.set_item_must_be_valid_name))]
[Fact(Skip = "generated")]
public void set_item_must_be_valid_name() {
    var schema = Schema.Create(_ => {

    });
}


}
// Move UnionTypeMemberTypesTests into a separate file to start writing tests
[NoReorder] 
public  class UnionTypeMemberTypesTestsScaffold {
}
}