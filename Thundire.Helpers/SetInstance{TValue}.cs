using System;

namespace Thundire.Helpers
{
    public readonly ref struct SetInstance<T>
    {
        public SetInstance(bool success, T previous, T newValue)
        {
            State = new(success, previous, newValue);
        }

        public SetInstanceState State { get; init; }

        public SetInstance<T> Do(Action<SetInstanceState> behavior)
        {
            behavior?.Invoke(State);
            return this;
        }

        public SetInstance<T> DoOnSuccess(Action<SetInstanceState> behavior)
        {
            if (!State.Success) return this;
            behavior?.Invoke(State);
            return this;
        }

        public SetInstance<T> DoOnFailure(Action<SetInstanceState> behavior)
        {
            if (State.Success) return this;
            behavior?.Invoke(State);
            return this;
        }

        public readonly struct SetInstanceState
        {
            public SetInstanceState(bool success, T previous, T newValue)
            {
                Success = success;
                Previous = previous;
                New = newValue;
            }

            public bool Success { get; }
            public T Previous { get; }
            public T New { get; }
        }
    }
}