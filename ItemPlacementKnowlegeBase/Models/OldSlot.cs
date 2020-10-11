using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    [Serializable]
    public class OldSlot
    {
        private List<Type> valildeTypes = new List<Type>() { typeof(int), typeof(float), typeof(string), typeof(Frame) };

        public string Name { get; set; }

        [JsonIgnore]
        public Type ValueType { get; private set; }

        public String Type { get { return ValueType.Name; } }

        public Object Value { get; private set; }

        public OldSlot(string name, Object value, Type type)
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

        public OldSlot(string name, Type type)
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
