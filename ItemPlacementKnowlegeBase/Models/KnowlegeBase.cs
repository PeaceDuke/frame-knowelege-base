using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    public class KnowlegeBase
    {
        public List<Frame> Frames { get; }

        public KnowlegeBase()
        {
            Frames = new List<Frame>();
        }

        public Frame AddFrame(string name, bool isbase = false, Frame parent = null)
        {
            if (!IsFrameNameExist(name))
            {
                var frame = new Frame(name, isbase, parent);
                Frames.Add(frame);
                return frame;
            }
            else
                throw new ArgumentException("Фрейм с таким именем уже существует");
        }

        public void AddFrame(Frame frame)
        {
            if (!IsFrameNameExist(frame.Name))
                Frames.Add(frame);
            else
                throw new ArgumentException("Фрейм с таким именем уже существует");
        }

        public Frame GetFrame(string name)
        {
            return Frames.Find(x => x.Name.Equals(name));
        }

        public void SetFrame(Frame frame)
        {
            var frameIndex = Frames.FindIndex(x => x.Name.Equals(frame.Name));
            if (frameIndex < 0)
                Frames.Add(frame);
            else
                Frames[frameIndex] = frame;
        }

        private bool IsFrameNameExist(string name)
        {
            return Frames.Any(x => x.Name == name);
        }

        public void RemoveFrame(Frame frame)
        {
            if (IsFrameNameExist(frame.Name))
                Frames.Remove(frame);
            else
                throw new ArgumentException("Фрейма с таким именем не существует");
        }
    }
}
