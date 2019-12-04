using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    class Frame : ICloneable
    {
        public string Name { get; set; }

        public List<Slot> Slots { get; }

        public Frame(string name)
        {
            Name = name;
            Slots = new List<Slot>();
        }

        public void AddSlot(string slotName, Object value, Type type)
        {
            if (Slots.FindIndex(x => x.Name == slotName) >= 0)
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
    }
}
