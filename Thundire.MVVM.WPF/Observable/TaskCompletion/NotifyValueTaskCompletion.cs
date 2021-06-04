using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Thundire.MVVM.WPF.Observable.TaskCompletion
{
    public class NotifyValueTaskCompletion : INotifyPropertyChanged
    {
        public NotifyValueTaskCompletion(ValueTask task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync(task);
            }
        }

        private async ValueTask WatchTaskAsync(ValueTask task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                OnExceptionThrown?.Invoke(ex);
            }

            var propertyChanged = PropertyChanged;
            if (propertyChanged == null) return;
            propertyChanged(this, new(nameof(IsCompleted)));
            propertyChanged(this, new(nameof(IsNotCompleted)));
            if (task.IsCanceled)
            {
                propertyChanged(this, new(nameof(IsCanceled)));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new(nameof(IsFaulted)));
            }
            else
            {
                propertyChanged(this, new(nameof(IsSuccessfullyCompleted)));
            }
        }

        public ValueTask Task { get; }
        
        public bool IsCompleted => Task.IsCompleted;
        public bool IsNotCompleted => !Task.IsCompleted;

        public bool IsSuccessfullyCompleted => Task.IsCompletedSuccessfully;

        public bool IsCanceled => Task.IsCanceled;
        public bool IsFaulted => Task.IsFaulted;

        public event Action<Exception> OnExceptionThrown;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}