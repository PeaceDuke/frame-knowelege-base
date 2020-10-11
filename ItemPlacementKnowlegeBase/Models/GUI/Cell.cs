using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models.GUI
{
    class Cell
    {
            //координаты в сетке. (0,0) - левый верхний угол
        public int X { get; }
        public int Y { get; }
        public Item Item { get; }

        public Cell(int x, int y, Item item)
        {
            X = x;
            Y = y;
            Item = item;
        }
    }
}
