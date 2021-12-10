using System.Collections.Generic;
using System;

#pragma warning disable CS0660, CS0661, CS0659

namespace Thundire.MVVM.Core.Observable
{
    public class Notice : NotifyBase, IEquatable<Notice>
    {
        private string? _message;
        private NoticeStatus _status;

        public string? Message { get => _message; set => Set(ref _message, value); }
        public NoticeStatus Status
        {
            get => _status; set => Set(ref _status, value).DoOnSuccess(state =>
            {
                if (state.New is NoticeStatus.Hide)
                {
                    OnNoticeHided?.Invoke(this);
                }
            });
        }

        public event Action<Notice>? OnNoticeHided;
        
        public override bool Equals(object? obj) => Equals(obj as Notice);

        public bool Equals(Notice? other) =>
            other is not null &&
            _message == other._message &&
            _status == other._status;

        public static bool operator ==(Notice left, Notice right) => EqualityComparer<Notice>.Default.Equals(left, right);

        public static bool operator !=(Notice left, Notice right) => !(left == right);
    }
}