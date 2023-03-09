using NaiveAPI.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class testSlotDisplay : MonoBehaviour
{
    public UIDocument UIDocument;
    public string Name = "nan";
    public InventorySlot InventorySlot;
    public int size;
    public Vector2 pos;
    public bool visibal = true;
    public string rootPath;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        repaint();
    }

    [ContextMenu("Test")]
    public void repaint()
    {
        var t=UIDocument.rootVisualElement.Q<InventorySlotVisual>(Name);
        if (t != null)
            t.parent.Remove(t);
        InventorySlotVisual inventorySlotVisual = new InventorySlotVisual();
        inventorySlotVisual.style.height = size;
        inventorySlotVisual.style.width = size;
        inventorySlotVisual.style.marginRight = new StyleLength(StyleKeyword.Auto);
        inventorySlotVisual.style.marginBottom = new StyleLength(StyleKeyword.Auto);
        inventorySlotVisual.style.marginLeft = new StyleLength(pos.x);
        inventorySlotVisual.style.marginTop = new StyleLength(pos.y);
        inventorySlotVisual.style.position = new StyleEnum<Position>(Position.Absolute);
        inventorySlotVisual.name = Name;
        inventorySlotVisual.visible = visibal;
        //inventorySlotVisual.Repaint(InventorySlot);
        if (rootPath != null)
        {
            UIDocument.rootVisualElement.Q(rootPath).Add(inventorySlotVisual);
        }
        else
        {
            UIDocument.rootVisualElement.Add(inventorySlotVisual);
        }
    }
}
