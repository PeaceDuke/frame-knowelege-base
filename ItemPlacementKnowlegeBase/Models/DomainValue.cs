using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    /// <summary>
    /// Значение домена
    /// </summary>
    [Serializable]
    public class DomainValue : INotifyPropertyChanged
    {
        private readonly List<Delegate> _serializableDelegates;

        private string _text;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public DomainValue(string text)
        {
            Text = text;

            _serializableDelegates = new List<Delegate>();
        }

        public static implicit operator DomainValue(string text) =>
            text == null ? null : new DomainValue(text);

        protected bool Equals(DomainValue other)
        {
            return _text == other._text;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DomainValue) obj);
        }

        public override int GetHashCode()
        {
            return (_text != null ? _text.GetHashCode() : 0);
        }

        public override string ToString() => Text;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            _serializableDelegates.Clear();
            var handler = PropertyChanged;

            if (handler != null)
            {
                foreach (var invocation in handler.GetInvocationList())
                {
                    if (invocation.Target.GetType().IsSerializable)
                    {
                        _serializableDelegates.Add(invocation);
                    }
                }
            }
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            if (_serializableDelegates == null) return;

            foreach (var invocation in _serializableDelegates)
            {
                PropertyChanged += (PropertyChangedEventHandler)invocation;
            }
        }
    }
}
