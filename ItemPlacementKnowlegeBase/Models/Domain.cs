using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    /// <summary>
    /// Домен допустимых значений
    /// </summary>
    [Serializable]
    public class Domain : INotifyPropertyChanged
    {
        private readonly List<Delegate> _serializableDelegates;

        private string _name;

        /// <summary>
        /// Имя домена
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
        /// Допустимые значения домена
        /// </summary>
        public ObservableCollection<DomainValue> Values { get; }

        /// <summary>
        /// Значение домена по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Значение домена</returns>
        public DomainValue this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

        /// <summary>
        /// Значение домена по тексту значения домена
        /// </summary>
        /// <param name="text">Текст</param>
        /// <returns>Значение домена</returns>
        public DomainValue this[string text] =>
            Values.First(v => v.Text == text);

        [field: NonSerialized] public event PropertyChangedEventHandler PropertyChanged;

        public Domain(string name, IEnumerable<DomainValue> values = null)
        {
            Name = name;
            Values = values == null
                ? new ObservableCollection<DomainValue>()
                : new ObservableCollection<DomainValue>(values);
            Values.CollectionChanged += ValuesOnCollectionChanged;

            _serializableDelegates = new List<Delegate>();
        }

        private void ValuesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    foreach (var newItem in e.NewItems)
                    {
                        var domainValue = newItem as DomainValue;
                        ProcessAddedDomainValue(domainValue);
                    }

                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    foreach (var oldItem in e.OldItems)
                    {
                        var domainValue = oldItem as DomainValue;
                        ProcessRemovedDomainValue(domainValue);
                    }

                    break;
                }
            }
        }

        private void ProcessRemovedDomainValue(DomainValue domainValue)
        {
            domainValue.PropertyChanged -= DomainValueOnPropertyChanged;
        }

        private void ProcessAddedDomainValue(DomainValue domainValue)
        {
            if (Values.Count(dv => ReferenceEquals(dv, domainValue)) > 1)
            {
                Values.Remove(domainValue);
                throw new ArgumentException("Одинаковые значения доменов недопустимы", nameof(domainValue));
            }

            domainValue.PropertyChanged += DomainValueOnPropertyChanged;
        }

        private void DomainValueOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Values));
        }

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
                PropertyChanged += (PropertyChangedEventHandler) invocation;
            }
        }
    }
}