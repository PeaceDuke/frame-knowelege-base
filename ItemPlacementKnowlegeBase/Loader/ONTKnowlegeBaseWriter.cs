using ItemPlacementKnowlegeBase.Loader.Model;
using ItemPlacementKnowlegeBase.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Loader
{
    class ONTKnowlegeBaseWriter
    {
        private const string NODES = "nodes";
        private const string RELATIONS = "relations";
        private const string ATTRIBUTES = "attributes";
        private const string NAME = "name";
        private const string ID = "id";
        private const string TYPE = "type";
        private const string DOMAIN = "domain";
        private const string VALUE = "value";
        private const string IS_REQUESTABLE = "isRequestable";
        private const string DEST_NODE_ID = "destination_node_id";
        private const string SOURCE_NODE_ID = "source_node_id";

        private static int id;
        private static Random random = new Random();
        public static void Write(KnowlegeBase knowlegeBase, string filename)
        {
            id = 0;
            Frame[] frames = knowlegeBase.Frames.ToArray<Frame>();
            Domain[] domains = knowlegeBase.Domains.ToArray<Domain>();

            List<Node> nodes = new List<Node>();
            List<Relation> relations = new List<Relation>();

            Node domainNode = new Node("Домен", ++id);
            nodes.Add(domainNode);
            nodes.Add(new Node("Фрейм", ++id));
            foreach(Domain domain in domains)
            {
                if (domain.Name == "Фрейм")
                    continue;
                Node node = new Node(domain.Name, ++id);
                List<string> values = new List<string>();
                foreach(var value in domain.Values)
                {
                    values.Add(value.Text);
                }
                node.attributes["list"] = new ArrayAttribute(values.ToArray());
                nodes.Add(node);
                relations.Add(new Relation(domainNode, node, "is_a", ++id));
            }

            foreach(Frame frame in frames)
            {
                Node node = new Node(frame.Name, ++id);
                foreach(var slot in frame.Slots)
                {
                    if (slot.IsSystemSlot)
                        continue;
                    if(slot is DomainSlot)
                    {
                        DomainSlot domainSlot = slot as DomainSlot;
                        DomainAttribute attribute = new DomainAttribute(
                            domainSlot.Domain.Name,
                            domainSlot.Value.Text,
                            slot.IsRequestable
                            );
                        node.attributes[slot.Name] = attribute;
                    }
                    if(slot is TextSlot)
                    {
                        TextSlot textSlot = slot as TextSlot;
                        TextAttribute attribute = new TextAttribute(textSlot.ValueAsString);
                        node.attributes[slot.Name] = attribute;
                    }
                    if(slot is FrameSlot)
                    {
                        FrameSlot frameSlot = slot as FrameSlot;
                        FrameAttribute attribute = new FrameAttribute(frameSlot.ValueAsString, slot.IsRequestable);
                        node.attributes[slot.Name] = attribute;
                    }
                }
                nodes.Add(node);

            }

            foreach(Frame frame in frames)
            {
                Node node = nodes.Find(_n => _n.name == frame.Name);
                if(frame.Parent != null)
                {
                    Node parentNode = nodes.Find(_n => _n.name == frame.Parent.Name);
                    relations.Add(new Relation(parentNode, node, "is_a", ++id));
                } 
                else
                {
                    Node frameNode = nodes.Find(_n => _n.name == "Фрейм");
                    relations.Add(new Relation(frameNode, node, "is_a", ++id));
                }
                foreach (var slot in frame.Slots)
                {
                    if(slot is FrameSlot && !slot.IsSystemSlot)
                    {
                        FrameSlot frameSlot = slot as FrameSlot;
                        Node subFrameNode = nodes.Find(_n => _n.name == frameSlot.Frame.Name);
                        relations.Add(new Relation(subFrameNode, node, "sub_frame", ++id));
                    }
                }
            }


            using (StreamWriter stream = new StreamWriter(filename)) 
            {
                JsonWriterOptions options = new JsonWriterOptions();
                options.Indented = true;
                options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                Dictionary<int, Vector2> positions = new Dictionary<int, Vector2>();
                positions = GeneratePosition(nodes, relations);
                using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(stream.BaseStream, options))
                {
                    jsonWriter.WriteStartObject();
                    jsonWriter.WriteString("last_id", id.ToString());

                    jsonWriter.WritePropertyName("namespaces");
                    jsonWriter.WriteStartObject();
                    jsonWriter.WriteString("ontolis-avis", "http://knova.ru/ontolis-avis");
                    jsonWriter.WriteString("owl", "http://www.w3.org/2002/07/owl");
                    jsonWriter.WriteString("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns");
                    jsonWriter.WriteString("rdfs", "http://www.w3.org/2000/01/rdf-schema");
                    jsonWriter.WriteString("xsd", "http://www.w3.org/2001/XMLSchema");
                    jsonWriter.WriteEndObject();

                    jsonWriter.WritePropertyName("nodes");
                    jsonWriter.WriteStartArray();
                    foreach (Node node in nodes)
                    {
                        jsonWriter.WriteStartObject();

                        jsonWriter.WritePropertyName("attributes");
                        jsonWriter.WriteStartObject();
                        foreach (var attrib in node.attributes)
                        {
                            WriteAttribute(jsonWriter, attrib);
                        }
                        jsonWriter.WriteEndObject();
                        jsonWriter.WriteString("id", node.id.ToString());
                        jsonWriter.WriteString("name", node.name);
                        jsonWriter.WriteString("namespace", "ontolis-avis");

                        Vector2 pos = positions[node.id];
                        jsonWriter.WriteNumber("position_x", pos.X);
                        jsonWriter.WriteNumber("position_y", pos.Y);
                        positions[node.id] = pos;
                        jsonWriter.WriteEndObject();
                    }
                    jsonWriter.WriteEndArray();

                    jsonWriter.WritePropertyName("relations");
                    jsonWriter.WriteStartArray();
                    foreach (Relation relation in relations)
                    {
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("attributes");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WriteEndObject();
                        jsonWriter.WriteString("destination_node_id", relation.destination.id.ToString());
                        jsonWriter.WriteString("id", relation.id.ToString());
                        jsonWriter.WriteString("name", relation.name);
                        jsonWriter.WriteString("namespace", "ontolis-avis");
                        jsonWriter.WriteString("source_node_id", relation.source.id.ToString());
                        jsonWriter.WriteEndObject();
                    }
                    jsonWriter.WriteEndArray();

                    jsonWriter.WriteString("visualize_ont_path", "");
                    jsonWriter.WriteEndObject();
                }
            };
        }

        private static Dictionary<int, Vector2> GeneratePosition(List<Node> nodes, List<Relation> relations)
        {
            Dictionary<int, Vector2> positions = new Dictionary<int, Vector2>();
            foreach(var node in nodes)
            {
                if(!positions.ContainsKey(node.id))
                {
                    positions[node.id] = GeneratePosition(node, positions, nodes, relations);
                }
            }
            return positions;
        }

        private static Vector2 GeneratePosition(Node node, Dictionary<int, Vector2> positions, List<Node> nodes, List<Relation> relations)
        {
            Relation parentRel = relations.Find(_r => _r.source.id == node.id && _r.name == "is_a");
            if(parentRel == null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Vector2 newPos = new Vector2(10, random.Next(50, 800));
                    bool close = false;
                    foreach (var pos in positions)
                    {
                        if (Vector2.Distance(newPos, pos.Value) < 300)
                            close = true;
                    }
                    if (!close)
                        return newPos;
                }
                return new Vector2(10, random.Next(50, 800));
            }
            else
            {
                Node parentNode = parentRel.destination;
                if (!positions.ContainsKey(parentNode.id))
                {
                    positions[parentNode.id] = GeneratePosition(parentNode, positions, nodes, relations);
                }

                Vector2 parentPos = positions[parentNode.id];
                for (int i = 0; i < 50; i++)
                {
                    Vector2 newPos = new Vector2(parentPos.X + random.Next(100, 300), parentPos.Y + random.Next(-200, 200));
                    bool close = false;
                    foreach(var pos in positions)
                    {
                        if (Vector2.Distance(newPos, pos.Value) < 200)
                            close = true;
                    }
                    if (!close)
                        return newPos;
                }
                return new Vector2(parentPos.X + random.Next(100, 200), parentPos.Y + random.Next(-150, 150));
            }
        }

        private static void WriteAttribute(Utf8JsonWriter jsonWriter, KeyValuePair<string, IAttribute> attrib)
        {
            if(attrib.Value is TextAttribute)
            {
                TextAttribute textAttribute = attrib.Value as TextAttribute;
                jsonWriter.WritePropertyName(attrib.Key);
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("type", "t");
                jsonWriter.WriteString("value", textAttribute.value);
                jsonWriter.WriteEndObject();
            }
            if(attrib.Value is DomainAttribute)
            {
                DomainAttribute domainAttribute = attrib.Value as DomainAttribute;
                jsonWriter.WritePropertyName(attrib.Key);
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("type", "d");
                jsonWriter.WriteString("domain", domainAttribute.domain);
                jsonWriter.WriteBoolean("isRequestable", domainAttribute.isRequestable);
                jsonWriter.WriteString("value", domainAttribute.value);
                jsonWriter.WriteEndObject();
            }
            if (attrib.Value is FrameAttribute)
            {
                FrameAttribute fraimAttribute = attrib.Value as FrameAttribute;
                jsonWriter.WritePropertyName(attrib.Key);
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("type", "f");
                jsonWriter.WriteBoolean("isRequestable", fraimAttribute.isRequestable);
                jsonWriter.WriteString("value", fraimAttribute.value);
                jsonWriter.WriteEndObject();
            }
            if(attrib.Value is ArrayAttribute)
            {
                ArrayAttribute arrayAttribute = attrib.Value as ArrayAttribute;
                jsonWriter.WritePropertyName(attrib.Key);
                jsonWriter.WriteStartArray();
                foreach(var item in arrayAttribute.items)
                {
                    jsonWriter.WriteStringValue(item);
                }
                jsonWriter.WriteEndArray();
            }
        }
    }
}
