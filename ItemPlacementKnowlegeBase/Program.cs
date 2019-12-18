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
            //Application.Run(new Main_form());

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
            surfaceFrame.AddSlot("K1", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K2", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K3", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K4", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K5", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K6", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K7", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K8", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K11", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K12", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K13", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K14", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K15", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K16", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K17", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K18", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K21", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K22", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K23", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K24", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K25", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K26", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K27", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K28", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K31", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K32", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K33", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K34", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K35", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K36", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K37", cellFrame, typeof(Frame));
            surfaceFrame.AddSlot("K38", cellFrame, typeof(Frame));
            cellFrame.AddSlot("X", typeof(int));
            cellFrame.AddSlot("Y", typeof(int));
            cellFrame.AddSlot("Объект", objectFrame, typeof(Frame));
            objectFrame.AddSlot("W", typeof(int));
            objectFrame.AddSlot("H", typeof(int));
            objectFrame.AddSlot("Ссылка", typeof(string));
            var emptyObjectFrame = objectFrame.Determinate("Пустой объект");
            emptyObjectFrame.GetSlot("W").SetValue(1);
            emptyObjectFrame.GetSlot("H").SetValue(1);
            emptyObjectFrame.GetSlot("Ссылка").SetValue("/obj/empty.obj");
            KWBase.AddFrame(emptyObjectFrame);
            var flourObjectFrame = objectFrame.Clone("Напольный объект");
            KWBase.AddFrame(flourObjectFrame);
            var tableFrame= flourObjectFrame.Determinate("Стол");
            tableFrame.GetSlot("W").SetValue(1);
            tableFrame.GetSlot("H").SetValue(1);
            tableFrame.GetSlot("Ссылка").SetValue("/obj/table.obj");
            KWBase.AddFrame(tableFrame);

            var wraper = new InferenceWraper(KWBase);
            var result = wraper.StartInference(sceneFrame);

            var serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter("result.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, result);
            }
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
