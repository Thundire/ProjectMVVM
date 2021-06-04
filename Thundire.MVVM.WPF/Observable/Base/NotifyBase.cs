using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Thundire.MVVM.WPF.Observable.Base
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies subscribers when a property value has been updated
        /// </summary>
        /// <param name="propertyName">Property that must notify it subscribers</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new(propertyName));

        /// <summary>
        /// Set value to field if it not equals field value and Notify that property changed
        /// </summary>
        /// <typeparam name="T">Property Type</typeparam>
        /// <param name="field">Field that must be changed with value</param>
        /// <param name="value">Value to set</param>
        /// <param name="propertyName">Property that must notify about changes</param>
        /// <returns>False - if value equals field value, True - if field value changed on value</returns>
        protected virtual SetInstance<T> Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (Equals(field, value)) return new(false, field, value);
            var set = new SetInstance<T>(true, field, value);
            field = value;
            RaisePropertyChanged(propertyName);
            return set;
        }
    }
}