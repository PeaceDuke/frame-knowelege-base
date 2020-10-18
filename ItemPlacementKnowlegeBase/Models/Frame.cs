using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace ItemPlacementKnowlegeBase.Models
{
    /// <summary>
    /// Фрейм
    /// </summary>
    [Serializable]
    public class Frame : INotifyPropertyChanged
    {
        private readonly List<Delegate> _serializableDelegates;

        private readonly TextSlot _nameSystemSlot;
        private readonly FrameSlot _parentSystemSlot;

        public readonly string NameSlotName = "Имя";
        public readonly string ParentSlotName = "Родитель";

        /// <summary>
        /// Слот по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Слот</returns>
        public Slot this[int index] => Slots[index];

        /// <summary>
        /// Слот по имени
        /// </summary>
        /// <param name="name">Имя слота</param>
        /// <returns>Слот</returns>
        public Slot this[string name] => Slots.First(s => s.Name == name);

        /// <summary>
        /// Имя фрейма
        /// </summary>
        public string Name
        {
            get => _nameSystemSlot.Text;
            set
            {
                _nameSystemSlot.Text = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Родительский фрейм
        /// </summary>
        public Frame Parent
        {
            get => _parentSystemSlot.Frame;
            set
            {
                var oldParent = _parentSystemSlot.Frame;
                if (_parentSystemSlot.Frame != null) oldParent.PropertyChanged -= ParentOnPropertyChanged;
                _parentSystemSlot.Frame = value;
                if (value != null) value.PropertyChanged += ParentOnPropertyChanged;
                ProcessParentChange(oldParent, value);
                OnPropertyChanged(nameof(Parent));
            }
        }

        private void ParentOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Parent));
        }

        /// <summary>
        /// Список фреймов-наследников
        /// </summary>
        public ObservableCollection<Frame> Children { get; }

        /// <summary>
        /// Список слотов
        /// </summary>
        public ObservableCollection<Slot> Slots { get; }

        public Frame(string name)
        {
            Children = new ObservableCollection<Frame>();
            Slots = new ObservableCollection<Slot>();

            _nameSystemSlot = new TextSlot(NameSlotName, isSystemSlot: true);
            _parentSystemSlot = new FrameSlot(ParentSlotName, isSystemSlot: true);

            _nameSystemSlot.PropertyChanged += NameSystemSlotOnPropertyChanged;
            _parentSystemSlot.PropertyChanged += ParentSystemSlotOnPropertyChanged;

            Slots.Add(_nameSystemSlot);
            Slots.Add(_parentSystemSlot);

            Name = name;

            Children.CollectionChanged += ChildrenOnCollectionChanged;
            Slots.CollectionChanged += SlotsOnCollectionChanged;

            _serializableDelegates = new List<Delegate>();
        }

        private void ParentSystemSlotOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FrameSlot.Frame))
            {
                OnPropertyChanged(nameof(Parent));
                OnPropertyChanged(nameof(Children));
            }
        }

        private void NameSystemSlotOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TextSlot.Text))
                OnPropertyChanged(nameof(Name));
        }

        public TextSlot GetNameSystemSlot() =>
            Slots.OfType<TextSlot>().First(s => s.Name == NameSlotName && s.IsSystemSlot);

        public FrameSlot GetParentSystemSlot() =>
            Slots.OfType<FrameSlot>().First(s => s.Name == ParentSlotName && s.IsSystemSlot);

        public void ReplaceSlot(Slot oldSlot, Slot newSlot)
        {
            var oldIndex = Slots.IndexOf(oldSlot);
            Slots.Insert(oldIndex, newSlot);
            Slots.Remove(oldSlot);
        }

        private void SlotsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in e.NewItems)
                    {
                        var addedSlot = newItem as Slot;
                        ProcessAddedSlot(addedSlot);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                {
                    foreach (var oldItem in e.OldItems)
                    {
                        var removedSlot = oldItem as Slot;
                        ProcessRemovedSlot(removedSlot);
                    }

                    break;
                }
            }

            OnPropertyChanged(nameof(Slots));
        }

        private void ProcessRemovedSlot(Slot removedSlot)
        {
            if (removedSlot.IsSystemSlot)
            {
                Slots.Add(removedSlot);
                throw new Exception("Нельзя удалять системные слоты");
            }

            removedSlot.PropertyChanged -= FrameSlotOnPropertyChanged;
        }

        private void ProcessAddedSlot(Slot addedFrameSlot)
        {
            if (Slots.Count(s => ReferenceEquals(s, addedFrameSlot)) > 1)
            {
                Slots.Remove(addedFrameSlot);
                throw new ArgumentException("Такой слот уже есть у этого фрейма", nameof(addedFrameSlot));
            }

            addedFrameSlot.PropertyChanged += FrameSlotOnPropertyChanged;
        }

        private void FrameSlotOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Slots));
        }

        private void ChildrenOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in e.NewItems)
                    {
                        var addedChild = newItem as Frame;
                        ProcessAddedChild(addedChild);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var oldItem in e.OldItems)
                    {
                        var removedChild = oldItem as Frame;
                        ProcessRemovedChild(removedChild, e.OldItems);
                    }

                    break;
            }

            OnPropertyChanged(nameof(Children));
        }

        private void ProcessParentChange(Frame oldParent, Frame newParent)
        {
            if (newParent == null)
            {
                oldParent?.Children.Remove(this);
            }
            else
            {
                newParent.Children.Add(this);
            }
        }

        private void ProcessAddedChild(Frame child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));

            if (Children.Count(c => ReferenceEquals(c, child)) > 1)
                throw new ArgumentException("Фрейм уже является наследником", nameof(child));

            child._parentSystemSlot.Frame = this;
            child.PropertyChanged += ChildOnPropertyChanged;
        }

        private void ChildOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //OnPropertyChanged(nameof(Children));
        }

        private void ProcessRemovedChild(Frame child, IList oldItems)
        {
            child._parentSystemSlot.Frame = null;
            child.PropertyChanged -= ChildOnPropertyChanged;
        }

        [field: NonSerialized] public event PropertyChangedEventHandler PropertyChanged;

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
                PropertyChanged += (PropertyChangedEventHandler) invocation;
            }
        }
    }
}