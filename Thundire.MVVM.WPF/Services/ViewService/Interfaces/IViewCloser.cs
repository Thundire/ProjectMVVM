using System.Threading.Tasks;
using Thundire.MVVM.WPF.Services.ViewService.Models;

namespace Thundire.MVVM.WPF.Services.ViewService.Interfaces
{
    public interface IViewCloser
    {
        ValueTask CloseSelf(CloseViewEventArgs args);
    }
}