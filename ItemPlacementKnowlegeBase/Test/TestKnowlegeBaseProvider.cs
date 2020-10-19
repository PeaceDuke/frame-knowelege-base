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
    public class TestKnowlegeBaseProvider : IKnowlegeBaseProvider
    {

        private List<Item> items;
        private List<Rule> rules;
        private Field field;
        private string reason;

        public TestKnowlegeBaseProvider()
        {
            Frame fieldFrame = Test.Test.GetFieldFrame();
            field = new Field(int.Parse(fieldFrame["Высота"].ValueAsString), int.Parse(fieldFrame["Ширина"].ValueAsString), 32);
        }

        public List<string> GetLastReasoning()
        {
            var reasoning = Test.Test.getReasoning();
            var binding = Test.Test.GetBindedFrames();
            var response = new List<string>() { reason };
            response.Add("Сработавшие фреймы:");
            response.AddRange(binding);
            return response;
        }

        public Field LoadField()
        {
            Frame fieldFrame = Test.Test.GetFieldFrame();
            List<Frame> cellFrames = new List<Frame>(fieldFrame.Slots.Where(x => !x.IsSystemSlot && x is FrameSlot).Select(x => ((FrameSlot)x).Frame));
            field.Cells.Clear();
            foreach (Frame cellFrame in cellFrames)
            {
                Cell cell = new Cell(int.Parse(cellFrame["X"].ValueAsString), int.Parse(cellFrame["Y"].ValueAsString));
                Item item = getItem(cellFrame["Предмет"].ValueAsString);
                if (item != null)
                    cell.Item = item;
                field.Cells.Add(cell);

            }
            return field;
        }

        public List<Item> LoadItems()
        {
            items = new List<Item>();
            List<Frame> itemFrames = Test.Test.GetItemFrameList();

            foreach (Frame frame in itemFrames)
            {
                if (frame.Name == "Пустота")
                    continue;
                var picture = TextureResource.get(frame["Изображение"].ValueAsString).Source;
                items.Add(new Item(frame.Name, frame["Изображение"].ValueAsString,  picture));
            }

            return items;
        }

        public List<Rule> LoadRules()
        {
            rules = new List<Rule>();
            List<Frame> ruleFrames = Test.Test.GetRuleFrameList();

            foreach (Frame frame in ruleFrames)
            {
                if (frame.Slots.Where(x => x.Name == "Расположение").Any())
                    rules.Add(new Rule(frame.Name, getItem(frame["Объект"].ValueAsString), getItem(frame["Субъект"].ValueAsString), frame["Тип правила"].ValueAsString, frame["Расположение"].ValueAsString));
                else
                    rules.Add(new Rule(frame.Name, getItem(frame["Объект"].ValueAsString), getItem(frame["Субъект"].ValueAsString), frame["Тип правила"].ValueAsString));
            }

            return rules;
        }

        public bool PlaceItem(Cell cell, Item item)
        {
            string result = Test.Test.CheckRule(cell.X, cell.Y, item.Name);
            if (result == null)
            {
                reason = "Нет ни одного правила разрешающего такое размещение";
                return false;
            }
            else if (result == "allow")
            {
                Test.Test.PutItem(cell.X, cell.Y, item.Name);
                return true;
            }
            else
            {
                reason = result;
                return false;
            }
        }

        public bool RemoveItem(Cell cell)
        {
            Test.Test.RemoveItem(cell.X, cell.Y);
            return true;
             
        }

        public Rule AddRuleToList(Item obj, Item subject, string place, string type, string reason)
        {
            KnowlegeBase knowlegeBase = Test.Test.GetModel();
            string name = "Отношение " + obj.Name + " и " + subject.Name + " "+ DateTime.Now.Ticks;
            Domain itemDomain = knowlegeBase.Domains.Where(x => x.Name == "Предметы").First();
            Domain typeDomain = knowlegeBase.Domains.Where(x => x.Name == "Тип правила").First();
            Domain placeDomain = knowlegeBase.Domains.Where(x => x.Name == "Расположение").First();
            Frame frame = new Frame(name);
            frame.Slots.Add(new DomainSlot("Объект", itemDomain, itemDomain[obj.Name], false, true));
            frame.Slots.Add(new DomainSlot("Субъект", itemDomain, itemDomain[subject.Name], false, true));
            if(!string.IsNullOrEmpty(place))
                frame.Slots.Add(new DomainSlot("Расположение", placeDomain, placeDomain[place], false, true));
            frame.Slots.Add(new DomainSlot("Тип правила", typeDomain, typeDomain[type], false, true));
            frame.Slots.Add(new TextSlot("Объяснение", reason));
            frame.Parent = knowlegeBase["Правило"];
            knowlegeBase.Frames.Add(frame);
            Rule rule = new Rule(name, obj, subject, type, place);
            rules.Add(rule);
            return rule;
        }

        public void RemoveRuleFromList(Rule rule)
        {
            rules.Remove(rule);
            KnowlegeBase knowlegeBase = Test.Test.GetModel();
            Frame itemFrame = knowlegeBase[rule.Name];
            knowlegeBase.Frames.Remove(itemFrame);
        }

        public Item AddItemToList(string name, string image)
        {
            KnowlegeBase knowlegeBase = Test.Test.GetModel();
            Domain domain = knowlegeBase.Domains.Where(x => x.Name == "Предметы").First();
            if (domain.Values.Where(x => x.Text == name).Any())
                throw new Exception("Предмет с таким названием уже есть в базе");
            DomainValue domainValue = new DomainValue(name);
            domain.Values.Add(domainValue);
            Frame frame = new Frame(name);
            frame.Slots.Add(new DomainSlot("Предмет", domain, domainValue, false, true));
            frame.Slots.Add(new TextSlot("Изображение", image));
            frame.Parent = knowlegeBase["Предмет"];
            knowlegeBase.Frames.Add(frame);
            Item item = new Item(name, image, TextureResource.get(image).Source);
            items.Add(item);
            return item;
        }

        public Item ChangeItemFromList(Item item, string name, string image)
        {
            KnowlegeBase knowlegeBase = Test.Test.GetModel();
            Frame itemFrame = knowlegeBase[item.Name];
            if (!string.IsNullOrEmpty(name))
            {
                DomainValue domainValue = knowlegeBase.Domains.Where(x => x.Name == "Предметы").First().Values.Where(y => y.Text == item.Name).First();
                domainValue.Text = name;
                itemFrame.Name = name;
                item.Name = name;
                ((DomainSlot)itemFrame["Предмет"]).Value = domainValue;
            }
            if (!string.IsNullOrEmpty(image))
            {
                item.ImageName = image;
                item.Bitmap = TextureResource.get(image).Source;
                ((TextSlot)itemFrame["Изображение"]).Text = image;
            }
            return item;

        }

        public void RemoveItemFromList(Item item)
        {
            items.Remove(item);
            KnowlegeBase knowlegeBase = Test.Test.GetModel();
            Frame itemFrame = knowlegeBase[item.Name];
            Frame emptyFrame = knowlegeBase["Пустота"];
            var test = knowlegeBase.Frames.Where(x => x.Slots.Where(y => y.Name == "Предмет" && y is FrameSlot && ((FrameSlot)y).Frame.Equals(itemFrame)).Any()).Select(x => x["Предмет"] as FrameSlot);
            foreach (var cellItemSlot in test)
            {
                cellItemSlot.Frame = emptyFrame;
            }
            knowlegeBase.Frames.Remove(itemFrame);
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

        private Item getItem(string name)
        {
            if (name == "Пустота" || name == "")
                return null;
            if(items.Where(x => x.Name == name).Any())
                return items.Where(x => x.Name == name).First();
            throw new Exception("Предмет не найден");
        }
    }
}
