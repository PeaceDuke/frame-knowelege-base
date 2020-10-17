using System;
using System.Collections.Generic;
using System.Linq;
using ItemPlacementKnowlegeBase.Models;

namespace ItemPlacementKnowlegeBase.Services
{
    public class Reasoner
    {
        private readonly KnowlegeBase _model;
        private Dictionary<String, DomainValue> _memory = new Dictionary<string, DomainValue>();

        private Frame _bindingCandidate = null;
        private Stack<Frame> _bindingStack = new Stack<Frame>();

        private Dictionary<Frame, bool> _bindedFrames = new Dictionary<Frame, bool>();
        private Slot _askSlot;

        private Frame _bindedSubframe = null;
        private Frame _resultFrame;

        public Reasoner(KnowlegeBase model)
        {
            _model = model;
        }

        public bool AnswerFound { get => _bindedSubframe != null && _resultFrame != null; }

        public DomainSlot GetNextValueToAsk()
        {
            if (_bindingCandidate == null)
            {
                _bindingCandidate = _getFirstCandidate();
                Console.WriteLine("First candidate is ", _bindingCandidate.Name);

                _bindingStack.Push(_bindingCandidate);
            }

            while (true)
            {
                if (_bindingStack.Count == 0 && _resultFrame != null)
                {
                    Console.WriteLine("The binding stack is empty");
                    return null;
                }

                if (_bindingStack.Count == 0)
                {
                    if (_bindedSubframe != null)
                    {
                        Console.WriteLine("Subframe bound " + _bindedSubframe.Name);
                        var cand = GetCandidateFrame(_bindedSubframe);
                        if (cand == null)
                        {
                            Console.WriteLine("Candidate not found");
                            return null;
                        }
                        Console.WriteLine("Next candidate is " + cand.Name);
                        _bindingStack.Push(cand);
                    }
                    else
                    {
                        Console.WriteLine("Subframe not bound. Go to next candidate");
                        var cand = GetCandidateFrame(null);
                        if (cand == null)
                        {
                            Console.WriteLine("Candidate not found");
                            return null;
                        }
                        Console.WriteLine("Next candidate is " + cand.Name);
                        _bindingStack.Push(cand);
                    }

                    continue;
                }

                var topFrameSlotsSuits = CheckSlotsSuits(_bindingStack.Peek());
                Console.WriteLine("Top frame " + _bindingStack.Peek().Name + " " + (topFrameSlotsSuits));
                if (topFrameSlotsSuits == true)
                {
                    _bindedFrames[_bindingStack.Peek()] = true;
                    if (_bindingStack.Peek().Parent != null)
                        _bindingStack.Push(_bindingStack.Peek().Parent);
                    else
                    {
                        if (_bindedSubframe == null)
                            _bindedSubframe = _bindingStack.Last();
                        else
                            _resultFrame = _bindingStack.Last();
                        _bindingStack.Clear();
                    }
                }
                else if (topFrameSlotsSuits == false)
                {
                    _bindedFrames[_bindingStack.Last()] = false;
                    foreach (var frame in _bindingStack)
                    {
                        _bindedFrames[frame] = false;
                    }

                    _bindingStack.Clear();
                }
                else
                {
                    var slot = FindFirstSlotToAsk(_bindingStack.Peek());
                    if (slot.IsRequestable)
                    {
                        _askSlot = slot;
                        return (DomainSlot)_askSlot;
                    }

                    _memory[slot.Name] = (slot as DomainSlot).Value;
                }
            }
        }

