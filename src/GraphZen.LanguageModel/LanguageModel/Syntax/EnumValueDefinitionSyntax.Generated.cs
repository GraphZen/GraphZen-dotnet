#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.LanguageModel {
public  partial class EnumValueDefinitionSyntax {

	    /// <summary>Empty, read-only list of <see cref="EnumValueDefinitionSyntax"/> nodes.</summary>
		public static IReadOnlyList<EnumValueDefinitionSyntax> EmptyList {get;} = ImmutableList<EnumValueDefinitionSyntax>.Empty; 
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> enters a <see cref="EnumValueDefinitionSyntax"/> node.</summary>
		public override void VisitEnter( GraphQLSyntaxVisitor visitor) => visitor.EnterEnumValueDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor"/> leaves a <see cref="EnumValueDefinitionSyntax"/> node.</summary>
		public override void VisitLeave( GraphQLSyntaxVisitor visitor) => visitor.LeaveEnumValueDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> enters a <see cref="EnumValueDefinitionSyntax"/> node.</summary>
		public override TResult VisitEnter<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterEnumValueDefinition(this);
		/// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}"/> leaves a <see cref="EnumValueDefinitionSyntax"/> node.</summary>
		public override TResult VisitLeave<TResult>( GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveEnumValueDefinition(this);
		public override SyntaxKind Kind {get;} = SyntaxKind.EnumValueDefinition;	

}
}
// Source Hash Code: 2065553021433748603