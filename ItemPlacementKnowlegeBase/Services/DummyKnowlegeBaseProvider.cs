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

        public List<Item> LoadItems()
        {
            if (items == null)
            {
                items = new List<Item>();
                items.Add(new Item("chest", ImagePathes.CHEST, TextureResource.get(ImagePathes.CHEST).Source));
                items.Add(new Item("closet", ImagePathes.CLOSET, TextureResource.get(ImagePathes.CLOSET).Source));
                items.Add(new Item("commode", ImagePathes.COMMODE, TextureResource.get(ImagePathes.COMMODE).Source));
                items.Add(new Item("freezer", ImagePathes.FREEZER, TextureResource.get(ImagePathes.FREEZER).Source));
                items.Add(new Item("lamp1", ImagePathes.LAMP1, TextureResource.get(ImagePathes.LAMP1).Source));
                items.Add(new Item("lamp2", ImagePathes.LAMP2,TextureResource.get(ImagePathes.LAMP2).Source));
                items.Add(new Item("picture1", ImagePathes.PICTURE1, TextureResource.get(ImagePathes.PICTURE1).Source));
                items.Add(new Item("picture2", ImagePathes.PICTURE2, TextureResource.get(ImagePathes.PICTURE2).Source));
                items.Add(new Item("shelf", ImagePathes.SHELF, TextureResource.get(ImagePathes.SHELF).Source));
                items.Add(new Item("table", ImagePathes.TABLE, TextureResource.get(ImagePathes.TABLE).Source));
            }
            return items;
        }
        public bool RemoveItem(Cell cell)
        {
            var i = field.Cells.FindIndex(_cell => _cell.X == cell.X && _cell.Y == cell.Y);
            field.Cells[i] = new Cell(cell.X, cell.Y, null);
            return true;
        }

        public bool PlaceItem(Cell cell, Item item)
        {
            var i = field.Cells.FindIndex(_cell => _cell.X == cell.X && _cell.Y == cell.Y);
            field.Cells[i] = new Cell(cell.X, cell.Y, item);
            return true;
        }

        public List<string> GetLastReasoning()
        {
            return new List<string>(new[] { "ну а хули!" });
        }

        public Field LoadField()
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
