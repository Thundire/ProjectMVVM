using System.Threading.Tasks;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public delegate ValueTask CloseViewEventHandler(object? sender, CloseViewEventArgs args);
}