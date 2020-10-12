using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Gui
{
    public class Item
    {
        public string Name { get; }

        //придумай тут формат в котором будет цвет, лучше думаю hex

        public Bitmap Bitmap { get; }

        public Item(string name, Bitmap bitmap)
        {
            Name = name;
            Bitmap = bitmap;
        }

        public override bool Equals(object obj)
        {
            Item item = obj as Item;
            if (item == null)
            {
                return false;
            }
            else
            {
                return this.Name == item.Name && this.Bitmap == item.Bitmap;
            }

        }
    }
}
