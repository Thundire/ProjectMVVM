using Thundire.MVVM.Core.Commands;

namespace Thundire.MVVM.Core.Observable.ContextCollections.Items
{
    public abstract record CommandItem;
    public sealed record CommandsSeparator : CommandItem;
    public record CommandInfo(string Name, IExecutableCommand Command);
}