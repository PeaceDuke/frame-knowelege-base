using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    class Slot
    {
        private Object slotValue;

        public string Name { get; set; }

        public Type ValueType { get; set; }

        public Object Value 
        { 
            get { return slotValue; } 
            set { slotValue = SetValue(value); } 
        }

        public Slot(string name, Object value, Type type)
        {
            Name = name;
            ValueType = type;
            Value = value;
        }

        public Object SetValue(Object newValue)
        {
            if (ValidateValue(newValue))
                return newValue;
            else
                throw new ArgumentException("Необходимы или строки или другие фреймы");
        }

        public bool ValidateValue(Object value)
        {
            return value.GetType().Equals(ValueType);
        }
    }
}
