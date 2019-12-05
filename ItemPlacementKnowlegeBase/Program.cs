using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemPlacementKnowlegeBase.Models;
//using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace ItemPlacementKnowlegeBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(new Main_form());
        }
    }
}

/*
    var KWBase = new KnowlegeBase();
    var emptyObjectFrame = new Frame("Пустота");
    var baseCellFrame = new Frame("Клетка");
    var surfaceFrame = new Frame("Поверхность");
    var sceneFrame = new Frame("Сцена");
    emptyObjectFrame.AddSlot("длинна", 1, typeof(Int32));
    emptyObjectFrame.AddSlot("ширина", 1, typeof(Int32));
    baseCellFrame.AddSlot("x координата", 0, typeof(Int32));
    baseCellFrame.AddSlot("y координата", 0, typeof(Int32));
    baseCellFrame.AddSlot("объект", emptyObjectFrame, typeof(Frame));
    for(int i = 0; i < 20; i++)
    {
        for(int j = 0; j < 20; j++)
        {
            var cellFrame = (Frame)baseCellFrame.Clone();
            cellFrame.SetSlotValue("x координата", i);
            cellFrame.SetSlotValue("y координата", j);
            surfaceFrame.AddSlot(string.Format("{0}:{1}", i, j), cellFrame, typeof(Frame));
        }
    }
    sceneFrame.AddSlot("пол", (Frame)surfaceFrame.Clone(), typeof(Frame));
    sceneFrame.AddSlot("стена з", (Frame)surfaceFrame.Clone(), typeof(Frame));
    sceneFrame.AddSlot("стена в", (Frame)surfaceFrame.Clone(), typeof(Frame));
    sceneFrame.AddSlot("стена с", (Frame)surfaceFrame.Clone(), typeof(Frame));
    sceneFrame.AddSlot("стена ю", (Frame)surfaceFrame.Clone(), typeof(Frame));
    sceneFrame.AddSlot("потолок", (Frame)surfaceFrame.Clone(), typeof(Frame));
*/
