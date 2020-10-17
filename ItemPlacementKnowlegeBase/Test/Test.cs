using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Models;
using ItemPlacementKnowlegeBase.Services;

namespace ItemPlacementKnowlegeBase.Test
{
    class Test
    {
        private static Domain[] domains = new[]
        {
            new Domain("Место", new[]
            {
                new DomainValue("Пол"),
                new DomainValue("Стена"),
                new DomainValue("Потолок"),
            }),
            new Domain("Тип правила", new[]
            {
                new DomainValue("Запрещающее"),
                new DomainValue("Разрешающее"),
            }),
            new Domain("Предметы", new[]
            {
                new DomainValue("Стол"),
                new DomainValue("Стул"),
                new DomainValue("Горшок"),
            }),
            new Domain("Расположение", new[]
            {
                new DomainValue("Выше"),
                new DomainValue("Ниже"),
                new DomainValue("Левее"),
                new DomainValue("Правее"),
                new DomainValue("Рядом"),
            }),
        };
        private static KnowlegeBase TestFrameModel
        {
            get
            {
                var frames = new[]
                {
                    new Frame("Предмет"),
                    new Frame("Напольный предмет"),
                    new Frame("Стол"),
                    new Frame("Стул"),
                    new Frame("Горшок"),
                    new Frame("Поле"),
                    new Frame("Клетка"),
                    new Frame("Правило"),
                    new Frame("Событие"),
                    new Frame("Отношение стола и стула"),
                    new Frame("Отношение горшка и стола"),
                    new Frame("Отношение стола и стула 2"),
                    new Frame("Пустота"),
                };

                var frameModel = new KnowlegeBase();

                frames[1].Slots.Add(new DomainSlot("Место размещения", domains[0], domains[0][0]));
                frames[1].Parent = frames[0];

                frames[2].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][0]));
                frames[2].Slots.Add(new TextSlot("Цвет", "#000000"));
                frames[2].Slots.Add(new TextSlot("Изображение", "/path/to/image"));
                frames[2].Parent = frames[1];

