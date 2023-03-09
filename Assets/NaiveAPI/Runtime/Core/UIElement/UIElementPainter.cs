using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace NaiveAPI
{
    public class UIElementPainter<T> : MonoBehaviour,IElementPainter where T:VisualElement,new()
    {
        [HideInInspector] public UIDocument uid;
        public UIDocument UIDocument { get => uid; }
        [HideInInspector] public Type TargetType { get => typeof(T); }
        [HideInInspector] public string ElementName= "UIElement";
        [HideInInspector] public bool Visible = true;

        internal T element;
        public VisualElement Element => element;

        public virtual void Initialize()
        {
            element = uid.rootVisualElement.Q<T>(ElementName);
        }

        public virtual void ApplySettings()
        {
            if (element == null)
                return;

            element.visible = Visible;
            element.MarkDirtyRepaint();
        }
    }
}
