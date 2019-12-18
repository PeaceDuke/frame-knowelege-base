using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ItemPlacementKnowlegeBase.Models
{
    [Serializable]
    public class Frame
    {
        public bool IsBase { get; set; }

        public string Name { get; set; }

        public List<Slot> Slots { get; }

        public string ParentFrameName { get; private set; }

        public Frame(string name, bool isbase = false, string parent = null)
        {
            if (parent is null)
            {
                Slots = new List<Slot>();
            }
            else
            {
                /*if (parent.IsBase)
                    Slots = new List<Slot>(parent.Slots);
                else
                    throw new ArgumentException("Фрейм может быть создан только от базового");*/
            }
            Name = name;
            ParentFrameName = parent;
            IsBase = isbase;
        }

        public void AddSlot(string slotName, Object value, Type type)
        {
            if (IsSlotExist(slotName))
                throw new ArgumentException("Слот с таким именем уже существует");
            if (IsBase && type == typeof(Frame))
                if(!(value as Frame).IsBase)
                    throw new ArgumentException("Базовый фрейм может принимать лишь базовые фреймы в качестве значений слота");
            Slots.Add(new Slot(slotName, value, type));
        }

        public void AddSlot(string slotName, Type type)
        {
            if (IsSlotExist(slotName))
                throw new ArgumentException("Слот с таким именем уже существует");
            Slots.Add(new Slot(slotName, type));
        }

        public Slot GetSlot(string name)
        {
            return Slots.Find(x => x.Name.Equals(name));
        }

        public void SetSlot(Slot slot)
        {
            var slotIndex = Slots.FindIndex(x => x.Name == slot.Name);
            if (slotIndex < 0)
                Slots.Add(slot);
            else
                Slots[slotIndex] = slot;
        }
        public Frame Clone(string name)
        {
            if (IsSlotExist(name))
                throw new ArgumentException("Фрейм стаким именем уже существует");
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                var frame = (Frame)formatter.Deserialize(ms);
                frame.Name = name;
                frame.ParentFrameName = this.Name;

                return frame;
            }
        }


        public Frame Determinate(string name) 
        {
            var frame = this.Clone(name);
            frame.IsBase = false;

            return frame;
        }

        private bool IsSlotExist(string name)
        {
            return Slots.Any(x => x.Name == name);
        }

        public void RemoveSlot(string slotName)
        {
            if (IsSlotExist(slotName))
                Slots.RemoveAll(x => x.Name == slotName);
            else
                throw new ArgumentException("Слота с таким именем не существует");
        }

        public void RemoveSlot(Slot slot)
        {
            if (IsSlotExist(slot.Name))
                Slots.Remove(slot);
            else
                throw new ArgumentException("Слота с таким именем не существует");
        }

        public override string ToString()
        {
            return Name;
        }

        //public void Refresh()
        //{
        //    if(!(parentFrame is null))
        //    {
        //        var t = parentFrame.Slots;

        //    }
        //}
    }
}
