using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Gui
{
    class Field
    {
        public int Heigth { get; }
        public int Width { get; }

        public int CellSize { get; }
        public List<Cell> Cells { get;}

        public Field(int width, int heigth, int cellSize)
        {
            Heigth = heigth;
            Width = width;
            CellSize = cellSize;
            Cells = new List<Cell>();
            for (int i = 0; i < width; i++)
                for (int j = 0; j < heigth; j++)
                    Cells.Add(new Cell(i, j, null));
        }
    }
}
