using System.Threading.Tasks;

namespace Thundire.MVVM.WPF.Abstractions.ViewHandler
{
    public delegate ValueTask CloseViewEventHandler(object sender, CloseViewEventArgs args);
}