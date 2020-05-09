// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.InputObjects
{
    [NoReorder]
    public class InputObjectsTests
    {
        [Spec(nameof(named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create("input Foo");
            schema.HasInputObject("Foo").Should().BeTrue();
        }

        [Spec(nameof(named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "needs implementation")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create("extend input Foo");
            schema.HasInputObject("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo"); });
            schema.HasInputObject("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.InputObject((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {

            Schema.Create(_ =>
            {
                Action add = () => _.InputObject(name);
                add.Should().Throw<InvalidNameException>().WithMessage($"Cannot get or create GraphQL type builder for input object named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").Name("Bar"); });
            schema.HasInputObject("Foo").Should().BeFalse();
            schema.HasInputObject("Bar").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_renamed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_renamed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var foo = _.InputObject("Foo");
                Action rename = () => foo.Name(null!);
                rename.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("")]

        public void named_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {

            Schema.Create(_ =>
            {
                var foo = _.InputObject("Foo");
                Action rename = () => foo.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage($"Cannot rename input object Foo. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }

        [Spec(nameof(named_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void named_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo");
                var bar = _.InputObject("Bar");
                Action rename = () => bar.Name("Foo");
                rename.Should().Throw<DuplicateNameException>()
                    .WithMessage(
                        "Cannot rename input object Bar to \"Foo\", input object Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(named_item_can_be_removed))]
        [Fact(Skip = "needs implementation")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject("Foo");
                _.RemoveInputObject("Foo");
            });
            schema.HasInputObject("Foo").Should().BeFalse();
        }


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.InputObject((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_removed_with_invalid_name))]
        [Theory(Skip = "needs impl")]
        [InlineData("")]
        public void named_item_cant_be_removed_with_invalid_name_(string name)
        {

            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveInputObject(name);
                remove.Should().Throw<InvalidNameException>().WithMessage("x");
            });
        }


        private class Poco
        {
        }

        [GraphQLName(AnnotatedName)]
        private class PocoNameAnnotated
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName(InvalidName)]
        private class PocoInvalidNameAnnotation
        {
            public const string InvalidName = "abc @#$%^";
        }


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.InputObject(typeof(Poco)); });
            schema.HasInputObject<Poco>().Should().BeTrue();
        }


        

        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.InputObject((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.InputObject<PocoInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $@"Cannot get or create GraphQL input object type builder with CLR class 'PocoInvalidNameAnnotation'. The name ""abc @#$%^"" specified in the GraphQLNameAttribute on the PocoInvalidNameAnnotation CLR class is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact(Skip = "needs implementation")]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<Poco>();
                _.RemoveInputObject(typeof(Poco));
            });
            schema.HasInputObject<Poco>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact(Skip = "needs implementation")]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.InputObject<Poco>();
                _.RemoveInputObject<Poco>();
            });
            schema.HasInputObject<Poco>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveInputObject((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_changed))]
        [Fact]
        public void clr_typed_item_can_have_clr_type_changed_()
        {
            var schema = Schema.Create(_ => { _.InputObject<Poco>().ClrType<PocoNameAnnotated>(); });
            schema.HasInputObject<PocoNameAnnotated>().Should().BeTrue();
            schema.HasInputObject<Poco>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_have_clr_type_changed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_have_clr_type_changed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                var poco = _.InputObject<Poco>();
                Action remove = () => poco.ClrType(null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_can_have_clr_type_removed))]
        [Fact(Skip = "needs implementation")]
        public void clr_typed_item_can_have_clr_type_removed_()
        {
            var schema = Schema.Create(_ => { _.InputObject<Poco>().RemoveClrType(); });
            schema.HasInputObject<Poco>().Should().BeFalse();
            schema.GetInputObject(nameof(Poco)).ClrType.Should().BeNull();
        }


        [Spec(nameof(clr_typed_item_with_type_removed_should_retain_clr_type_name))]
        [Fact(Skip = "needs implementation")]
        public void clr_typed_item_with_type_removed_should_retain_clr_type_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject<Poco>().RemoveClrType(); });
            schema.HasInputObject(nameof(Poco)).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name))]
        [Fact(Skip = "needs implementation")]
        public void clr_typed_item_with_name_annotation_type_removed_should_retain_annotated_name_()
        {
            var schema = Schema.Create(_ => { _.InputObject<PocoNameAnnotated>().RemoveClrType(); });
            schema.HasInputObject(PocoNameAnnotated.AnnotatedName).Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.InputObject<Poco>().Name("Foo"); });
            schema.GetInputObject<Poco>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.InputObject<PocoNameAnnotated>().Name("Foo"); });
            schema.GetInputObject<PocoNameAnnotated>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("  xy")]
        [InlineData("")]

        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {

            Schema.Create(_ =>
            {
                var poco = _.InputObject<Poco>();
                Action rename = () => poco.Name(name);
                rename.Should().Throw<InvalidNameException>().WithMessage($"Cannot rename input object Poco. \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo");
                var poco = _.InputObject<Poco>();
                Action rename = () => poco.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename input object Poco to ""Foo"", input object Foo already exists. All GraphQL type names must be unique.");
            });
        }


        [Spec(nameof(untyped_item_can_have_clr_type_added))]
        [Fact]
        public void untyped_item_can_have_clr_type_added_()
        {
            var schema = Schema.Create(_ => { _.InputObject("Foo").ClrType<Poco>(); });
            schema.HasInputObject<Poco>().Should().BeTrue();
        }


        [Spec(nameof(untyped_item_cannot_have_clr_type_added_that_is_already_in_use))]
        [Fact(Skip = "needs implementation")]
        public void untyped_item_cannot_have_clr_type_added_that_is_already_in_use_()
        {
            Schema.Create(_ =>
            {
                _.InputObject<Poco>();
                var foo = _.InputObject("Foo");
                Action add = () => foo.ClrType<Poco>();
                add.Should().Throw<DuplicateClrTypeException>();
            });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs design")]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo")
                    .Field("outputField", "OutputType");

                Action add = () => _.InputObject("OutputType");
                add.Should().Throw<Exception>()
                    .WithMessage(
                        "Cannot add input object OutputType because OutputType is already identified as an output type.");
            });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs impl")]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "OutputType");
                var bar = _.InputObject("Bar");
                Action rename = () => bar.Name("OutputType");
                rename.Should().Throw<Exception>()
                    .WithMessage(
                        @"Cannot rename input object Bar to ""OutputTYpe"" because OutputType is already identified as an output type.");
            });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", "OutputType");
                var poco = _.InputObject<Poco>();
                Action rename = () => poco.Name("OutputType");
                rename.Should().Throw<Exception>()
                    .WithMessage(
                        @"Cannot rename input object Bar to ""OutputTYpe"" because OutputType is already identified as an output type.");
            });
        }


        [Spec(nameof(UniquelyInputOutputTypeCollectionSpecs
            .clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "needs impl")]
        public void
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.Object("Foo").Field("outputField", PocoNameAnnotated.AnnotatedName);
                Action add = () => _.InputObject<PocoNameAnnotated>();
                add.Should().Throw<Exception>()
                    .WithMessage(
                        @"Cannot create input object AnnotatedName because because AnnotatedName is already identified as an output type.");
            });
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_with_custom_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_typed_item_with_conflicting_name_can_be_added_via_type_param_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void clr_typed_item_can_be_added_via_type_param_with_custom_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(custom_named_clr_typed_item_with_type_removed_should_retain_custom_name))]
        [Fact(Skip = "TODO")]
        public void custom_named_clr_typed_item_with_type_removed_should_retain_custom_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(adding_clr_type_to_item_changes_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_changes_name_()
        {
            var schema = Schema.Create(_ => { });
        }


        [Spec(nameof(clr_type_with_conflicting_name_can_be_added_with_custom_name))]
        [Fact(Skip = "TODO")]
        public void adding_clr_type_to_item_with_name_changes_name_from_param_()
        {
            var schema = Schema.Create(_ => { });
        }




        [Spec(nameof(cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts))]
        [Fact(Skip = "TODO")]
        public void cannot_add_clr_type_to_item_with_custom_name_if_name_conflicts_()
        {
            var schema = Schema.Create(_ => { });
        }
    }
}