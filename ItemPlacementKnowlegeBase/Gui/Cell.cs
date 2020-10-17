using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Gui
{
    class Cell
    {
            //координаты в сетке. (0,0) - левый верхний угол
        public int X { get; }
        public int Y { get; }
        public Item Item { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Cell(int x, int y, Item item)
        {
            X = x;
            Y = y;
            Item = item;
        }

        public override bool Equals(object obj)
        {
            Cell cell = obj as Cell;
            if (cell == null)
            {
                return false;
            }
            else
            {
                return this.X == cell.X && this.Y == cell.Y && this.Item == cell.Item;
            }
        }
    }
}
