using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemPlacementKnowlegeBase.Models;

namespace ItemPlacementKnowlegeBase.Services
{
    public class InferenceWraper
    {
        public KnowlegeBase KnowlegeBase { get; set; }

        public InferenceWraper(KnowlegeBase knowlegeBase)
        {
            KnowlegeBase = knowlegeBase;
        }

        public Frame StartInference(Frame targetFrame)
        {
            if (!targetFrame.IsBase)
                throw new ArgumentException("На вход принимаются только базовые фреймы");
            return DeterminateFrame(targetFrame); 
            
        }

        private Frame DeterminateFrame(Frame frame)
        {
            var detFrame = frame.Determinate(string.Format("{0}_{1}", frame.Name, RandomString(20)));
            KnowlegeBase.AddFrame(detFrame);
            foreach (var slot in detFrame.Slots)
            {
                if(slot.ValueType == typeof(Frame))
                {
                    var slotFrame = (slot.Value as Frame);
                    if (slotFrame.IsBase)
                    {
                        slot.SetValue(DeterminateFrame(slotFrame));
                    }
                }
            }
            fillFrame(detFrame);
            return detFrame;
        }

        private int surfaceXIndex = 0;
        private int surfaceYIndex = 0;

        private void fillFrame(Frame frame)
        {
            switch(frame.Name)
            {
                case "Поверхность": { surfaceXIndex = surfaceYIndex = 0;  break; }
                case "Клетка": 
                    { 
                        frame.Slots.Find(x => x.Name == "X").SetValue(surfaceXIndex++);
                        frame.Slots.Find(x => x.Name == "Y").SetValue(surfaceYIndex++);
                        break; 
                    }
                case "Объект":
                    {
                        frame.Slots.Find(x => x.Name == "W").SetValue(1);
                        frame.Slots.Find(x => x.Name == "H").SetValue(1);
                        frame.Slots.Find(x => x.Name == "Ссылка").SetValue("empty");
                        break;
                    }
            }
        }



        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
