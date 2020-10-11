using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ItemPlacementKnowlegeBase.Models
{
    /// <summary>
    /// Слот фрейма доменного (перечислимого) типа
    /// </summary>
    [Serializable]
    public class DomainSlot : Slot
    {
        private DomainValue _value;
        private Domain _domain;

        public override string TypeAsString => Domain.Name;

        public override string ValueAsString => Value?.Text ?? string.Empty;

        /// <summary>
        /// Домен слота
        /// </summary>
        public Domain Domain
        {
            get => _domain;
            set
            {
                if (_domain != null) _domain.PropertyChanged -= DomainOnPropertyChanged;
                _domain = value;
                if (_domain != null) _domain.PropertyChanged += DomainOnPropertyChanged;
                
                OnPropertyChanged(nameof(Domain));
                OnPropertyChanged(nameof(TypeAsString));
            }
        }

        private void DomainOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Domain));
            OnPropertyChanged(nameof(TypeAsString));
        }

        /// <summary>
        /// Значение слота
        /// </summary>
        public DomainValue Value
        {
            get => _value;
            set
            {
                if (value != null && !Domain.Values.Contains(value))
                    throw new ArgumentOutOfRangeException(nameof(value), "Значение должно быть из домена");

                if (_value != null) _value.PropertyChanged -= ValueOnPropertyChanged;
                _value = value;
                if (_value != null) _value.PropertyChanged += ValueOnPropertyChanged;
                
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(ValueAsString));
            }
        }

        private void ValueOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Value));
            OnPropertyChanged(nameof(ValueAsString));
        }

        public DomainSlot(string name, Domain domain, DomainValue value = null, bool isSystemSlot = false, bool isRequestable = false, bool isResult = false) : base(name, isSystemSlot, isRequestable, isResult)
        {
            Domain = domain;
            Value = value;
        }
    }
}