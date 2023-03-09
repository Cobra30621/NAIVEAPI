using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace NaiveAPI
{
    namespace ItemSystem
    {
        public class InventorySlotPainter : UIElementPainter<InventorySlotVisual>
        {

            public SOInventorySlotTheme theme;
            public InventorySlot testSlot;
            private void Start()
            {
                Initialize();
                element.ApplyTheme(theme);
                ApplySettings();

                element.Repaint(testSlot);
            }
            [ContextMenu("DEBUG")]
            public void ttst()
            {
                Debug.Log(element);
            }
        }
    }
}
