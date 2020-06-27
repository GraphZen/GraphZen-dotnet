// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableMemberDefinition : IMemberDefinition
    {
        new SchemaDefinition Schema { get; }
        MemberDefinitionBuilder Builder { get; }
    }

    [GraphQLIgnore]
    public interface IMemberDefinition
    {
        internal string? GetName() => this is INamed named ? named.Name : null;

        ISchemaDefinition Schema { get; }


        internal IMemberDefinition? GetParentMember() => this switch
        {
            IDirectiveDefinition directive => directive.Schema,
            INamedTypeDefinition type => type.Schema,
            IArgumentDefinition argumentDefinition => argumentDefinition.DeclaringMember,
            IEnumValueDefinition enumValueDefinition => enumValueDefinition.DeclaringType,
            IFieldDefinition fieldDefinition when
            fieldDefinition.DeclaringType is IInterfaceTypeDefinition @interface => @interface,
            IFieldDefinition fieldDefinition when fieldDefinition.DeclaringType is IObjectTypeDefinition @object =>
            @object,
            IInputFieldDefinition inputFieldDefinition => inputFieldDefinition.DeclaringType,
            _ => null
        };
    }
}