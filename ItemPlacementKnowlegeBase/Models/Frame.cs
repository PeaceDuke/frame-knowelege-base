using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    public class Frame : ICloneable
    {
        public string Name { get; set; }

        public List<Slot> Slots { get; }

        public Frame(string name)
        {
            Name = name;
            Slots = new List<Slot>();
        }

        public void AddSlot(Slot slot)
        {
            if (IsSlotExist(slot.Name))
                throw new ArgumentException("Слот с таким именем уже существует");
            Slots.Add(slot);
        }

        public void AddSlot(string slotName, Object value, Type type)
        {
            if (IsSlotExist(slotName))
                throw new ArgumentException("Слот с таким именем уже существует");
            Slots.Add(new Slot(slotName, value, type));
        }

        public void SetSlotValue(string slotName, Object value)
        {
            var slotIndex = Slots.FindIndex(x => x.Name == slotName);
            if (slotIndex < 0)
                throw new IndexOutOfRangeException();
            Slots[slotIndex].SetValue(value);
        }

        public object Clone() { return this.MemberwiseClone(); }

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
    }
}
