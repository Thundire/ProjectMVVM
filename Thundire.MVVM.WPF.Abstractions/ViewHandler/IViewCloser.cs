using System.Threading.Tasks;

namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public interface IViewCloser
    {
        ValueTask CloseSelf(CloseViewEventArgs args);
    }
}