                frames[3].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][1]));
                frames[3].Slots.Add(new TextSlot("Цвет", "#FFFFFF"));
                frames[3].Slots.Add(new TextSlot("Изображение", "/path/to/image"));
                frames[3].Parent = frames[1];

                frames[4].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][2]));
                frames[4].Slots.Add(new TextSlot("Цвет", "#111111"));
                frames[4].Slots.Add(new TextSlot("Изображение", "/path/to/image"));
                frames[4].Parent = frames[1];

                frames[5].Slots.Add(new TextSlot("Высота", "10"));
                frames[5].Slots.Add(new TextSlot("Ширина", "10"));

                frames[8].Slots.Add(new FrameSlot("Правило", frames[7], false, true));

                frames[9].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][0], false, true));
                frames[9].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][1], false, true));
                frames[9].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][2], false, true));
                frames[9].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][0]));
                frames[9].Parent = frames[7];

                frames[10].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][2], false, true));
                frames[10].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][0], false, true));
                frames[10].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][4], false, true));
                frames[10].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][1]));
                frames[10].Parent = frames[7];

                frames[11].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][0], false, true));
                frames[11].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][1], false, true));
                frames[11].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][3], false, true));
                frames[11].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][0]));
                frames[11].Parent = frames[7];

                frames[12].Parent = frames[0];

                foreach (var domain in domains)
                {
                    frameModel.Domains.Add(domain);
                }

                foreach (var frame in frames)
                {
                    frameModel.Frames.Add(frame);
                }

                return frameModel;
            }
        }

        private static Reasoner reasoner = new Reasoner(TestFrameModel);

        public static Frame GetFieldFrame()
        {
            Frame fieldFrame = reasoner.GetFrame("Поле");
            Frame cellFrame = reasoner.GetFrame("Клетка");
            Frame emptyItem = reasoner.GetFrame("Пустота");
            int h = int.Parse(fieldFrame["Высота"].ValueAsString);
            int w = int.Parse(fieldFrame["Ширина"].ValueAsString);
            Frame floor = new Frame("Пол");
            foreach (Slot slot in fieldFrame.Slots)
                if (!slot.IsSystemSlot)
                    floor.Slots.Add(slot);
            floor.Slots.Add(new DomainSlot("Место", domains[0], domains[0][0], false, true));
            floor.Parent = fieldFrame;
            reasoner.AddFrame(floor);
            Frame floorCell = new Frame("Клетка пола");
            floorCell.Slots.Add(new FrameSlot("Место", floor, false, true));
            floorCell.Parent = cellFrame;
            List<Frame> cellFrames = new List<Frame>();
            Domain domain = new Domain("Числа");
            int max = h > w ? h : w;
            for (int i = 0; i < max; i++)
                domain.Values.Add(new DomainValue(i.ToString()));
            reasoner.AddDomain(domain);
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    string name = "Клетка" + i + ":" + j;
                    Frame newCellFrame = new Frame(name);
                    newCellFrame.Slots.Add(new DomainSlot("X", domain, domain[i], false, true));
                    newCellFrame.Slots.Add(new DomainSlot("Y", domain, domain[j], false, true));
                    Frame leftFrame = j > 0 ? cellFrames[i * h + j - 1] : null;
                    Frame upFrame = i > 0 ? cellFrames[(i - 1) * h + j] : null;
                    if(leftFrame != null)
                    {
                        newCellFrame.Slots.Add(new FrameSlot("Слева", leftFrame));
                        leftFrame.Slots.Add(new FrameSlot("Справа", newCellFrame));
                    }
                    if(upFrame != null)
                    {
                        newCellFrame.Slots.Add(new FrameSlot("Выше", upFrame));
                        upFrame.Slots.Add(new FrameSlot("Ниже", newCellFrame));
                    }
                    newCellFrame.Slots.Add(new FrameSlot("Предмет", emptyItem));
                    newCellFrame.Parent = floorCell;
                    floor.Slots.Add(new FrameSlot(name, newCellFrame));
                    cellFrames.Add(newCellFrame);
                    reasoner.AddFrame(newCellFrame);
                }
            Frame cealing = new Frame("Пол");
            foreach (Slot slot in fieldFrame.Slots)
                if (!slot.IsSystemSlot)
                    cealing.Slots.Add(slot);
            cealing.Slots.Add(new DomainSlot("Место", domains[0], domains[0][0], false, true));
            cealing.Parent = fieldFrame;
            reasoner.AddFrame(cealing);
            Frame cealingCell = new Frame("Клетка пола");
            cealingCell.Slots.Add(new FrameSlot("Место", floor, false, true));
            cealingCell.Parent = cellFrame;
            cellFrames = new List<Frame>();
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    string name = "Клетка" + i + ":" + j;
                    Frame newCellFrame = new Frame(name);
                    newCellFrame.Slots.Add(new DomainSlot("X", domain, domain[i], false, true));
                    newCellFrame.Slots.Add(new DomainSlot("Y", domain, domain[j], false, true));
                    Frame leftFrame = j > 0 ? cellFrames[i * h + j - 1] : null;
                    Frame upFrame = i > 0 ? cellFrames[(i - 1) * h + j] : null;
                    if(leftFrame != null)
                    {
                        newCellFrame.Slots.Add(new FrameSlot("Слева", leftFrame));
                        leftFrame.Slots.Add(new FrameSlot("Справа", newCellFrame));
                    }
                    if(upFrame != null)
                    {
                        newCellFrame.Slots.Add(new FrameSlot("Выше", upFrame));
                        upFrame.Slots.Add(new FrameSlot("Ниже", newCellFrame));
                    }
                    newCellFrame.Slots.Add(new FrameSlot("Предмет", emptyItem));
                    newCellFrame.Parent = cealingCell;
                    cealing.Slots.Add(new FrameSlot(name, newCellFrame));
                    cellFrames.Add(newCellFrame);
                    reasoner.AddFrame(newCellFrame);
                }
            return floor;
        }

        public static List<Frame> GetItemFrameList()
        {
            return reasoner.GetAllSubFrames("Предмет");
        }

        public static bool GetRuleFrame()
        {
            Frame eventFrame = reasoner.GetFrame("Событие");

            while (true)
            {
                var slot = reasoner.test(eventFrame);
                if (slot == null)
                    break;
                Console.WriteLine(slot + "?");
                Console.WriteLine(String.Join(", ", slot.Domain.Values.Select(x => x.Text)));
                var valId = int.Parse(Console.ReadLine());
                reasoner.SetAnswer(slot.Domain.Values[valId]);
            }
            Console.WriteLine();
            return false;
        }


        public static void Do()
        {
    
            //Frame eventFrame = reasoner.GetFrame("Событие");

            //while (true)
            //{
            //    var slot = reasoner.test(eventFrame);
            //    if (slot == null)
            //        break;
            //    Console.WriteLine(slot + "?");
            //    Console.WriteLine(String.Join(", ", slot.Domain.Values.Select(x => x.Text)));
            //    var valId = int.Parse(Console.ReadLine());
            //    reasoner.SetAnswer(slot.Domain.Values[valId]);
            //}
            //Console.WriteLine();

            //Console.WriteLine("Answer: " + reasoner.GetAnswer());
            //reasoner.GetInferringPath();
            //Console.WriteLine();


            //reasoner.Clear();

            //foreach (var f in reasoner.GetAllSubFrames("Предмет"))
            //{
            //    Console.WriteLine(f);
            //}

            //Console.WriteLine();


            //foreach (var f in reasoner.GetAllSubFrames("Правило"))
            //{
            //    Console.WriteLine(f);
            //}

            //Console.WriteLine();

            Frame fieldFrame = GetFieldFrame();

            while (true)
            {
                var slot = reasoner.GetNextValueToAsk();
                if (slot == null)
                    break;
                Console.WriteLine(slot + "?");
                Console.WriteLine(String.Join(", ", slot.Domain.Values.Select(x => x.Text)));
                var valId = int.Parse(Console.ReadLine());
                reasoner.SetAnswer(slot.Domain.Values[valId]);
            }

            Console.WriteLine("Answer: " + reasoner.GetAnswer());

            Console.ReadKey();

        }
    }
}
