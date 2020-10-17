using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Gui;
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
                new DomainValue("Картина"),
            }),
            new Domain("Расположение", new[]
            {
                new DomainValue("Выше"),
                new DomainValue("Ниже"),
                new DomainValue("Слева"),
                new DomainValue("Справа"),
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
                    new Frame("Картина"),
                    new Frame("Поле"),
                    new Frame("Клетка"),
                    new Frame("Правило"),
                    new Frame("Событие"),
                    new Frame("Отношение стола и стула"),
                    new Frame("Отношение горшка и стола"),
                    new Frame("Отношение стола и стула 2"),
                    new Frame("Пустота"),
                    new Frame("Костыль"),
                };

                var frameModel = new KnowlegeBase();

                frames[1].Slots.Add(new DomainSlot("Место размещения", domains[0], domains[0][0]));
                frames[1].Parent = frames[0];

                frames[2].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][0]));
                frames[2].Slots.Add(new TextSlot("Цвет", "#000000"));
                frames[2].Slots.Add(new TextSlot("Изображение", "table"));
                frames[2].Parent = frames[1];

                frames[3].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][1]));
                frames[3].Slots.Add(new TextSlot("Цвет", "#FFFFFF"));
                frames[3].Slots.Add(new TextSlot("Изображение", "chair"));
                frames[3].Parent = frames[1];

                frames[4].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][2]));
                frames[4].Slots.Add(new TextSlot("Цвет", "#111111"));
                frames[4].Slots.Add(new TextSlot("Изображение", "picture"));
                frames[4].Parent = frames[1];

                frames[5].Slots.Add(new TextSlot("Высота", "10"));
                frames[5].Slots.Add(new TextSlot("Ширина", "10"));

                frames[8].Slots.Add(new FrameSlot("Правило", frames[7], false, true));

                frames[9].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][1], false, true));
                frames[9].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][0], false, true));
                frames[9].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][2], false, true));
                frames[9].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][0]));
                frames[11].Slots.Add(new TextSlot("Объяснение", "Нельзя ставить стул справа стола"));
                frames[9].Parent = frames[7];

                frames[10].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][0], false, true));
                frames[10].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][2], false, true));
                frames[10].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][4], false, true));
                frames[10].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][1]));
                frames[10].Slots.Add(new TextSlot("Объяснение", "Нельзя ставить горшок рядом со столом"));
                frames[10].Parent = frames[7];

                frames[11].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][1], false, true));
                frames[11].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][0], false, true));
                frames[11].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][3], false, true));
                frames[11].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][0]));
                frames[11].Slots.Add(new TextSlot("Объяснение", "Нельзя ставить стул слева стола"));
                frames[11].Parent = frames[7];

                frames[12].Parent = frames[0];

                frames[13].Slots.Add(new FrameSlot("Клетка", frames[6], false, true));

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

        private static bool inited = false;

        public static Frame GetFieldFrame()
        {
            reasoner.Clear();
            Frame fieldFrame = reasoner.GetFrame("Поле");
            if(!inited)
            {
                Frame cellFrame = reasoner.GetFrame("Клетка");
                Frame emptyItem = reasoner.GetFrame("Пустота");
                int h = int.Parse(fieldFrame["Высота"].ValueAsString);
                int w = int.Parse(fieldFrame["Ширина"].ValueAsString);
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
                        Frame leftFrame = i > 0 ? cellFrames[(i - 1) * h + j] : null;
                        Frame upFrame = j > 0 ? cellFrames[i * h + j - 1] : null;
                        if (leftFrame != null)
                        {
                            newCellFrame.Slots.Add(new FrameSlot("Слева", leftFrame));
                            leftFrame.Slots.Add(new FrameSlot("Справа", newCellFrame));
                        }
                        if (upFrame != null)
                        {
                            newCellFrame.Slots.Add(new FrameSlot("Выше", upFrame));
                            upFrame.Slots.Add(new FrameSlot("Ниже", newCellFrame));
                        }
                        newCellFrame.Slots.Add(new FrameSlot("Предмет", emptyItem));
                        newCellFrame.Parent = cellFrame;
                        fieldFrame.Slots.Add(new FrameSlot(name, newCellFrame));
                        cellFrames.Add(newCellFrame);
                        reasoner.AddFrame(newCellFrame);
                    }
                inited = true;
            }
            return fieldFrame;
        }

        public static List<Frame> GetItemFrameList()
        {
            reasoner.Clear();
            return reasoner.GetAllSubFrames("Предмет");
        }

        public static string CheckRule(int x, int y, string itemName)
        {
            reasoner.Clear();
            DomainValue answer = null;

            while (true)
            {
                var answerSlot = reasoner.GetNextValueToAsk();
                if (answerSlot == null)
                    break;
                switch (answerSlot.Name)
                {
                    case "X":
                        answer = answerSlot.Domain[x];
                        break;
                    case "Y":
                        answer = answerSlot.Domain[y];
                        break;

                }
                if (answer == null)
                    throw new Exception("Ответ не найден");
                reasoner.SetAnswer(answer);
            }

            Frame cellFrame = reasoner.GetAnswer();

            if (cellFrame == null)
                throw new Exception("Клетка не найдена");

            Frame eventFrame = reasoner.GetFrame("Событие");

            foreach (Slot slot in cellFrame.Slots)
            {
                if (slot.IsSystemSlot || slot.Name == "Предмет" || !(slot is FrameSlot))
                    continue;
                reasoner.Clear();
                Frame nearCellFrame = ((FrameSlot)slot).Frame;
                while (true)
                {
                    var answerSlot = reasoner.test(eventFrame);
                    if (answerSlot == null)
                        break;
                    switch (answerSlot.Name)
                    {
                        case "Объект":
                            answer = answerSlot.Domain[itemName];
                            break;
                        case "Субъект":
                            if (nearCellFrame["Предмет"].ValueAsString == "Пустота")
                                answer = null;
                            else
                                answer = answerSlot.Domain[nearCellFrame["Предмет"].ValueAsString];
                            break;
                        case "Расположение":
                            answer = answerSlot.Domain[slot.Name];
                            break;

                    }
                    reasoner.SetAnswer(answer);
                }
                if (reasoner.AnswerFound)
                    return reasoner.GetAnswer()["Объяснение"].ValueAsString;
            }
            return null;
        }

        public static void PutItem(int x, int y, string itemName)
        {
            reasoner.Clear();
            DomainValue answer = null;
            Frame fieldFrame = reasoner.GetFrame("Костыль");

            while (true)
            {
                var answerSlot = reasoner.test(fieldFrame);
                if (answerSlot == null)
                    break;
                switch (answerSlot.Name)
                {
                    case "X":
                        answer = answerSlot.Domain[x];
                        break;
                    case "Y":
                        answer = answerSlot.Domain[y];
                        break;

                }
                if (answer == null)
                    throw new Exception("Ответ не найден");
                reasoner.SetAnswer(answer);
            }

            Frame cellFrame = reasoner.GetAnswer();

            if (cellFrame == null)
                throw new Exception("Клетка не найдена");

            FrameSlot cellItemslot = (FrameSlot)cellFrame["Предмет"];

            Frame item = reasoner.GetFrame(itemName);
            if (item == null)
                throw new Exception("Предмет не найден");
            cellItemslot.Frame = item;
        }

        public static void RemoveItem(int x, int y)
        {
            reasoner.Clear();
            DomainValue answer = null;
            Frame fieldFrame = reasoner.GetFrame("Костыль");

            while (true)
            {
                var answerSlot = reasoner.test(fieldFrame);
                if (answerSlot == null)
                    break;
                switch (answerSlot.Name)
                {
                    case "X":
                        answer = answerSlot.Domain[x];
                        break;
                    case "Y":
                        answer = answerSlot.Domain[y];
                        break;

                }
                if (answer == null)
                    throw new Exception("Ответ не найден");
                reasoner.SetAnswer(answer);
            }

            Frame cellFrame = reasoner.GetAnswer();

            if (cellFrame == null)
                throw new Exception("Клетка не найдена");

            FrameSlot cellItemslot = (FrameSlot)cellFrame["Предмет"];

            Frame emptyItem = reasoner.GetFrame("Пустота");
            cellItemslot.Frame = emptyItem;
        }

        public static List<Frame> getReasoning()
        {
            return reasoner.GetInferringPath();
        }


        public static void Do()
        {

            Frame eventFrame = reasoner.GetFrame("Событие");

            reasoner.Clear();
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

            Console.WriteLine("Answer: " + reasoner.GetAnswer());
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

            //Frame fieldFrame = GetFieldFrame();

            //while (true)
            //{
            //    var slot = reasoner.GetNextValueToAsk();
            //    if (slot == null)
            //        break;
            //    Console.WriteLine(slot + "?");
            //    Console.WriteLine(String.Join(", ", slot.Domain.Values.Select(x => x.Text)));
            //    var valId = int.Parse(Console.ReadLine());
            //    reasoner.SetAnswer(slot.Domain.Values[valId]);
            //}

            //Console.WriteLine("Answer: " + reasoner.GetAnswer());

            //Console.ReadKey();

        }
    }
}
