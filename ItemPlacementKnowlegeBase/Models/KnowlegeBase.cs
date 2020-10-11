using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    public class KnowlegeBase
    {
        private readonly Domain _frameSlotDomain;
        private readonly Domain _textSlotDomain;
        private Frame _frameToDelete;

        /// <summary>
        /// Фреймы модели
        /// </summary>
        public ObservableCollection<Frame> Frames { get; set; }

        /// <summary>
        /// Домены модели
        /// </summary>
        public ObservableCollection<Domain> Domains { get; }

        /// <summary>
        /// Фрейм по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Фрейм</returns>
        //public Frame this[int index] => Frames[index];

        /// <summary>
        /// Фрейм по имени
        /// </summary>
        /// <param name="name">Имя фрейма</param>
        /// <returns>Фрейм</returns>
        public Frame this[string name] => Frames.First(f => f.Name == name);

        public Domain FrameSlotDomain => _frameSlotDomain;

        public Domain TextSlotDomain => _textSlotDomain;

        public KnowlegeBase()
        {
            Domains = new ObservableCollection<Domain>();
            Frames = new ObservableCollection<Frame>();

            _frameSlotDomain = new Domain(FrameSlot.TypeFriendlyName);
            _textSlotDomain = new Domain(TextSlot.TypeFriendlyName);

            _frameSlotDomain.Values.Add(string.Empty);

            Domains.Add(_frameSlotDomain);
            Domains.Add(_textSlotDomain);
        }

        private void ProcessAddedFrame(Frame frame)
        {
            if (frame == null)
                throw new ArgumentNullException(nameof(frame), "Фрейм не может быть null");

            if (Frames.Count(f => ReferenceEquals(f, frame)) > 1)
            {
                Frames.Remove(frame);
                throw new ArgumentException("Такой фрейм уже есть в модели", nameof(frame));
            }

            foreach (var slot in frame.Slots.OfType<DomainSlot>())
            {
                var domain = slot.Domain;

                if (!Domains.Contains(domain))
                    Domains.Add(domain);
            }

            _frameSlotDomain.Values.Add(frame.Name);
        }

        private void ProcessRemovedFrame(Frame frame)
        {
            _frameToDelete = frame;

            frame.Parent = null;

            foreach (var frameChild in frame.Children)
            {
                frameChild.Parent = null;
            }

            var domainValue = _frameSlotDomain.Values.First(v => v.Text == frame.Name);
            _frameSlotDomain.Values.Remove(domainValue);
        }

        private void ProcessExternallyAddedFrame(Frame frame)
        {
            if (frame != null && !Frames.Contains(frame))
                Frames.Add(frame);
        }

        private void ProcessAddedDomain(Domain domain)
        {
            if (Domains.Count(d => ReferenceEquals(d, domain)) > 1)
            {
                Domains.Remove(domain);
                throw new ArgumentException("Такой домен уже есть в модели", nameof(domain));
            }
        }

        private void ProcessRemovedDomain(Domain domain)
        {
            var useList = new List<Tuple<Frame, DomainSlot>>();

            foreach (var frame in Frames)
                foreach (var slot in frame.Slots.OfType<DomainSlot>())
                    if (ReferenceEquals(slot.Domain, domain))
                        useList.Add(Tuple.Create(frame, slot));

            if (useList.Count > 0)
            {
                Domains.Add(domain);
            }
        }

        public List<Tuple<Frame, DomainSlot, Domain>> CheckDomainValueIntegrity(DomainValue valueToCheck)
        {
            var usedList = new List<Tuple<Frame, DomainSlot, Domain>>();

            foreach (var frame in Frames)
            {
                foreach (var slot in frame.Slots.OfType<DomainSlot>())
                {
                    var domain = slot.Domain;

                    if (ReferenceEquals(slot.Value, valueToCheck))
                    {
                        usedList.Add(Tuple.Create(frame, slot, domain));
                    }
                }
            }

            return usedList;
        }

        public List<Tuple<Frame, DomainSlot, Domain, DomainValue>> RestoreDomainValueIntegrity()
        {
            var resetList = new List<Tuple<Frame, DomainSlot, Domain, DomainValue>>();

            foreach (var frame in Frames)
            {
                foreach (var slot in frame.Slots.OfType<DomainSlot>())
                {
                    var domain = slot.Domain;
                    var value = slot.Value;

                    if (!domain.Values.Contains(value))
                    {
                        resetList.Add(Tuple.Create(frame, slot, domain, value));
                        slot.Value = null;
                    }
                }
            }

            return resetList;
        }
    }
}
