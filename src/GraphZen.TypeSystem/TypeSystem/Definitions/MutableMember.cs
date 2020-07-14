// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract class MutableMember : IMutableMember
    {
        private ConfigurationSource _configurationSource;
        private ConfigurationSource _descriptionConfigurationSource = ConfigurationSource.Convention;

        public abstract MutableSchema Schema { get; }
        ISchema IMember.Schema => Schema;

        protected MutableMember(ConfigurationSource configurationSource)
        {
            _configurationSource = configurationSource;
        }

        internal MemberDefinitionBuilder InternalBuilder => GetInternalBuilder();
        protected abstract MemberDefinitionBuilder GetInternalBuilder();

        public string? Description { get; private set; }

        public bool RemoveDescription(ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(_descriptionConfigurationSource))
            {
                Description = null;
                return true;
            }

            return false;
        }

        public bool SetDescription(string description, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(_descriptionConfigurationSource))
            {
                Description = description;
                _descriptionConfigurationSource = configurationSource;
                return true;
            }

            return false;
        }

        public ConfigurationSource GetDescriptionConfigurationSource() => _descriptionConfigurationSource;

        public ConfigurationSource GetConfigurationSource() => _configurationSource;

        public void UpdateConfigurationSource(ConfigurationSource configurationSource)
        {
            _configurationSource = _configurationSource.Max(configurationSource);
        }
    }
}