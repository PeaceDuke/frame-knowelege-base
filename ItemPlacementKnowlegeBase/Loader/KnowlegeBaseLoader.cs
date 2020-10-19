using ItemPlacementKnowlegeBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ItemPlacementKnowlegeBase.Loader
{
    class KnowlegeBaseLoader
    {
        public const string TEST_KNOWLEDGE_BASE = "../../Loader/KB.xml";
        public static KnowlegeBase Parce(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            KnowlegeBase result = new KnowlegeBase();
            xDoc.Load(path);
            var xDomains = xDoc.GetElementsByTagName("Domains")[0];
            var xFrames = xDoc.GetElementsByTagName("Frames")[0];
            Domain[] domains = ParceDomains(xDomains);
            Frame[] frames = ParceFrames(xFrames, domains);

            foreach (var domain in domains)
            {
                result.Domains.Add(domain);
            }

            foreach (var frame in frames)
            {
                result.Frames.Add(frame);
            }

            return result;
        }

        private static Frame[] ParceFrames(XmlNode xFrames, Domain[] domains)
        {
            List<Frame> frames = new List<Frame>();
            foreach(XmlNode xFrame in xFrames.ChildNodes)
            {
                string frameName = xFrame.Attributes.GetNamedItem("name").Value;
                Frame frame = new Frame(frameName);
                XmlNode xParent = xFrame.Attributes.GetNamedItem("parent");
                if(xParent != null)
                {
                    frame.Parent = frames.Find(_f => _f.Name == xParent.Value);
                    string a = frame.Parent.Name;
                }
                foreach(XmlNode xSlot in xFrame.ChildNodes)
                {
                    var slotType = xSlot.Name;
                    switch(slotType)
                    {
                        case "DomainSlot":
                            DomainSlot domainSlot = ParceDomainSlot(xSlot, domains);
                            frame.Slots.Add(domainSlot);
                            break;
                        case "TextSlot":
                            TextSlot textSlot = ParceTextSlot(xSlot);
                            frame.Slots.Add(textSlot);
                            break;
                        case "FrameSlot":
                            FrameSlot frameSlot = ParceFrameSlot(xSlot, frames);
                            frame.Slots.Add(frameSlot);
                            break;
                    }
                }
                frames.Add(frame);
            }
            return frames.ToArray();
        }

        private static FrameSlot ParceFrameSlot(XmlNode xSlot, List<Frame> frames)
        {
            string name = xSlot.Attributes.GetNamedItem("name").Value;
            string sValue = xSlot.Attributes.GetNamedItem("value").Value;
            Frame value = frames.Find(_f => _f.Name == sValue);
            XmlNode xIsSystemSlot = xSlot.Attributes.GetNamedItem("isSystemSlot");
            XmlNode xIsRequestable = xSlot.Attributes.GetNamedItem("isRequestable");
            XmlNode xIsResult = xSlot.Attributes.GetNamedItem("isResult");
            bool isSystemSlot = xIsSystemSlot == null ? false : Convert.ToBoolean(xIsSystemSlot.Value);
            bool isRequestable = xIsRequestable == null ? false : Convert.ToBoolean(xIsRequestable.Value);
            bool isResult = xIsResult == null ? false : Convert.ToBoolean(xIsResult.Value);
            return new FrameSlot(name, value, isSystemSlot, isRequestable, isResult);
        }

        private static TextSlot ParceTextSlot(XmlNode xSlot)
        {
            string name = xSlot.Attributes.GetNamedItem("name").Value;
            string sValue = xSlot.Attributes.GetNamedItem("value").Value;
            XmlNode xIsSystemSlot = xSlot.Attributes.GetNamedItem("isSystemSlot");
            XmlNode xIsRequestable = xSlot.Attributes.GetNamedItem("isRequestable");
            XmlNode xIsResult = xSlot.Attributes.GetNamedItem("isResult");
            bool isSystemSlot = xIsSystemSlot == null ? false : Convert.ToBoolean(xIsSystemSlot.Value);
            bool isRequestable = xIsRequestable == null ? false : Convert.ToBoolean(xIsRequestable.Value);
            bool isResult = xIsResult == null ? false : Convert.ToBoolean(xIsResult.Value);
            return new TextSlot(name, sValue, isSystemSlot, isRequestable, isResult);
        }

        private static DomainSlot ParceDomainSlot(XmlNode xSlot, Domain[] domains)
        {
            string name = xSlot.Attributes.GetNamedItem("name").Value;
            string sDomain = xSlot.Attributes.GetNamedItem("domain").Value;
            string sValue = xSlot.Attributes.GetNamedItem("value").Value;
            XmlNode xIsSystemSlot = xSlot.Attributes.GetNamedItem("isSystemSlot");
            XmlNode xIsRequestable = xSlot.Attributes.GetNamedItem("isRequestable");
            XmlNode xIsResult = xSlot.Attributes.GetNamedItem("isResult");
            bool isSystemSlot = xIsSystemSlot == null ? false : Convert.ToBoolean(xIsSystemSlot.Value);
            bool isRequestable = xIsRequestable == null ? false : Convert.ToBoolean(xIsRequestable.Value);
            bool isResult = xIsResult == null ? false : Convert.ToBoolean(xIsResult.Value);
            Domain domain = Array.Find<Domain>(domains, _d => _d.Name == sDomain);
            DomainValue domainValue = domain[sValue];
            return new DomainSlot(name, domain, domainValue, isSystemSlot, isRequestable, isResult);
        }

        private static Domain[] ParceDomains(XmlNode xDomains)
        {
            List<Domain> domains = new List<Domain>();
            foreach (XmlNode xDomain in xDomains.ChildNodes)
            {
                string domainName = xDomain.Attributes.GetNamedItem("name").Value;
                List<DomainValue> domainValues = new List<DomainValue>();
                foreach (XmlNode xDomainValue in xDomain.ChildNodes)
                {
                    string domainValueName = xDomainValue.Attributes.GetNamedItem("name").Value;
                    domainValues.Add(new DomainValue(domainValueName));
                }
                domains.Add(new Domain(domainName, domainValues.ToArray()));

            }
            return domains.ToArray();
        }
    }
}
