using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemPlacementKnowlegeBase.Models;

namespace ItemPlacementKnowlegeBase.Services
{
    static class KWBSerializer
    {
        private static JsonSerializer serializer = new JsonSerializer();

        public static void SerilizeToFile(Object obj, String fileName)
        {

            using (StreamWriter sw = new StreamWriter(fileName))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static Object DeserilizeFromFile(String fileName)
        {

            using (StreamReader rw = new StreamReader(fileName))
            using (JsonReader reader = new JsonTextReader(rw))
            {
                Object obj = serializer.Deserialize(reader, typeof(Frame));
                return obj;
            }
        }

    }
}
