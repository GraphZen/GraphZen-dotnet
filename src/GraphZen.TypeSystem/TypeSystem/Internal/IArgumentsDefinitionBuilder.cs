// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface IArgumentsDefinitionBuilder<out TBuilder>
    {
        TBuilder RemoveArgument(string name);
        TBuilder Argument(string name, Action<InputValueBuilder> configurator);
        InputValueBuilder Argument(string name);
        TBuilder Argument(string name, string type);
        TBuilder Argument(string name, string type, Action<InputValueBuilder> configurator);

        TBuilder Argument<TArgument>(string name);
        TBuilder Argument<TArgument>(string name, Action<InputValueBuilder> configurator);

        TBuilder IgnoreArgument(string name);


        TBuilder UnignoreArgument(string name);
    }
}