using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    public interface ISlot
    {
        string Name { get; set; }

        Object Value { get; }

        Object SetValue(Object newValue);

        Object SetValue(Object newValue, Type newType);
    }
}
