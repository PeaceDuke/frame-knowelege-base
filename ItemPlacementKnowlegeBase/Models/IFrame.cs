using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    public interface IFrame : ICloneable
    {
        string Name { get; set; }

        List<ISlot> Slots { get; }

        void AddSlot(ISlot slot);

        ISlot GetSlot(string name);

        void SetSlot(ISlot slot);

        void RemoveSlot(ISlot slot);

        object Clone();

        bool IsSlotExist(string name);
    }
}
