using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    public class Slot
    {
        public string Name { get; set; }

        private Type ValueType { get; }

        public Object Value { get; }

        public Slot(string name, Object value, Type type)
        {
            if (ValidateValue(value, type))
            {
                Name = name;
                ValueType = type;
                Value = value;
            }
            else
                throw new ArgumentException("Необходимы или строки или другие фреймы");
        }

        public Object SetValue(Object newValue)
        {
            if (ValidateValue(newValue, ValueType))
                return newValue;
            else
                throw new ArgumentException("Значение не соответствует типу слота");
        }

        public Object SetValue(Object newValue, Type newType)
        {
            if (ValidateValue(newValue, newType))
                return newValue;
            else
                throw new ArgumentException("Значение не соответствует указаному типу слота");
        }

        public bool ValidateValue(Object value, Type type)
        {
            return value.GetType().Equals(type);
        }
    }
}
