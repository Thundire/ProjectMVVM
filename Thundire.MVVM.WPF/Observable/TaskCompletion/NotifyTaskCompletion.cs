using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Thundire.MVVM.WPF.Observable.TaskCompletion
{
    public sealed class NotifyTaskCompletion : INotifyPropertyChanged
    {
        public NotifyTaskCompletion(Task task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync(task);
            }
        }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch { }

            var propertyChanged = PropertyChanged;
            if (propertyChanged == null) return;
            propertyChanged(this, new(nameof(Status)));
            propertyChanged(this, new(nameof(IsCompleted)));
            propertyChanged(this, new(nameof(IsNotCompleted)));
            if (task.IsCanceled)
            {
                propertyChanged(this, new(nameof(IsCanceled)));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new(nameof(IsFaulted)));
                propertyChanged(this, new(nameof(Exception)));
                propertyChanged(this, new(nameof(InnerException)));
                propertyChanged(this, new(nameof(ErrorMessage)));
            }
            else
            {
                propertyChanged(this, new(nameof(IsSuccessfullyCompleted)));
            }
        }

        public Task Task { get; }


        public TaskStatus Status => Task.Status;
        public bool IsCompleted => Task.IsCompleted;
        public bool IsNotCompleted => !Task.IsCompleted;

        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;

        public bool IsCanceled => Task.IsCanceled;
        public bool IsFaulted => Task.IsFaulted;

        public AggregateException Exception => Task.Exception;
        public Exception InnerException => Exception?.InnerException;
        public string ErrorMessage => InnerException?.Message;


        public event PropertyChangedEventHandler PropertyChanged;
    }
}