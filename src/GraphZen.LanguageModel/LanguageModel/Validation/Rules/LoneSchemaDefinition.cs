// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel.Validation.Rules
{
    public class LoneSchemaDefinition : DocumentValidationRuleVisitor
    {
        public LoneSchemaDefinition(DocumentValidationContext context) : base(context)
        {
        }
    }
}