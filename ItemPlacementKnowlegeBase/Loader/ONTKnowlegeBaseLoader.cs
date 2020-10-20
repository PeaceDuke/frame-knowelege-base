using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemPlacementKnowlegeBase.Models;
using System.Text.Json;
using System.IO;

namespace ItemPlacementKnowlegeBase.Loader
{
    class ONTKnowlegeBaseLoader
    {
        private const string NODES = "nodes";
        private const string ATTRIBUTES = "attributes";
        public static KnowlegeBase Parce(string filename)
        {
            string text = File.ReadAllText(filename);
            KnowlegeBase knowlegeBase = null;
            using(JsonDocument document = JsonDocument.Parse(text))
            {
                JsonElement root = document.RootElement;
                JsonElement nodes;
                if(!root.TryGetProperty(NODES, out nodes))
                {
                    return null;
                }
                foreach(var node in nodes.EnumerateArray())
                {
                    JsonElement attributes;
                    if(!node.TryGetProperty(ATTRIBUTES, out attributes))
                    {
                        return null;
                    }
                }
            }
            return knowlegeBase;
        }
    }
}
