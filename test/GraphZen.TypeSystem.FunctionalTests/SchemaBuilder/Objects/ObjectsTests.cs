// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.FunctionalTests.Specs;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.ClrTypedCollectionSpecs;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.NamedCollectionSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.SchemaBuilder.Objects
{
    [NoReorder]
    public class ObjectsTests
    {

        public class PlainClass
        {
        }

        [GraphQLName(AnnotatedName)]
        public class PlainClassNameAnnotated
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName(InvalidName)]
        public class PlainClassInvalidNameAnnotation
        {
            public const string InvalidName = "abc @#$%^";
        }

        [Spec(nameof(named_item_can_be_added))]
        [Fact]
        public void _object_can_be_added_to_schema()
        {
            var schema = Schema.Create(_ => { _.Object("Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }

        [Spec(nameof(named_item_can_be_removed))]
        [Fact]
        public void object_can_be_removed_from_schema()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.RemoveObject("Foo");
            });
            schema.HasObject("Foo").Should().BeFalse();
        }


        [Spec(nameof(named_item_can_be_renamed))]
        [Fact]
        public void object_can_be_renamed()
        {
            // Priority: High
            var schema = Schema.Create(_ =>
            {
                _.Object("Foo");
                _.Object("Foo").Name("PlainClass");
            });
            schema.HasObject("Foo").Should().BeFalse();
            schema.HasObject("PlainClass").Should().BeTrue();
        }




        [Spec(nameof(named_item_cannot_be_added_with_invalid_name))]
        [Theory]
        [InlineData(")(&(*#")]
        public void object_cannot_be_added_with_invalid_name(string name)
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object(name);
                add.Should().Throw<InvalidNameException>().WithMessage(
                    $"Cannot get or create GraphQL type builder for object named \"{name}\". The type name \"{name}\" is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(named_item_cannot_be_added_with_null_value))]
        [Fact]
        public void object_cannot_be_added_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object((string)null!);
                add.Should().ThrowArgumentNullException("name");
            });
        }





        [Spec(nameof(named_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void object_cannot_be_removed_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveObject((string)null!);
                remove.Should().ThrowArgumentNullException("name");
            });
        }





        [Spec(nameof(clr_typed_item_can_be_added))]
        [Fact]
        public void clr_typed_object_can_be_added()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClassNameAnnotated>(); });
            schema.HasObject<PlainClassNameAnnotated>().Should().BeTrue();
        }




        [Spec(nameof(clr_typed_item_can_be_removed))]
        [Fact]
        public void clr_typed_object_can_be_removed()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClassNameAnnotated>();
                _.RemoveObject(typeof(PlainClassNameAnnotated));
            });
            schema.HasObject<PlainClassNameAnnotated>().Should().BeFalse();
        }


        [Spec(nameof(clr_typed_item_can_be_removed_via_type_param))]
        [Fact]
        public void clr_typed_object_can_be_removed_via_type_param()
        {
            var schema = Schema.Create(_ =>
            {
                _.Object<PlainClassNameAnnotated>();
                _.RemoveObject<PlainClassNameAnnotated>();
            });
            schema.HasObject<PlainClassNameAnnotated>().Should().BeFalse();
        }




        [Spec(nameof(clr_typed_item_cannot_be_added_with_invalid_name_attribute))]
        [Fact]
        public void clr_typed_object_cannot_be_added_with_invalid_name_attribute()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object<PlainClassInvalidNameAnnotation>();
                add.Should().Throw<InvalidNameException>().WithMessage(
                    @"Cannot get or create GraphQL object type builder with CLR class 'PlainClassInvalidNameAnnotation'. The name ""abc @#$%^"" specified in the GraphQLNameAttribute on the PlainClassInvalidNameAnnotation CLR class is not a valid GraphQL name. Names are limited to underscores and alpha-numeric ASCII characters.");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_added_with_null_value))]
        [Fact]
        public void clr_typed_object_cannot_be_added_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action add = () => _.Object((Type)null!);
                add.Should().ThrowArgumentNullException("clrType");
            });
        }


        [Spec(nameof(clr_typed_item_cannot_be_removed_with_null_value))]
        [Fact]
        public void clr_typed_object_cannot_be_removed_with_null_value()
        {
            Schema.Create(_ =>
            {
                Action remove = () => _.RemoveObject((Type)null!);
                remove.Should().ThrowArgumentNullException("clrType");
            });
        }



        [Spec(nameof(named_item_can_be_added_via_sdl))]
        [Fact]
        public void named_item_can_be_added_via_sdl_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"type Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }


        [Spec(nameof(named_item_can_be_added_via_sdl_extension))]
        [Fact(Skip = "TODO")]
        public void named_item_can_be_added_via_sdl_extension_()
        {
            var schema = Schema.Create(_ => { _.FromSchema(@"extend type Foo"); });
            schema.HasObject("Foo").Should().BeTrue();
        }










        [Spec(nameof(TypeSystemSpecs.ClrTypedCollectionSpecs.clr_typed_item_can_be_added_via_type_param))]
        [Fact()]
        public void clr_typed_item_can_be_added_via_type_param_()
        {
            var schema = Schema.Create(_ => { _.Object<PlainClass>(); });
            schema.HasObject<PlainClass>();
        }
    }
}