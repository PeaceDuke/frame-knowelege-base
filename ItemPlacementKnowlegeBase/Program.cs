using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemPlacementKnowlegeBase.Models;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using ItemPlacementKnowlegeBase.Services;

namespace ItemPlacementKnowlegeBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(new Main_form());

            var KWBase = new KnowlegeBase();
            var sceneFrame = KWBase.AddFrame("Сцена", true);
            var surfaceFrame = KWBase.AddFrame("Поверхность", true);
            var cellFrame = KWBase.AddFrame("Клетка", true);
            var objectFrame = KWBase.AddFrame("Объект", true);
            sceneFrame.AddSlot("Cтена 1", surfaceFrame, typeof(Frame));
            sceneFrame.AddSlot("Cтена 2", surfaceFrame, typeof(Frame));
            sceneFrame.AddSlot("Cтена 3", surfaceFrame, typeof(Frame));
            sceneFrame.AddSlot("Cтена 4", surfaceFrame, typeof(Frame));
            sceneFrame.AddSlot("Пол", surfaceFrame, typeof(Frame));
            sceneFrame.AddSlot("Потолок", surfaceFrame, typeof(Frame));
            for(var i = 0; i < 8; i++)
            {
                for(var j = 0; j < 8; j++)
                {
                    surfaceFrame.AddSlot("K" + i.ToString() + j.ToString(), cellFrame, typeof(Frame));
                }
            }
            cellFrame.AddSlot("X", typeof(int));
            cellFrame.AddSlot("Y", typeof(int));
            cellFrame.AddSlot("Объект", objectFrame, typeof(Frame));
            objectFrame.AddSlot("W", typeof(int));
            objectFrame.AddSlot("H", typeof(int));
            objectFrame.AddSlot("Ссылка", typeof(string));

            var wraper = new FrameWraper();
            var result = wraper.StartInference(sceneFrame);

            KWBSerializer.SerilizeToFile(result, "result.json");


            var toolboxFrame = KWBase.AddFrame("Тулбокс", true);
            var tableFrame = objectFrame.Determinate("Стол");
            KWBase.AddFrame(tableFrame);
            var chairFrame = objectFrame.Determinate("Стул");
            KWBase.AddFrame(chairFrame);
            toolboxFrame.AddSlot("Пустой объект", objectFrame, typeof(Frame));

            var floorToolboxFrame = wraper.StartInference(toolboxFrame);
            floorToolboxFrame.Name = "Тулбокс для пола";
            floorToolboxFrame.AddSlot("Предмет 1", chairFrame, typeof(Frame));
            floorToolboxFrame.AddSlot("Предмет 2", tableFrame, typeof(Frame));

            KWBSerializer.SerilizeToFile(floorToolboxFrame, "toolbox.json");


            var eventFrames = new List<Frame>();

            var eventFrame = KWBase.AddFrame("Ситуация", true);
            eventFrame.AddSlot("Агент",typeof(string));
            eventFrame.AddSlot("Реципиент", typeof(string));
            eventFrame.AddSlot("Процедура", typeof(string));
            var tryPlaceEventFrame = eventFrame.Determinate("Ситуация попытки размещения");
            var placementEventFrame = eventFrame.Determinate("Ситуация попытки размещения");
            var errorEventFrame = eventFrame.Determinate("Ситуация попытки размещения");
            tryPlaceEventFrame.GetSlot("Агент").SetValue("Объект");
            tryPlaceEventFrame.GetSlot("Реципиент").SetValue("Клетка");
            tryPlaceEventFrame.GetSlot("Процедура").SetValue("ChecPlace()");
            tryPlaceEventFrame.AddSlot("Следующий", placementEventFrame, typeof(Frame));
            tryPlaceEventFrame.AddSlot("Ошибка", errorEventFrame, typeof(Frame));
            placementEventFrame.GetSlot("Агент").SetValue("Объект");
            placementEventFrame.GetSlot("Реципиент").SetValue("Клетка");
            placementEventFrame.GetSlot("Процедура").SetValue("Place()");
            errorEventFrame.GetSlot("Агент").SetValue("Объект");
            errorEventFrame.GetSlot("Реципиент").SetValue("Клетка");
            errorEventFrame.GetSlot("Процедура").SetValue("ThrowPlacementError()");
            eventFrames.Add(tryPlaceEventFrame);
            eventFrames.Add(placementEventFrame);
            eventFrames.Add(errorEventFrame);

            KWBSerializer.SerilizeToFile(eventFrames, "events.json");
            KWBSerializer.SerilizeToFile(KWBase, "entireKWB.json");

            KWBSerializer.DeserilizeFromFile("result.json");

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
