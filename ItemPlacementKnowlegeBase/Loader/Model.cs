using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Loader.Model
{
    internal class Node
    {
        public string name;
        public int id;
        public Dictionary<string, IAttribute> attributes;

        public Node(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.attributes = new Dictionary<string, IAttribute>();
        }
    }

    internal class Relation
    {
        public Node destination;
        public Node source;
        public string name;
        public int id;

        public Relation(Node destination, Node source, string name, int id)
        {
            this.destination = destination;
            this.source = source;
            this.name = name;
            this.id = id;
        }
    }
    //маркерный интерфейс
    internal interface IAttribute { }
    internal class ArrayAttribute : IAttribute
    {
        public string[] items;

        public ArrayAttribute(string[] items)
        {
            this.items = items;
        }
    }

    internal class DomainAttribute : IAttribute
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

    internal class TextAttribute : IAttribute
    {
        public string value;

        public TextAttribute(string value)
        {
            this.value = value;
        }
    }

    internal class FrameAttribute : IAttribute
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
