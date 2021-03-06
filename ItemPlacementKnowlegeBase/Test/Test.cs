﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Gui;
using ItemPlacementKnowlegeBase.Loader;
using ItemPlacementKnowlegeBase.Models;
using ItemPlacementKnowlegeBase.Services;

namespace ItemPlacementKnowlegeBase.Test
{
    class Test
    {
        public Test(KnowlegeBase knowlegeBase)
        {
            this.knowlegeBase = knowlegeBase;
            reasoner = new Reasoner(knowlegeBase);
        }
        private KnowlegeBase knowlegeBase = null;

        private Reasoner reasoner;
        //public KnowlegeBase TestFrameModel
        //{
        //    set
        //    {
        //        knowlegeBase = value;
        //        reasoner = new Reasoner(knowlegeBase);
        //    }
        //    get
        //    {
        //        if (!String.IsNullOrWhiteSpace(filename))
        //        {
        //            return ONTKnowlegeBaseLoader.Parce(filename);
        //        }
        //        else
        //        {

        //            var frames = new[]
        //            {
        //            new Frame("Предмет"),                       //0
        //            new Frame("Напольный предмет"),             //1
        //            new Frame("Стол"),                          //2
        //            new Frame("Стул"),                          //3
        //            new Frame("Картина"),                       //4
        //            new Frame("Поле"),                          //5
        //            new Frame("Клетка"),                        //6
        //            new Frame("Правило"),                       //7
        //            new Frame("Событие"),                       //8
        //            new Frame("Отношение стола и стула"),       //9
        //            new Frame("Отношение горшка и стола"),      //10
        //            new Frame("Отношение стола и стула 2"),     //11
        //            new Frame("Пустота"),                       //12
        //            new Frame("Клетки_корень"),                       //13
        //        };

        //            var frameModel = new KnowlegeBase();

        //            frames[2].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][0]));
        //            frames[2].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][0], false, true));
        //            frames[2].Slots.Add(new TextSlot("Изображение", "table"));
        //            frames[2].Parent = frames[0];

        //            frames[3].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][1]));
        //            frames[3].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][1], false, true));
        //            frames[3].Slots.Add(new TextSlot("Изображение", "chair"));
        //            frames[3].Parent = frames[0];

        //            frames[4].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][2]));
        //            frames[4].Slots.Add(new DomainSlot("Предмет", domains[2], domains[2][2], false, true));
        //            frames[4].Slots.Add(new TextSlot("Изображение", "picture"));
        //            frames[4].Parent = frames[0];

        //            frames[5].Slots.Add(new TextSlot("Размер", "5"));
        //            frames[5].Slots.Add(new TextSlot("Размер клетки", "64"));

        //            frames[8].Slots.Add(new FrameSlot("Правило", frames[7], false, true));

        //            frames[9].Slots.Add(new FrameSlot("Объект", frames[3], false, true));
        //            frames[9].Slots.Add(new FrameSlot("Субъект", frames[2], false, true));
        //            frames[9].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][1], false, true));
        //            frames[9].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][0], false, true));
        //            frames[9].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][2], false, true));
        //            frames[9].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][0], false, true));
        //            frames[9].Slots.Add(new TextSlot("Объяснение", "Нельзя ставить стул справа стола"));
        //            frames[9].Parent = frames[7];

        //            frames[10].Slots.Add(new FrameSlot("Объект", frames[2], false, true));
        //            frames[10].Slots.Add(new FrameSlot("Субъект", frames[4], false, true));
        //            frames[10].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][0], false, true));
        //            frames[10].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][2], false, true));
        //            frames[10].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][3], false, true));
        //            frames[10].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][1], false, true));
        //            frames[10].Slots.Add(new TextSlot("Объяснение", "Нельзя ставить картину рядом со столом"));
        //            frames[10].Parent = frames[7];

        //            frames[11].Slots.Add(new FrameSlot("Объект", frames[3], false, true));
        //            frames[11].Slots.Add(new FrameSlot("Субъект", frames[2], false, true));
        //            frames[11].Slots.Add(new DomainSlot("Объект", domains[2], domains[2][1], false, true));
        //            frames[11].Slots.Add(new DomainSlot("Субъект", domains[2], domains[2][0], false, true));
        //            frames[11].Slots.Add(new DomainSlot("Расположение", domains[3], domains[3][3], false, true));
        //            frames[11].Slots.Add(new DomainSlot("Тип правила", domains[1], domains[1][0], false, true));
        //            frames[11].Slots.Add(new TextSlot("Объяснение", "Нельзя ставить стул слева стола"));
        //            frames[11].Parent = frames[7];

        //            frames[12].Parent = frames[0];

        //            frames[13].Slots.Add(new FrameSlot("Клетка", frames[6], false, true));

        //            foreach (var domain in domains)
        //            {
        //                frameModel.Domains.Add(domain);
        //            }

        //            foreach (var frame in frames)
        //            {
        //                frameModel.Frames.Add(frame);
        //            }