        private Frame GetCandidateFrame(Frame subframe)
        {
            if (subframe != null)
            {
                var frames = _model.Frames.Where(x =>
                    x.Slots.Where(z => z is FrameSlot).Select(y => (y as FrameSlot).Frame).Contains(subframe));
                var suitFrames = frames.Where(x => CheckSlotsSuits(x) == true && !_bindedFrames.ContainsKey(x));
                if (suitFrames.Count() != 0)
                    return suitFrames.First();
                var unusedFrames = frames.Where(x => CheckSlotsSuits(x) == null && !_bindedFrames.ContainsKey(x));
                if (unusedFrames.Count() != 0)
                    return unusedFrames.FirstOrDefault();

                while (true)
                {
                    frames = frames.Select(x => x.Parent);
                    if (frames.Contains(null))
                        return null;
                    suitFrames = frames.Where(x => CheckSlotsSuits(x) == true && !_bindedFrames.ContainsKey(x));
                    if (suitFrames.Count() != 0)
                        return suitFrames.First();
                    unusedFrames = frames.Where(x => CheckSlotsSuits(x) == null && !_bindedFrames.ContainsKey(x));
                    if (unusedFrames.Count() != 0)
                        return unusedFrames.FirstOrDefault();
                }
            }
            else
            {

                var subframes = new List<FrameSlot>();

                foreach (var frame in _model.Frames)
                {
                    subframes.AddRange(frame.Slots.Where(x => x is FrameSlot && !x.IsSystemSlot)
                        .Select(x => x as FrameSlot));
                }

                var suitFrames = subframes.Where(x => CheckSlotsSuits(x.Frame) == true && !_bindedFrames.ContainsKey(x.Frame));
                if (suitFrames.Count() != 0)
                    return suitFrames.First()?.Frame;
                var unusedFrames = subframes.Where(x => CheckSlotsSuits(x.Frame) == null && !_bindedFrames.ContainsKey(x.Frame));
                return unusedFrames.FirstOrDefault()?.Frame;
            }

        }

        public DomainSlot test(Frame f)
        {
            if (_bindingCandidate == null)
            {
                _bindingCandidate = f;
                Console.WriteLine("First candidate is ", _bindingCandidate.Name);

                _bindingStack.Push(_bindingCandidate);
            }

            while (true)
            {
                if (_bindingStack.Count == 0)
                {
                    if (_bindedSubframe != null)
                    {
                        Console.WriteLine("Subframe bound " + _bindedSubframe.Name);
                        var cand = GetCandidateFrame(_bindedSubframe);
                        if (cand == null)
                        {
                            Console.WriteLine("Candidate not found");
                            return null;
                        }
                        Console.WriteLine("Next candidate is " + cand.Name);
                        _bindingStack.Push(cand);
                    }
                    else
                    {
                        Console.WriteLine("Subframe not bound. Go to next candidate");
                        var cand = GetCandidateFrame(null);
                        if (cand == null)
                        {
                            Console.WriteLine("Candidate not found");
                            return null;
                        }
                        Console.WriteLine("Next candidate is " + cand.Name);
                        _bindingStack.Push(cand);
                    }

                    continue;
                }

                var topFrameSlotsSuits = CheckSlotsSuits(_bindingStack.Peek());
                Console.WriteLine("Top frame " + _bindingStack.Peek().Name + " " + (topFrameSlotsSuits));
                if (topFrameSlotsSuits == true)
                {
                    _bindedFrames[_bindingStack.Peek()] = true;
                    if (_bindingStack.Peek().Parent != null)
                        _bindingStack.Push(_bindingStack.Peek().Parent);
                    else
                    {
                        if (_bindedSubframe == null)
                        {
                            foreach (var slot in _bindingStack.Last().Slots)
                            {
                                if (slot is FrameSlot && !slot.IsSystemSlot && slot.IsRequestable)
                                {
                                    _bindedSubframe = (slot as FrameSlot).Frame;
                                    break;
                                }
                            }
                        }
                        else
                            _resultFrame = _bindingStack.Last();
                        _bindingStack.Clear();
                    }
                }
                else if (topFrameSlotsSuits == false)
                {
                    _bindedFrames[_bindingStack.Last()] = false;
                    foreach (var frame in _bindingStack)
                    {
                        _bindedFrames[frame] = false;
                    }

                    _bindingStack.Clear();
                }
                else
                {
                    var slot = FindFirstSlotToAsk(_bindingStack.Peek());
                    if (slot.IsRequestable)
                    {
                        _askSlot = slot;
                        return (DomainSlot)_askSlot;
                    }

                    _memory[slot.Name] = (slot as DomainSlot).Value;
                }
            }
        }

        public List<Frame> GetAllSubFrames(string frameName)
        {
            List<Frame> frames = new List<Frame>();
            Frame frame = _model[frameName];
            if (frame != null)
                frames.AddRange(getSubFrames(frame));
            return frames;
        }

        public Frame GetFrame(string frameName)
        {
            Frame frame = _model[frameName];
            if (frame != null)
                return frame;
            return null;
        }

