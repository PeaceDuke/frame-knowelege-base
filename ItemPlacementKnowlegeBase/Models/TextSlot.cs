using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemPlacementKnowlegeBase.Models
{
    /// <summary>
    /// Слот текстового типа
    /// </summary>
    [Serializable]
    public class TextSlot : Slot
    {
        private string _text;

        public static string TypeFriendlyName => "Текст";

        public override string TypeAsString => TextSlot.TypeFriendlyName;

        public override string ValueAsString => Text ?? string.Empty;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(ValueAsString));
            }
        }

        public TextSlot(string name, string text = null, bool isSystemSlot = false, bool isRequestable = false, bool isResult = false) : base(name, isSystemSlot, isRequestable, isResult)
        {
            Text = text ?? string.Empty;
        }

    }
}
