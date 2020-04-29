#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming

namespace GraphZen.TypeSystem {
public  partial interface ISchemaBuilder<TContext> {
#region InputObject type accessors

         ISchemaBuilder<TContext> IgnoreInputObject<TInputObject>() where TInputObject: notnull;

         ISchemaBuilder<TContext> IgnoreInputObject(Type clrType);

         ISchemaBuilder<TContext> IgnoreInputObject(string name);


#endregion
#region Interface type accessors

         ISchemaBuilder<TContext> IgnoreInterface<TInterface>() where TInterface: notnull;

         ISchemaBuilder<TContext> IgnoreInterface(Type clrType);

         ISchemaBuilder<TContext> IgnoreInterface(string name);





         
        IInterfaceTypeBuilder<object, TContext> Interface(Type clrType);


        IInterfaceTypeBuilder<object, TContext> Interface(string name);


        IInterfaceTypeBuilder<TInterface, TContext> Interface<TInterface>() where TInterface : notnull;

#endregion
#region Object type accessors

         ISchemaBuilder<TContext> IgnoreObject<TObject>() where TObject: notnull;

         ISchemaBuilder<TContext> IgnoreObject(Type clrType);

         ISchemaBuilder<TContext> IgnoreObject(string name);





         
        IObjectTypeBuilder<object, TContext> Object(Type clrType);


        IObjectTypeBuilder<object, TContext> Object(string name);


        IObjectTypeBuilder<TObject, TContext> Object<TObject>() where TObject : notnull;

#endregion
#region Union type accessors

         ISchemaBuilder<TContext> IgnoreUnion<TUnion>() where TUnion: notnull;

         ISchemaBuilder<TContext> IgnoreUnion(Type clrType);

         ISchemaBuilder<TContext> IgnoreUnion(string name);





         
        IUnionTypeBuilder<object, TContext> Union(Type clrType);


        IUnionTypeBuilder<object, TContext> Union(string name);


        IUnionTypeBuilder<TUnion, TContext> Union<TUnion>() where TUnion : notnull;

#endregion
}
}
