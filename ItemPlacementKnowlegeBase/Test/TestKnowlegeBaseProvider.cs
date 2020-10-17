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

        private List<Item> items;
        private Field field;

        public TestKnowlegeBaseProvider()
        {
            Frame fieldFrame = Test.Test.GetFieldFrame();
            field = new Field(int.Parse(fieldFrame["Высота"].ValueAsString), int.Parse(fieldFrame["Ширина"].ValueAsString), 32);
        }

        public List<string> getLastReasoning()
        {
            throw new NotImplementedException();
        }

        public Field loadField()
        {
            Frame fieldFrame = Test.Test.GetFieldFrame();
            List<Frame> cellFrames = new List<Frame>(fieldFrame.Slots.Where(x => !x.IsSystemSlot && x is FrameSlot).Select(x => ((FrameSlot)x).Frame));
            field.Cells.Clear();
            foreach (Frame cellFrame in cellFrames)
            {
                Cell cell = new Cell(int.Parse(cellFrame["X"].ValueAsString), int.Parse(cellFrame["Y"].ValueAsString));
                if (items != null)
                {
                    IEnumerable<Item> i = items.Where(x => x.Name == cellFrame["Предмет"].ValueAsString);
                    if(i.Any())
                    {
                        Item item = i.First();
                        if (item != null)
                            cell.Item = item;
                    }
                }
                field.Cells.Add(cell);

            }
            return field;
        }

        public List<Item> loadItems()
        {
            items = new List<Item>();
            List<Frame> itemFrames = Test.Test.GetItemFrameList();

            foreach (Frame frame in itemFrames)
            {
                if (frame.Name == "Пустота")
                    continue;
                items.Add(new Item(frame.Name, createChessBoardBitmap(32, 8, Brushes.Black, new SolidBrush(ColorTranslator.FromHtml(frame["Цвет"].ValueAsString)))));
            }

            return items;
        }

        public bool placeItem(Cell cell, Item item)
        {
            bool result = Test.Test.CheckRule(cell.X, cell.Y, item.Name);
            if (result)
                Test.Test.PutItem(cell.X, cell.Y, item.Name);
            return result;
        }

        public bool removeItem(Cell cell)
        {
            Test.Test.RemoveItem(cell.X, cell.Y);
            return true;
             
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
