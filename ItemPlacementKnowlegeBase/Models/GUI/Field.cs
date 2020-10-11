using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models.GUI
{
    class Field
    {
        public int Heigth { get; }
        public int Width { get; }
        public List<Cell> Cells { get;}

        public Field(int heigth, int width)
        {
            Heigth = heigth;
            Width = width;
            Cells = new List<Cell>();
            for (int i = 0; i < heigth; i++)
                for (int j = 0; j < width; j++)
                    Cells.Add(new Cell(i, j, null));
        }
    }
}
