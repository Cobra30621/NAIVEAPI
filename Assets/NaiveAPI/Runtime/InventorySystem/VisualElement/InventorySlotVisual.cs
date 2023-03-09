using NaiveAPI.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NaiveAPI
{
    namespace ItemSystem
    {
        public class InventorySlotVisual : VisualElement
        {
            public InventorySlotVisual()
            {
                IconImage = new VisualElement();
                CountLabel = new Label();
                IconImage.style.position = new StyleEnum<Position>(Position.Absolute);
                CountLabel.style.position = new StyleEnum<Position>(Position.Relative);

                CountLabel.style.marginTop = new StyleLength(StyleKeyword.Auto);
                CountLabel.style.marginLeft = new StyleLength(StyleKeyword.Auto);

                IconImage.style.backgroundColor = Color.white.A(0.5f);
                CountLabel.style.backgroundColor = Color.gray;
                CountLabel.style.borderTopLeftRadius = 35;
                CountLabel.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);

                SlotSize = 100;
                CountTextSize = 20;
                CountTextAnchor = TextAnchor.LowerRight;

                this.style.backgroundColor = new Color(.75f,.75f,.75f);
                this.Add(IconImage);
                this.Add(CountLabel);
                this.RegisterCallback<MouseEnterEvent>((e) =>
                {

                });
                this.RegisterCallback<MouseLeaveEvent>((e) =>
                {

                });
            }

            public InventorySlot Target { get => target; }
            private InventorySlot target;

            public VisualElement IconImage;
            public Label CountLabel;
            public void Repaint(InventorySlot slot)
            {
                if (slot.Item != null)
                    if (slot.Item.Icon != null)
                        IconImage.style.backgroundImage = new StyleBackground(slot.Item.Icon);
                CountLabel.text = slot.Count.ToString();
                target = slot;
            }

            public void ApplyTheme(SOInventorySlotTheme theme)
            {
                this.style.backgroundImage = new StyleBackground(theme.BackGroundImage);
                this.style.backgroundColor = Color.clear;
                IconImage.style.backgroundColor = Color.clear;
                CountLabel.style.backgroundColor = theme.CountTextBackground;
                if (theme.CountTextFont != null)
                    CountLabel.style.unityFont = theme.CountTextFont;
            }

            [UnityEngine.Scripting.Preserve]
            public new class UxmlFactory : UxmlFactory<InventorySlotVisual, UxmlTraits> { }
            public new class UxmlTraits : VisualElement.UxmlTraits
            { 
                UxmlIntAttributeDescription slotSize =
                    new UxmlIntAttributeDescription { name = "slot-size", defaultValue = 100 };
                UxmlIntAttributeDescription numberSize =
                    new UxmlIntAttributeDescription { name = "count-text-size", defaultValue = 20 };
                UxmlEnumAttributeDescription<TextAnchor> numberAnchor =
                    new UxmlEnumAttributeDescription<TextAnchor> { name = "count-text-anchor" };

                public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
                {
                    base.Init(ve, bag, cc);
                    var ate = ve as InventorySlotVisual;
                    ate.SlotSize = slotSize.GetValueFromBag(bag, cc);
                    ate.CountTextSize = numberSize.GetValueFromBag(bag, cc);
                    ate.CountTextAnchor = numberAnchor.GetValueFromBag(bag, cc);

                    ate.style.width = new StyleLength(new Length(ate.SlotSize, LengthUnit.Pixel));
                    ate.style.height = new StyleLength(new Length(ate.SlotSize, LengthUnit.Pixel));

                    Vector2 tempSize = new Vector2(ate.SlotSize * 0.75f, ate.SlotSize * 0.125f);
                    ate.IconImage.style.width = new StyleLength(new Length(tempSize.x, LengthUnit.Pixel));
                    ate.IconImage.style.height = new StyleLength(new Length(tempSize.x, LengthUnit.Pixel));
                    ate.IconImage.style.marginLeft = new StyleLength(new Length(tempSize.y, LengthUnit.Pixel));
                    ate.IconImage.style.marginTop = new StyleLength(new Length(tempSize.y, LengthUnit.Pixel));

                    ate.CountLabel.style.width = new StyleLength(new Length(ate.CountTextSize, LengthUnit.Pixel));
                    ate.CountLabel.style.height = new StyleLength(new Length(ate.CountTextSize, LengthUnit.Pixel));

                    ate.CountLabel.style.SetMarginAnchor(ate.CountTextAnchor);

                    ate.style.width = ate.SlotSize;
                    ate.style.height = ate.SlotSize;
                }
            }

            public int SlotSize { get; set; }
            public int CountTextSize { get; set; }
            public TextAnchor CountTextAnchor { get; set; }
        }
    }
}
