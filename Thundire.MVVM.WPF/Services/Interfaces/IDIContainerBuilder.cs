using System;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Services.Interfaces
{
    public interface IDIContainerBuilder
    {
        void RegisterType(Type type, LifeTimeMode mode);
        void RegisterTypeAs(Type type, Type asType, LifeTimeMode mode);
    }
}