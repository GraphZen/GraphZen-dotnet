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

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces
{
    [NoReorder]
    public class InterfacesTests
    {
        // ReSharper disable once InconsistentNaming
        private interface PlainInterface
        {
        }

        [GraphQLName(AnnotatedName)]
        // ReSharper disable once InconsistentNaming
        private interface PlainInterfaceAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("abc &*(")]
        // ReSharper disable once InconsistentNaming
        private interface PlainInterfaceInvalidNameAnnotation
        {
        }


        [Spec(nameof(InputOrOutputTypeSpecs
            .named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs design/impl")]
        public void named_item_cannot_be_added_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("input", "Bar");
                Action add = () => _.Interface("Bar");
                add.Should().Throw<Exception>();
            });
        }


        [Spec(nameof(InputOrOutputTypeSpecs
            .named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io))]
        // [Fact(Skip = "needs design/impl")]
        [Fact]
        public void named_item_cannot_be_renamed_to_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("input", "Bar");
                var baz = _.Interface("Baz");
                Action rename = () => baz.Name("Bar");
                rename.Should().Throw<Exception>();
            });
        }


        [Spec(nameof(InputOrOutputTypeSpecs
            .clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io))]
        [Fact(Skip = "needs design/implementation")]
        public void clr_typed_item_cannot_be_renamed_if_name_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("input", "Bar");
                var poci = _.Interface<PlainInterface>();
                Action rename = () => poci.Name("Bar");
                rename.Should().Throw<Exception>().WithMessage("something about input/output type");
            });
        }


        [Spec(nameof(InputOrOutputTypeSpecs
            .clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io
        ))]
        [Fact(Skip = "needs design/impl")]
        public void
            clr_typed_item_with_name_attribute_cannot_be_added_if_name_attribute_conflicts_with_type_identity_of_opposite_io_()
        {
            Schema.Create(_ =>
            {
                _.InputObject("Foo").Field("input", PlainInterfaceAnnotatedName.AnnotatedName);
                Action add = () => _.Interface<PlainInterfaceAnnotatedName>();
                add.Should().Throw<Exception>().WithMessage("something about input/output type");
            });
        }


        [Spec(nameof(SdlSpec.item_can_be_defined_by_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"interface Foo"); });
            schema.HasInterface("Foo").Should().BeTrue();
        }


        [Spec(nameof(SdlExtensionSpec.item_can_be_defined_by_sdl_extension))]
        [Fact(Skip = "needs design/impl")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend interface Foo"); });
            schema.HasInterface("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void named_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo"); });
            schema.HasInterface("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void named_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData("  xy")]
        [InlineData("")]
        public void named_item_cannot_be_added_with_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface(name);
                add.Should().Throw<InvalidNameException>("x");
            });
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void named_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface("Foo").Name("Bar"); });
            schema.HasInterface("Bar").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_removed))]
        [Fact(Skip = "needs impl")]
        public void named_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface("Foo");
                _.RemoveInterface("Foo");
            });
            schema.HasInterface("Foo").Should().BeFalse();
        }


        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void named_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                Action remove = () => _.RemoveInterface((string)null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_item_can_be_added_()
        {
            var schema = Schema.Create(_ => { _.Interface(typeof(PlainInterface)); });
            schema.HasInterface<PlainInterface>().Should().BeTrue();
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_item_cannot_be_added_with_invalid_name_attribute_()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Interface<PlainInterfaceInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    @"Cannot get or create GraphQL interface type builder with CLR interface 'PlainInterfaceInvalidNameAnnotation'. The name ""abc &*("" specified in the GraphQLNameAttribute on the PlainInterfaceInvalidNameAnnotation CLR interface is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact(Skip = "needs design/impl")]
        public void clr_typed_item_can_be_removed_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>();
                _.RemoveInterface(typeof(PlainInterface));
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact(Skip = "needs impl")]
        public void clr_typed_item_can_be_removed_via_type_param_()
        {
            var schema = Schema.Create(_ =>
            {
                _.Interface<PlainInterface>();
                _.RemoveInterface<PlainInterface>();
            });
            schema.HasInterface<PlainInterface>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_item_cannot_be_removed_with_null_value_()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveInterface((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_can_be_added_via_type_param))]
        [Fact]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterface>(); });
            schema.HasInterface<PlainInterface>().Should().BeTrue();
        }

        [Spec(nameof(clr_typed_item_can_be_renamed))]
        [Fact]
        public void clr_typed_item_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterface>().Name("Foo"); });
            schema.GetInterface<PlainInterface>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_with_name_attribute_can_be_renamed))]
        [Fact]
        public void clr_typed_item_with_name_attribute_can_be_renamed_()
        {
            var schema = Schema.Create(_ => { _.Interface<PlainInterfaceAnnotatedName>().Name("Foo"); });
            schema.GetInterface<PlainInterfaceAnnotatedName>().Name.Should().Be("Foo");
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_with_an_invalid_name))]
        [Theory]
        [InlineData("  xy")]
        [InlineData("")]
        public void clr_typed_item_cannot_be_renamed_with_an_invalid_name_(string name)
        {
            Schema.Create(_ =>
            {
                var poci = _.Interface<PlainInterface>();
                Action rename = () => poci.Name(name);
                rename.Should().Throw<InvalidNameException>()
                    .WithMessage(
                        $"Cannot rename interface PlainInterface: \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_renamed_if_name_already_exists))]
        [Fact]
        public void clr_typed_item_cannot_be_renamed_if_name_already_exists_()
        {
            Schema.Create(_ =>
            {
                _.Interface("Foo");
                var poci = _.Interface<PlainInterface>();
                Action rename = () => poci.Name("Foo");
                rename.Should().Throw<DuplicateNameException>().WithMessage(
                    @"Cannot rename interface PlainInterface to ""Foo"", interface Foo already exists. All GraphQL type names must be unique.");
            });
        }
    }
}