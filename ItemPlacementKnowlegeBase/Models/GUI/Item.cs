using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models.GUI
{
    class Item
    {
        public string Name { get; }

        //придумай тут формат в котором будет цвет, лучше думаю hex
        public string Color { get; }

        public Item(string name, string color)
        {
            Name = name;
            Color = color;
        }
    }
}
