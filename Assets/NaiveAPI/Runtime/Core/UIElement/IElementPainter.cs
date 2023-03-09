using System;
using UnityEngine.UIElements;

namespace NaiveAPI
{
    public interface IElementPainter
    {
        Type TargetType { get; }
        UIDocument UIDocument { get; }
        VisualElement Element { get; }
        public void ApplySettings();
        public void Initialize();
    }
}
