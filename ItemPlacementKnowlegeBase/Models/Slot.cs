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
    /// Обобщение слота фрейма
    /// </summary>
    [Serializable]
    public abstract class Slot : INotifyPropertyChanged
    {
        private readonly List<Delegate> _serializableDelegates;

        private string _name;
        private bool _isResult;
        private bool _isSystemSlot;
        private bool _isRequestable;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Имя слота
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Является ли слот системным
        /// </summary>
        public bool IsSystemSlot
        {
            get => _isSystemSlot;
            set
            {
                _isSystemSlot = value;
                OnPropertyChanged(nameof(IsSystemSlot));
            }
        }

        /// <summary>
        /// Является ли слот запрашиваемым
        /// </summary>
        public bool IsRequestable
        {
            get => _isRequestable;
            set
            {
                _isRequestable = value;
                OnPropertyChanged(nameof(IsRequestable));
            }
        }

        /// <summary>
        /// Является ли слот результатом
        /// </summary>
        public bool IsResult
        {
            get => _isResult;
            set
            {
                _isResult = value;
                OnPropertyChanged(nameof(IsResult));
            }
        }

        public abstract string TypeAsString { get; }

        public abstract string ValueAsString { get; }

        protected Slot(string name, bool isSystemSlot = false, bool isRequestable = false, bool isResult = false)
        {
            Name = name;
            IsSystemSlot = isSystemSlot;
            IsRequestable = isRequestable;
            IsResult = isResult;

            _serializableDelegates = new List<Delegate>();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Name;
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