        //            if (knowlegeBase == null)
        //            {
        //                knowlegeBase = ONTKnowlegeBaseLoader.Parce("D:\\Projects\\frame-knowelege-base\\Ontolis\\itemPlacement.ont");
        //                testFrameModel = XMLKnowlegeBaseLoader.Parce(XMLKnowlegeBaseLoader.TEST_KNOWLEDGE_BASE);
        //            }

        //            return frameModel;
        //        }
        //    }
        //}


        private bool inited = false;

        public Frame GetFieldFrame()
        {
            reasoner.Clear();
            Frame fieldFrame = reasoner.GetFrame("Поле");
            if(!inited)
            {
                if (fieldFrame.Slots.Where(x => x is FrameSlot && !x.IsSystemSlot).Any())
                {
                    inited = true;
                    return fieldFrame;
                }
                Frame cellFrame = reasoner.GetFrame("Клетка");
                Frame emptyItem = reasoner.GetFrame("Пустота");
                List<Frame> cellFrames = new List<Frame>();
                Domain domain = new Domain("Числа");
                int max = int.Parse(fieldFrame["Размер"].ValueAsString);
                for (int i = 0; i < max; i++)
                    domain.Values.Add(new DomainValue(i.ToString()));
                reasoner.AddDomain(domain);
                for (int x = 0; x < max; x++)
                    for (int y = 0; y < max; y++)
                    {
                        string name = "Клетка" + x + ":" + y;
                        Frame newCellFrame = new Frame(name);
                        newCellFrame.Slots.Add(new DomainSlot("X", domain, domain[x], false, true));
                        newCellFrame.Slots.Add(new DomainSlot("Y", domain, domain[y], false, true));
                        Frame leftFrame = x > 0 ? cellFrames[(x - 1) * max + y] : null;
                        Frame upFrame = y > 0 ? cellFrames[x * max + y - 1] : null;
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

        public List<Frame> GetItemFrameList()
        {
            reasoner.Clear();
            return reasoner.GetAllSubFrames("Предмет");
        }

        public List<Frame> GetRuleFrameList()
        {
            reasoner.Clear();
            return reasoner.GetAllSubFrames("Правило");
        }

        public string CheckRule(int x, int y, string itemName)
        {
            reasoner.Clear();
            DomainValue answer = null;

            Frame fieldFrame = reasoner.GetFrame("Клетки_корень");

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

            Frame eventFrame = reasoner.GetFrame("Событие");

            reasoner.Clear();
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
                        if (cellFrame["Предмет"].ValueAsString == "Пустота")
                            answer = null;
                        else
                            answer = answerSlot.Domain[cellFrame["Предмет"].ValueAsString];
                        break;
                    case "Расположение":
                        answer = answerSlot.Domain["Вместо"];
                        break;
                    case "Тип правила":
                        answer = answerSlot.Domain["Запрещающее"];
                        break;

                }
                reasoner.SetAnswer(answer);
            }
            bool anyItemNear = false;
            if (!reasoner.AnswerFound)
                foreach (Slot slot in cellFrame.Slots)
                {
                    if (slot.IsSystemSlot || slot.Name == "Предмет" || !(slot is FrameSlot))
                        continue;
                    reasoner.Clear();
                    Frame nearCellFrame = ((FrameSlot)slot).Frame;
                    if (nearCellFrame["Предмет"].ValueAsString == "Пустота")
                        continue;
                    anyItemNear = true;
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
                            case "Тип правила":
                                answer = answerSlot.Domain["Запрещающее"];
                                break;

                        }
                        reasoner.SetAnswer(answer);
                    }
                    if (reasoner.AnswerFound)
                        break;
                    reasoner.Clear();
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
                            case "Тип правила":
                                answer = answerSlot.Domain["Разрешающее"];
                                break;

                        }
                        reasoner.SetAnswer(answer);
                    }
                    if (reasoner.AnswerFound)
                        break;
                }
            if (reasoner.AnswerFound)
            {
                Frame answerFrame = reasoner.GetAnswer();
                if(answerFrame["Тип правила"].ValueAsString == "Разрешающее")
                    return "allow";
                else
                    return answerFrame["Объяснение"].ValueAsString;
            }
            else if(!anyItemNear) 
                    if (cellFrame["Предмет"].ValueAsString == "Пустота")
                        return "allow";
            return null;
        }

        public void PutItem(int x, int y, string itemName)
        {
            reasoner.Clear();
            DomainValue answer = null;
            Frame fieldFrame = reasoner.GetFrame("Клетки_корень");

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

        public void RemoveItem(int x, int y)
        {
            reasoner.Clear();
            DomainValue answer = null;
            Frame fieldFrame = reasoner.GetFrame("Клетки_корень");

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

        public List<string> GetBindedFrames()
        {
            List<string> bidedList = new List<string>();
            foreach(var bind in reasoner.GetBindedFrames())
            {
                bidedList.Add("Фрейм: "+ bind.Key.Name + " Сработал: " + bind.Value);
            }
            return bidedList;
        }
        

        public List<Frame> getReasoning()
        {
            return reasoner.GetInferringPath();
        }

        public KnowlegeBase GetModel()
        {
            return reasoner.GetModel();
        }


        public void Do()
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

            Console.ReadKey();

        }

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
                new DomainValue("Вместо"),
            }),
        };
    }

}
