using ItemPlacementKnowlegeBase.Gui;
using ItemPlacementKnowlegeBase.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Services
{
    class TestKnowlegeBaseProvider : IKnowlegeBaseProvider
    {

        public List<string> getLastReasoning()
        {
            throw new NotImplementedException();
        }

        public Field loadField()
        {
            Frame fieldFrame = Test.Test.GetFieldFrame();
            Field field = new Field(int.Parse(fieldFrame["Высота"].ValueAsString), int.Parse(fieldFrame["Ширина"].ValueAsString), 32);
            return field;
        }

        public List<Item> loadItems()
        {
            List<Item> items = new List<Item>();
            List<Frame> itemFrames = Test.Test.GetItemFrameList();

            foreach (Frame frame in itemFrames)
            {
                items.Add(new Item(frame.Name, createChessBoardBitmap(32, 8, Brushes.Black, new SolidBrush(ColorTranslator.FromHtml(frame["Цвет"].ValueAsString)))));
            }

            return items;
        }

        public bool placeItem(Cell cell, Item item)
        {
            throw new NotImplementedException();
        }

        public bool removeItem(Cell cell)
        {
            throw new NotImplementedException();
        }

        private Bitmap createChessBoardBitmap(int size, int cells, Brush color1, Brush color2)
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