        public void SetAnswer(DomainValue value)
        {
            _memory[_askSlot.Name] = value;
        }

        public void AddFrame(Frame frame)
        {
            _model.Frames.Add(frame);
        }

        public void AddDomain(Domain domain)
        {
            _model.Domains.Add(domain);
        }

        public Domain GetDomain(string domainName)
        {
            return _model.Domains.Where(x => x.Name == domainName).First();
        }

        public void Clear()
        {
            _memory.Clear();
            _bindingStack.Clear();
            _bindedFrames.Clear();
            _bindingCandidate = null;
            _bindedSubframe = null;
            _resultFrame = null;
        }

        private List<Frame> getSubFrames(Frame frame)
        {
            List<Frame> frames = new List<Frame>();
            foreach (Frame f in frame.Children)
            {
                frames.AddRange(getSubFrames(f));
                if (f.Children.Count == 0)
                    frames.Add(f);
            }
            return frames;

        }

        private Slot FindFirstSlotToAsk(Frame frame)
        {
            foreach (var slot in frame.Slots)
            {
                if (!(slot is DomainSlot)) continue;
                if (!_memory.ContainsKey(slot.Name))
                {
                    return slot;
                }
            }

            throw new Exception($"Frame {frame} was expected to have unasked slots");
        }

        public Frame GetAnswer()
        {
            var result = _resultFrame;
            //while (result != null && result.Parent != null)
            //    result = result.Parent;
            if (result != null)
                return result;

            return null;
        }

        public List<Frame> GetInferringPath()
        {
            var selectedFrames = new List<Frame>();
            var subframe = _bindedSubframe;

            while (subframe != null)
            {
                selectedFrames.Add(subframe);
                subframe = subframe.Parent;
            }

            return selectedFrames;
        }

        private bool? CheckSlotsSuits(Frame frame)
        {
            var hasUnsetSlots = false;
            foreach (var slot in frame.Slots)
            {
                if (slot is DomainSlot)
                {
                    var domainSlot = slot as DomainSlot;
                    if (_memory.ContainsKey(slot.Name))
                    {
                        if (_memory[slot.Name] != domainSlot.Value)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        hasUnsetSlots = true;
                    }
                }
            }

            if (hasUnsetSlots)
                return null;
            return true;
        }

        public Frame _getFirstCandidate()
        {
            var subframe = _getMostCommonSubframe();
            var leaf = _getAnyLeaf();
            return subframe ?? leaf;
        }

        private Frame _getMostCommonSubframe()
        {
            var subframes_count = new Dictionary<Frame, int>();

            foreach (var frame in _model.Frames)
            {
                foreach (var subframe in frame.Slots.Where(x => x is FrameSlot && !x.IsSystemSlot)
                    .Select(x => x as FrameSlot))
                {
                    if (!subframes_count.ContainsKey(subframe.Frame))
                        subframes_count[subframe.Frame] = 0;
                    subframes_count[subframe.Frame] += 1;
                }
            }

            if (subframes_count.Count == 0)
                return null;
            return subframes_count.OrderBy(x => x.Value).LastOrDefault().Key;

            //return used_subframes.FirstOrDefault()?.Frame;

            //var maxUsed = used_subframes.Max(x => used_subframes.Count(y => y == x));
            //var result = used_subframes.FirstOrDefault(x => used_subframes.Count(y => y == x) == maxUsed);
            //var frameSlot = result as FrameSlot;
            //return frameSlot?.Frame;
            //var used_subframes = new List<FrameSlot>();

            //foreach (var frame in _model.Frames)
            //{
            //    used_subframes.AddRange(frame.Slots.Where(x => x is FrameSlot && !x.IsSystemSlot)
            //        .Select(x => x as FrameSlot));
            //}

            //return used_subframes.FirstOrDefault()?.Frame;

            //var maxUsed = used_subframes.Max(x => used_subframes.Count(y => y == x));
            //var result = used_subframes.FirstOrDefault(x => used_subframes.Count(y => y == x) == maxUsed);
            //var frameSlot = result as FrameSlot;
            //return frameSlot?.Frame;
        }

        private Frame _getAnyLeaf()
        {
            foreach (var frame in _model.Frames)
            {
                if (frame.Children.Count == 0)
                    return frame;
            }

            return null;
        }
    }
}