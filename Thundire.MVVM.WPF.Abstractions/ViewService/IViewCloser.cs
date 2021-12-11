using System.Threading.Tasks;

namespace Thundire.MVVM.WPF.Abstractions.ViewService
{
    public interface IViewCloser
    {
        ValueTask CloseSelf(CloseViewEventArgs args);
    }
}