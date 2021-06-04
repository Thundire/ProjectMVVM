using System;
using System.Collections.Generic;
using System.Windows;
using Thundire.MVVM.WPF.Observable.Base;

namespace Thundire.MVVM.WPF.Services.ViewService
{
    public class TemplateSelector : NotifyBase
    {
        private readonly Dictionary<string, DataTemplate> _templates;
        private DataTemplate _template;

        public TemplateSelector(Dictionary<string, DataTemplate> templates)
        {
            _templates = templates;
        }

        public object Content { get; private set; }
        public bool IsShowView { get; private set; }
        public DataTemplate Template => _template;

        public void ChangePresenter(string presenterKey)
        {
            if (_templates.TryGetValue(presenterKey, out _template))
            {
                RaisePropertyChanged(nameof(Template));
            }
        }

        public void ChangeContent(object content, string presenterKey = null)
        {
            if (!string.IsNullOrEmpty(presenterKey) || !string.IsNullOrWhiteSpace(presenterKey))
            {
                var presenterFound = false;
                if (_templates.TryGetValue(presenterKey, out _template))
                {
                    presenterFound = true;
                }
                else
                {
                    var type = content.GetType();
                    foreach (var value in _templates.Values)
                    {
                        var expected = value.DataType as Type;
                        if (expected == type || type.IsAssignableTo(expected))
                        {
                            _template = value;
                            presenterFound = true;
                            break;
                        }
                    }
                }

                if(!presenterFound) throw new ArgumentException($"Cannot find template for content of type {content.GetType()}");
            }

            Content = content;
            RaisePropertyChanged(nameof(Content));
        }

        public void Show()
        {
            IsShowView = true;
            RaisePropertyChanged(nameof(IsShowView));
        }

        public void Hide()
        {
            IsShowView = false;
            RaisePropertyChanged(nameof(IsShowView));
        }
    }
}