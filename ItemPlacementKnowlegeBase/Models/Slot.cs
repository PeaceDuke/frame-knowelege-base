using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    [Serializable]
    public class Slot
    {
        private List<Type> valildeTypes = new List<Type>() { typeof(int), typeof(float), typeof(string), typeof(Frame) };

        public string Name { get; set; }

        public Type ValueType { get; private set; }

        public Object Value { get; private set; }

        public Slot(string name, Object value, Type type)
        {
            if (ValidateValue(value, type))
            {
                Name = name;
                ValueType = type;
                Value = value;
            }
            else
                throw new ArgumentException("Значение не соответствует указаному типу слота");
        }

        public Slot(string name, Type type)
        {
                Name = name;
                ValueType = type;
        }

        public void SetValue(Object newValue)
        {
            if (ValidateValue(newValue, ValueType))
            {
                Value = newValue;
            }
            else
                throw new ArgumentException("Значение не соответствует типу слота");
        }

        public void SetValue(Object newValue, Type newType)
        {
            if (ValidateValue(newValue, newType))
            {
                ValueType = newType;
                Value = newValue;
            }
            else
                throw new ArgumentException("Значение не соответствует указаному типу слота");
        }

        public bool ValidateValue(Object value, Type type)
        {
            if (valildeTypes.Any(x => x == type))
                return value.GetType().Equals(type);
            else
                throw new ArgumentException("Данный тип не может быть использован");
        }
    }
}
