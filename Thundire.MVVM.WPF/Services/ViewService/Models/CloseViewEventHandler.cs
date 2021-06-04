using System.Threading.Tasks;

namespace Thundire.MVVM.WPF.Services.ViewService.Models
{
    public delegate ValueTask CloseViewEventHandler(object sender, CloseViewEventArgs args);
}