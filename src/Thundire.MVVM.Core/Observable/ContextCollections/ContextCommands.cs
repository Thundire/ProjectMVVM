using System.Collections.Generic;
using System.Collections.ObjectModel;
using Thundire.MVVM.Core.Observable.ContextCollections.Items;

namespace Thundire.MVVM.Core.Observable.ContextCollections
{
    public class ContextCommands : Dictionary<byte, CommandItem[]>
    {
        private static readonly ContextCommands Void = new(0);
        public byte CurrentSelectedCommandsGroup { get; private set; }

        private ContextCommands(int capacity) : base(capacity) => Commands = new();
        public ContextCommands() => Commands = new();

        public static ContextCommands Empty() => Void;

        public ContextCommands SetCommandsFromGroup(byte group)
        {
            if (!TryGetValue(group, out var commands) || commands!.Length <= 0) return this;

            CurrentSelectedCommandsGroup = group;
            Commands.Clear();
            Commands.AddRange(commands);
            return this;
        }

        public ObservableCollection<CommandItem> Commands { get; }
    }
}