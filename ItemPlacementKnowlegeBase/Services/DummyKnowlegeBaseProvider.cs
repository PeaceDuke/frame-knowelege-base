using ItemPlacementKnowlegeBase.Models;
using ItemPlacementKnowlegeBase.Gui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemPlacementKnowlegeBase.Images;

namespace ItemPlacementKnowlegeBase.Services
{
    class DummyKnowlegeBaseProvider : IKnowlegeBaseProvider
    {
        private static List<Item> items = null;
        private Field field;

        public DummyKnowlegeBaseProvider()
        {
            //тут создается база знаний
            field = new Field(10, 4, 80);
        }

        public List<Item> loadItems()
        {
            if (items == null)
            {
                items = new List<Item>();
                items.Add(new Item("chest", TextureResource.get(ImagePathes.CHEST).Source));
                items.Add(new Item("closet", TextureResource.get(ImagePathes.CLOSET).Source));
                items.Add(new Item("commode", TextureResource.get(ImagePathes.COMMODE).Source));
                items.Add(new Item("freezer", TextureResource.get(ImagePathes.FREEZER).Source));
                items.Add(new Item("lamp1", TextureResource.get(ImagePathes.LAMP1).Source));
                items.Add(new Item("lamp2", TextureResource.get(ImagePathes.LAMP2).Source));
                items.Add(new Item("picture1", TextureResource.get(ImagePathes.PICTURE1).Source));
                items.Add(new Item("picture2", TextureResource.get(ImagePathes.PICTURE2).Source));
                items.Add(new Item("shelf", TextureResource.get(ImagePathes.SHELF).Source));
                items.Add(new Item("table", TextureResource.get(ImagePathes.TABLE).Source));
            }
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
