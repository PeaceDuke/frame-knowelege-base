using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemPlacementKnowlegeBase.Models;

namespace ItemPlacementKnowlegeBase.Services
{
    public class FrameWraper
    {

        //    public Frame StartInference(Frame targetFrame)
        //    {
        //        if (!targetFrame.IsBase)
        //            throw new ArgumentException("На вход принимаются только базовые фреймы");
        //        return DeterminateFrame(targetFrame); 

        //    }

        //    private Frame DeterminateFrame(Frame frame)
        //    {
        //        var detFrame = frame.Determinate(string.Format("{0}_{1}", frame.Name, RandomString(20)));
        //        foreach (var slot in detFrame.Slots)
        //        {
        //            if(slot.ValueType == typeof(Frame))
        //            {
        //                var slotFrame = (slot.Value as Frame);
        //                if (slotFrame.IsBase)
        //                {
        //                    slot.SetValue(DeterminateFrame(slotFrame));
        //                }
        //            }
        //        }
        //        fillFrame(frame.Name, detFrame);
        //        return detFrame;
        //    }

        //    private int surfaceIndex = 0;

        //    private void fillFrame(string name, Frame frame)
        //    {
        //        switch(name)
        //        {
        //            case "Поверхность": { 
        //                    surfaceIndex = 0;
        //                    break; 
        //                }
        //            case "Клетка": 
        //                { 
        //                    frame.GetSlot("X").SetValue(surfaceIndex / 8);
        //                    frame.GetSlot("Y").SetValue(surfaceIndex % 8);
        //                    surfaceIndex++;
        //                    break; 
        //                }
        //            case "Объект":
        //                {
        //                    frame.GetSlot("W").SetValue(1);
        //                    frame.GetSlot("H").SetValue(1);
        //                    frame.GetSlot("Ссылка").SetValue("empty");
        //                    break;
        //                }
        //        }
        //    }



        //    private static Random random = new Random();
        //    public static string RandomString(int length)
        //    {
        //        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //        return new string(Enumerable.Repeat(chars, length)
        //          .Select(s => s[random.Next(s.Length)]).ToArray());
        //    }

        //}
    }
}
