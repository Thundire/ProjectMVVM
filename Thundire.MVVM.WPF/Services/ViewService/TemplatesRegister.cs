using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Thundire.MVVM.WPF.Services.ViewService.Interfaces;

namespace Thundire.MVVM.WPF.Services.ViewService
{
    public class TemplatesRegister : ITemplatesRegister, ITemplatesSelectorFactory
    {
        private static readonly Dictionary<string, TemplatesCacheBuilder> Register = new();

        public TemplatesRegister AddGroup(string group, Action<ITemplatesCacheBuilder> configuration)
        {
            if(Register.ContainsKey(group))
                throw new ArgumentException("Group already registered", nameof(group));

            var factory = new TemplatesCacheBuilder(group);
            configuration?.Invoke(factory);
            Register.Add(group, factory);

            return this;
        }

        public TemplateSelector CreateSelector(string group)
        {
            if(!Register.TryGetValue(group, out var templatesFactory)) 
                throw new ArgumentException("Group not existed", nameof(group));

            return new(templatesFactory.Templates);
        }

        private class TemplatesCacheBuilder : ITemplatesCacheBuilder
        {
            private readonly string _groupName;
            public readonly Dictionary<string, DataTemplate> Templates = new();

            public TemplatesCacheBuilder(string groupName)
            {
                _groupName = groupName;
            }

            public ITemplatesCacheBuilder Register(Type element, string presenterKey, Type dataType)
            {
                if (!element.IsAssignableTo(typeof(FrameworkElement)))
                    throw new ArgumentException("Element must be of type Framework Element", nameof(element));

                var template = new DataTemplate(dataType)
                {
                    VisualTree = new(element)
                };
                Templates.Add(presenterKey, template);
                RegisterTemplateInResources(template);
                return this;
            }

            public void FromResourceDictionary(Uri resourceDictionary)
            {
                var resource = Application.Current.Resources.MergedDictionaries.FirstOrDefault(r => r.Source == resourceDictionary);
                if (resource is null)
                {
                    resource = new() { Source = resourceDictionary };
                    Application.Current.Resources.MergedDictionaries.Add(resource);
                }
                foreach (DictionaryEntry entry in resource)
                {
                    if (entry.Value is not DataTemplate template) continue;
                    if (!Templates.TryAdd(entry.Key.ToString(), template))
                    {
                        throw new InvalidOperationException($"Cannot register template with key: {entry.Key} in Group: {_groupName}");
                    }
                }
            }

            private static void RegisterTemplateInResources(DataTemplate template)
            {
                if (template is null) throw new ArgumentNullException(nameof(template), "Template was null");
                Application.Current.Resources.Add(Guid.NewGuid(), template);
            }
        }
    }
}