#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public partial class SchemaBuilder<TContext>
    {
        #region Directives

        public IDirectiveBuilder<object> Directive(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            var internalBuilder = Builder.Directive(name, ConfigurationSource.Explicit)!;
            var builder = new DirectiveBuilder<object>(internalBuilder);
            return builder;
        }


        public IDirectiveBuilder<TDirective> Directive<TDirective>() where TDirective : notnull
        {
            var internalBuilder = Builder.Directive(typeof(TDirective), ConfigurationSource.Explicit)!;
            var builder = new DirectiveBuilder<TDirective>(internalBuilder);
            return builder;
        }

        public IDirectiveBuilder<object> Directive(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Directive(clrType, ConfigurationSource.Explicit)!;
            var builder = new DirectiveBuilder<object>(internalBuilder);
            return builder;
        }


        public ISchemaBuilder<TContext> UnignoreDirective<TDirective>() where TDirective : notnull
        {
            Builder.UnignoreDirective(typeof(TDirective), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreDirective(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreDirective(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreDirective(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.UnignoreDirective(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<TContext> IgnoreDirective<TDirective>() where TDirective : notnull
        {
            Builder.IgnoreDirective(typeof(TDirective), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreDirective(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreDirective(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreDirective(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.IgnoreDirective(name, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveDirective<TDirective>() where TDirective : notnull
        {
            Builder.RemoveDirective(typeof(TDirective), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveDirective(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveDirective(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveDirective(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.RemoveDirective(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Types

        public ISchemaBuilder<TContext> UnignoreType<TClrType>() where TClrType : notnull
        {
            Builder.UnignoreType(typeof(TClrType), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreType(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.UnignoreType(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<TContext> IgnoreType<TClrType>() where TClrType : notnull
        {
            Builder.IgnoreType(typeof(TClrType), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreType(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.IgnoreType(name, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveType<TClrType>() where TClrType : notnull
        {
            Builder.RemoveType(typeof(TClrType), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveType(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.RemoveType(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Objects

        public IObjectTypeBuilder<object, TContext> Object(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            var internalBuilder = Builder.Object(name, ConfigurationSource.Explicit)!;
            var builder = new ObjectTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public IObjectTypeBuilder<TObject, TContext> Object<TObject>() where TObject : notnull
        {
            var internalBuilder = Builder.Object(typeof(TObject), ConfigurationSource.Explicit)!;
            var builder = new ObjectTypeBuilder<TObject, TContext>(internalBuilder);
            return builder;
        }

        public IObjectTypeBuilder<object, TContext> Object(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Object(clrType, ConfigurationSource.Explicit)!;
            var builder = new ObjectTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }


        public ISchemaBuilder<TContext> UnignoreObject<TObject>() where TObject : notnull
        {
            Builder.UnignoreObject(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreObject(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.UnignoreObject(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<TContext> IgnoreObject<TObject>() where TObject : notnull
        {
            Builder.IgnoreObject(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreObject(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.IgnoreObject(name, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveObject<TObject>() where TObject : notnull
        {
            Builder.RemoveObject(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveObject(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.RemoveObject(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Unions

        public IUnionTypeBuilder<object, TContext> Union(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            var internalBuilder = Builder.Union(name, ConfigurationSource.Explicit)!;
            var builder = new UnionTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public IUnionTypeBuilder<TUnion, TContext> Union<TUnion>() where TUnion : notnull
        {
            var internalBuilder = Builder.Union(typeof(TUnion), ConfigurationSource.Explicit)!;
            var builder = new UnionTypeBuilder<TUnion, TContext>(internalBuilder);
            return builder;
        }

        public IUnionTypeBuilder<object, TContext> Union(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Union(clrType, ConfigurationSource.Explicit)!;
            var builder = new UnionTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }


        public ISchemaBuilder<TContext> UnignoreUnion<TUnion>() where TUnion : notnull
        {
            Builder.UnignoreUnion(typeof(TUnion), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreUnion(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreUnion(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreUnion(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.UnignoreUnion(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<TContext> IgnoreUnion<TUnion>() where TUnion : notnull
        {
            Builder.IgnoreUnion(typeof(TUnion), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreUnion(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreUnion(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreUnion(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.IgnoreUnion(name, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveUnion<TUnion>() where TUnion : notnull
        {
            Builder.RemoveUnion(typeof(TUnion), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveUnion(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveUnion(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveUnion(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.RemoveUnion(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Scalars

        public ISchemaBuilder<TContext> UnignoreScalar<TScalar>() where TScalar : notnull
        {
            Builder.UnignoreScalar(typeof(TScalar), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreScalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreScalar(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreScalar(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.UnignoreScalar(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<TContext> IgnoreScalar<TScalar>() where TScalar : notnull
        {
            Builder.IgnoreScalar(typeof(TScalar), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreScalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreScalar(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreScalar(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.IgnoreScalar(name, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveScalar<TScalar>() where TScalar : notnull
        {
            Builder.RemoveScalar(typeof(TScalar), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveScalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveScalar(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveScalar(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.RemoveScalar(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Enums

        public IEnumTypeBuilder<string> Enum(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Enum(name, ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<string>(internalBuilder);
            return builder;
        }


        public IEnumTypeBuilder<TEnum> Enum<TEnum>() where TEnum : notnull
        {
            var internalBuilder = Builder.Enum(typeof(TEnum), ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<TEnum>(internalBuilder);
            return builder;
        }

        public IEnumTypeBuilder<string> Enum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Enum(clrType, ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<string>(internalBuilder);
            return builder;
        }


        public ISchemaBuilder<TContext> UnignoreEnum<TEnum>() where TEnum : notnull
        {
            Builder.UnignoreEnum(typeof(TEnum), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreEnum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreEnum(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreEnum(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.UnignoreEnum(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<TContext> IgnoreEnum<TEnum>() where TEnum : notnull
        {
            Builder.IgnoreEnum(typeof(TEnum), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreEnum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreEnum(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreEnum(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.IgnoreEnum(name, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveEnum<TEnum>() where TEnum : notnull
        {
            Builder.RemoveEnum(typeof(TEnum), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveEnum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveEnum(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveEnum(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.RemoveEnum(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region Interfaces

        public IInterfaceTypeBuilder<object, TContext> Interface(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            var internalBuilder = Builder.Interface(name, ConfigurationSource.Explicit)!;
            var builder = new InterfaceTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }

        public IInterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>() where TInterface : notnull
        {
            var internalBuilder = Builder.Interface(typeof(TInterface), ConfigurationSource.Explicit)!;
            var builder = new InterfaceTypeBuilder<TInterface, TContext>(internalBuilder);
            return builder;
        }

        public IInterfaceTypeBuilder<object, TContext> Interface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Interface(clrType, ConfigurationSource.Explicit)!;
            var builder = new InterfaceTypeBuilder<object, TContext>(internalBuilder);
            return builder;
        }


        public ISchemaBuilder<TContext> UnignoreInterface<TInterface>() where TInterface : notnull
        {
            Builder.UnignoreInterface(typeof(TInterface), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreInterface(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.UnignoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<TContext> IgnoreInterface<TInterface>() where TInterface : notnull
        {
            Builder.IgnoreInterface(typeof(TInterface), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInterface(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.IgnoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveInterface<TInterface>() where TInterface : notnull
        {
            Builder.RemoveInterface(typeof(TInterface), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveInterface(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.RemoveInterface(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion

        #region InputObjects

        public IInputObjectTypeBuilder<object> InputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            var internalBuilder = Builder.InputObject(name, ConfigurationSource.Explicit)!;
            var builder = new InputObjectTypeBuilder<object>(internalBuilder);
            return builder;
        }


        public IInputObjectTypeBuilder<TInputObject> InputObject<TInputObject>() where TInputObject : notnull
        {
            var internalBuilder = Builder.InputObject(typeof(TInputObject), ConfigurationSource.Explicit)!;
            var builder = new InputObjectTypeBuilder<TInputObject>(internalBuilder);
            return builder;
        }

        public IInputObjectTypeBuilder<object> InputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.InputObject(clrType, ConfigurationSource.Explicit)!;
            var builder = new InputObjectTypeBuilder<object>(internalBuilder);
            return builder;
        }


        public ISchemaBuilder<TContext> UnignoreInputObject<TInputObject>() where TInputObject : notnull
        {
            Builder.UnignoreInputObject(typeof(TInputObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreInputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreInputObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> UnignoreInputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.UnignoreInputObject(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<TContext> IgnoreInputObject<TInputObject>() where TInputObject : notnull
        {
            Builder.IgnoreInputObject(typeof(TInputObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreInputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInputObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> IgnoreInputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.IgnoreInputObject(name, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveInputObject<TInputObject>() where TInputObject : notnull
        {
            Builder.RemoveInputObject(typeof(TInputObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveInputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.RemoveInputObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<TContext> RemoveInputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            name.AssertValidNameArgument(nameof(name));
            Builder.RemoveInputObject(name, ConfigurationSource.Explicit);
            return this;
        }

        #endregion
    }
}
// Source Hash Code: 12056203023732778951