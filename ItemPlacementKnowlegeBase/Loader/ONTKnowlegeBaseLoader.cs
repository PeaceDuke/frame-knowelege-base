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
        public static KnowlegeBase Parce(string filename)
        {
            string text = File.ReadAllText(filename);
            KnowlegeBase knowlegeBase = new KnowlegeBase();
            List<Node> nodes = new List<Node>();
            List<Relation> relations = new List<Relation>();
            using (JsonDocument document = JsonDocument.Parse(text))
            {
                JsonElement root = document.RootElement;
                GetNodes(nodes, root);
                GetRelations(relations, nodes, root);
            }

            AddDomains(knowlegeBase, nodes, relations);
            AddFrames(knowlegeBase, nodes, relations);
            return knowlegeBase;
        }

        private static void AddFrames(KnowlegeBase knowlegeBase, List<Node> nodes, List<Relation> relations)
        {
            Node frameRootNode = nodes.Find(_n => _n.name == "Фрейм");
            var frameRootChilds = relations.FindAll(_r => _r.name == "is_a" && _r.destination == frameRootNode);
            foreach (var child in frameRootChilds)
            {
                Node node = child.source;
                Frame frame = new Frame(node.name);
                AddChildFrames(node, nodes, relations, knowlegeBase, frame);
                AddAttributes(frame, child.source, knowlegeBase);
                knowlegeBase.Frames.Add(frame);
            }
        }

        private static void AddChildFrames(Node parentNode, List<Node> nodes, List<Relation> relations, KnowlegeBase knowlegeBase, Frame parent)
        {
            var childsNodes = relations.FindAll(_r => _r.name == "is_a" && _r.destination == parentNode);
            foreach (var child in childsNodes)
            {
                Node node = child.source;
                Frame frame = new Frame(node.name);
                frame.Parent = parent;
                AddChildFrames(node, nodes, relations, knowlegeBase, frame);
                AddAttributes(frame, child.source, knowlegeBase);
                knowlegeBase.Frames.Add(frame);
            }
        }

        private static void AddAttributes(Frame frame, Node node, KnowlegeBase knowlegeBase)
        {
            foreach (var attrib in node.attributes)
            {
                if (attrib.Value is DomainAttribute)
                {
                    DomainAttribute domainAttribute = attrib.Value as DomainAttribute;
                    Domain domain = knowlegeBase.Domains.First<Domain>(_d => _d.Name == domainAttribute.domain);
                    DomainValue domainValue = domain[domainAttribute.value];
                    Slot slot = new DomainSlot(attrib.Key, domain, domainValue, false, domainAttribute.isRequestable, false);
                    frame.Slots.Add(slot);
                }
                if (attrib.Value is TextAttribute)
                {
                    TextAttribute textAttribute = attrib.Value as TextAttribute;
                    Slot slot = new TextSlot(attrib.Key, textAttribute.value, false, false, false);
                    frame.Slots.Add(slot);
                }
                if (attrib.Value is FrameAttribute)
                {
                    FrameAttribute frameAttribute = attrib.Value as FrameAttribute;
                    Slot slot = new FrameSlot(attrib.Key, knowlegeBase[frameAttribute.value], false, frameAttribute.isRequestable, false);
                    frame.Slots.Add(slot);
                }
            }
        }

        private static void AddDomains(KnowlegeBase knowlegeBase, List<Node> nodes, List<Relation> relations)
        {
            Node domainRootNode = nodes.Find(_n => _n.name == "Домен");
            var domainRootChilds = relations.FindAll(_r => _r.name == "is_a" && _r.destination == domainRootNode);
            foreach (var child in domainRootChilds)
            {
                List<DomainValue> values = new List<DomainValue>();
                foreach (var item in (child.source.attributes["list"] as ArrayAttribute).items)
                {
                    values.Add(new DomainValue(item));
                }
                Domain newDomain = new Domain(child.source.name, values);
                knowlegeBase.Domains.Add(newDomain);
            }
        }

        private static void GetRelations(List<Relation> relations, List<Node> nodes, JsonElement root)
        {
            JsonElement jRelations = root.GetProperty(RELATIONS);
            foreach (var jRelation in jRelations.EnumerateArray())
            {
                string name = jRelation.GetProperty(NAME).GetString();
                string destId = jRelation.GetProperty(DEST_NODE_ID).GetString();
                string sourceId = jRelation.GetProperty(SOURCE_NODE_ID).GetString();
                Node dest = nodes.Find(_n => _n.id == destId);
                Node source = nodes.Find(_n => _n.id == sourceId);
                if (dest == null || source == null)
                {
                    throw new ArgumentOutOfRangeException();
                }
                relations.Add(new Relation(dest, source, name));
            }
        }

        private static void GetNodes(List<Node> nodes, JsonElement root)
        {
            JsonElement jNodes = root.GetProperty(NODES);
            foreach (var jNode in jNodes.EnumerateArray())
            {
                var name = jNode.GetProperty(NAME).GetString();
                var id = jNode.GetProperty(ID).GetString();
                Node node = new Node(name, id);
                nodes.Add(node);
                JsonElement attributes = jNode.GetProperty(ATTRIBUTES);
                foreach (var attrib in attributes.EnumerateObject())
                {
                    var type = attrib.Value.ValueKind;
                    switch (type)
                    {
                        case JsonValueKind.Array:
                            {
                                List<string> items = new List<string>();
                                foreach (var item in attrib.Value.EnumerateArray())
                                {
                                    items.Add(item.GetString());
                                }
                                node.attributes[attrib.Name] = new ArrayAttribute(items.ToArray());
                                break;
                            }
                        case JsonValueKind.Object:
                            {
                                node.attributes[attrib.Name] = GetSlot(attrib.Value);
                                break;
                            }
                        default:
                            throw new ArgumentOutOfRangeException();
                            break;
                    }
                }
            }
        }

        private static IAttribute GetSlot(JsonElement node)
        {
            IAttribute result = null;
            switch (node.GetProperty(TYPE).GetString())
            {
                case "t":
                    {
                        string value = node.GetProperty(VALUE).GetString();
                        result = new TextAttribute(value);
                        break;
                    }
                case "d":
                    {
                        string domain = node.GetProperty(DOMAIN).GetString();
                        string value = node.GetProperty(VALUE).GetString();
                        bool isRequestable = node.GetProperty(IS_REQUESTABLE).GetBoolean();
                        result = new DomainAttribute(domain, value, isRequestable);
                        break;
                    }
                case "f":
                    {
                        string value = node.GetProperty(VALUE).GetString();
                        bool isRequestable = node.GetProperty(IS_REQUESTABLE).GetBoolean();
                        result = new FrameAttribute(value, isRequestable);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
                    break;
            }
            return result;
        }

        private class Node
        {
            public string name;
            public string id;
            public Dictionary<string, IAttribute> attributes;

            public Node(string name, string id)
            {
                this.name = name;
                this.id = id;
                this.attributes = new Dictionary<string, IAttribute>();
            }
        }

        private class Relation
        {
            public Node destination;
            public Node source;
            public string name;

            public Relation(Node destination, Node source, string name)
            {
                this.destination = destination;
                this.source = source;
                this.name = name;
            }
        }
        //маркерный интерфейс
        private interface IAttribute { }
        private class ArrayAttribute : IAttribute
        {
            public string[] items;

            public ArrayAttribute(string[] items)
            {
                this.items = items;
            }
        }

        private class DomainAttribute : IAttribute
        {
            public string domain;
            public string value;
            public bool isRequestable;

            public DomainAttribute(string domain, string value, bool isRequestable)
            {
                this.domain = domain;
                this.value = value;
                this.isRequestable = isRequestable;
            }
        }

        private class TextAttribute : IAttribute
        {
            public string value;

            public TextAttribute(string value)
            {
                this.value = value;
            }
        }

        private class FrameAttribute : IAttribute
        {
            public string value;
            public bool isRequestable;

            public FrameAttribute(string value, bool isRequestable)
            {
                this.value = value;
                this.isRequestable = isRequestable;
            }
        }
    }
}
