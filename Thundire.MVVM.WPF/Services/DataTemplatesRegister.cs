using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Thundire.MVVM.WPF.Services.Interfaces;

namespace Thundire.MVVM.WPF.Services
{
    public class DataTemplatesRegister : ITemplatesCache, ITemplatesRegister
    {
        private readonly Dictionary<string, Template> _templates = new();

        public void AddTemplates(Action<ITemplatesCacheBuilder> configuration)
        {
            var factory = new TemplatesCacheBuilder(_templates);
            configuration?.Invoke(factory);
        }

        public DataTemplate GetTemplate(object content, string presenterKey = null)
        {
            if ((!string.IsNullOrEmpty(presenterKey) || !string.IsNullOrWhiteSpace(presenterKey)) && _templates.TryGetValue(presenterKey, out var template))
            {
                return template.DataTemplate;
            }
            
            var type = content.GetType();
            var query = _templates.Values.Select(value => (value, expected: value.ContentType))
                .Where(@t => @t.expected == type || type.IsAssignableTo(@t.expected))
                .Select(@t => @t.value.DataTemplate);
            return query.FirstOrDefault();
        }

        private class TemplatesCacheBuilder : ITemplatesCacheBuilder
        {
            private readonly Dictionary<string, Template> _templates;

            public TemplatesCacheBuilder(Dictionary<string, Template> templates) => _templates = templates;

            public ITemplatesCacheBuilder Register(Type element, string presenterKey, Type dataType)
            {
                if (!element.IsAssignableTo(typeof(FrameworkElement)))
                    throw new ArgumentException("Element must be of type Framework Element", nameof(element));

                var template = new DataTemplate(dataType) { VisualTree = new(element) };
                _templates.Add(presenterKey, new(dataType, template));
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

                    if (template.DataType is not Type dataType)
                        throw new InvalidOperationException("All data templates in resource must have data type assigned as x:Type");

                    if (!_templates.TryAdd(entry.Key.ToString(), new(dataType, template)))
                        throw new InvalidOperationException($"Cannot register template with key: {entry.Key}");
                }
            }

            private static void RegisterTemplateInResources(DataTemplate template)
            {
                if (template is null) 
                    throw new ArgumentNullException(nameof(template), "Template was null");

                Application.Current.Resources.Add(Guid.NewGuid(), template);
            }
        }

        private class Template
        {
            public Template(Type contentType, DataTemplate dataTemplate)
            {
                ContentType = contentType;
                DataTemplate = dataTemplate;
            }

            public Type ContentType { get; }
            public DataTemplate DataTemplate { get; }
        }
    }
}