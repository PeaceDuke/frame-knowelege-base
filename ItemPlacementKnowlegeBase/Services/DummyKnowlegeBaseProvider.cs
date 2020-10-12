using ItemPlacementKnowlegeBase.Models;
using ItemPlacementKnowlegeBase.Gui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Services
{
    class DummyKnowlegeBaseProvider : IKnowlegeBaseProvider
    {
        private static List<Item> items = new List<Item>();
        private Field field;

        public DummyKnowlegeBaseProvider()
        {
            //тут создается база знаний
            field = new Field(21, 10, 32);
            items.Add(new Item("предмет 1", createChessBoardBitmap(32, 8, Brushes.Red, Brushes.Yellow)));
            items.Add(new Item("предмет 2", createChessBoardBitmap(32, 8, Brushes.White, Brushes.Black)));
            items.Add(new Item("предмет 3", createChessBoardBitmap(32, 8, Brushes.Green, Brushes.Blue)));
        }

        public List<Item> loadItems()
        {
            return items;
        }
        public bool removeItem(Cell cell)
        {
            var i = field.Cells.FindIndex(_cell => _cell.X == cell.X && _cell.Y == cell.Y);
            field.Cells[i] = new Cell(cell.X, cell.Y, null);
            return true;
        }

        public bool placeItem(Cell cell, Item item)
        {
            var i = field.Cells.FindIndex(_cell => _cell.X == cell.X && _cell.Y == cell.Y);
            field.Cells[i] = new Cell(cell.X, cell.Y, item);
            return true;
        }

        public List<string> getLastReasoning()
        {
            return new List<string>(new[] { "ну а хули!" });
        }

        public Field loadField()
        {
            return field;
        }

        private static Bitmap createChessBoardBitmap(int size, int cells, Brush color1, Brush color2)
        {
            var bitmap = new Bitmap(size, size);
            var cellSize = size / cells;
            Graphics gr = Graphics.FromImage(bitmap);
            for (int i = 0; i <= cells; i++)
                for (int j = 0; j <= cells; j++)
                {
                    var brush = (i % 2 == j % 2) ? color1 : color2;
                    gr.FillRectangle(brush, i * cellSize, j * cellSize, cellSize, cellSize);
                }
            return bitmap;
        }
    }
}
