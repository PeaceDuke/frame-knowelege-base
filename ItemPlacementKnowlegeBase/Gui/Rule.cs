using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Gui
{
    public class Rule
    {
        public string Name { get; }
        public Item Object { get; }
        public Item Subject { get; }
        public string Type { get; }
        public string Where { get; }

        public Rule(string name, Item obj, Item subject)
        {
            Name = name;
            Object = obj;
            Subject = subject;
        }

        public Rule(string name, Item obj, Item subject, string type)
        {
            Name = name;
            Object = obj;
            Subject = subject;
            Type = type;
        }

        public Rule(string name, Item obj, Item subject, string type, string where)
        {
            Name = name;
            Object = obj;
            Subject = subject;
            Type = type;
            Where = where;
        }

        public string GetDescription()
        {
            return (Object == null ? "Любой" : Object.Name) + " -> " + (Subject == null ? "Любой" : Subject.Name) + ": " + (string.IsNullOrEmpty(Where) ? "Рядом" : Where) + ", " + Type;
        }
    }
}
