using NaiveAPI.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSystemSampleUIScript : MonoBehaviour
{
    private VisualElement root;
    public InventorySlot slot = new InventorySlot();
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        //var  ui = root.Q<InventorySlotVisual>("slot");
        //ui.SetSize(100);
        //ui.Repaint(slot);

        //root.Q<Button>("TestButton").clicked += () =>
        //{
        //    Debug.Log("ÂIÀ»«ö¶s!");
        //};
        Button a = new Button();
        a.style.paddingBottom = 50;
        StyleLength s = new StyleLength();
        s.keyword = StyleKeyword.Auto;
        a.style.marginTop = s;
        a.style.marginBottom = s;
        a.clicked += () =>
        {
            Debug.Log("·sªº«ö¶s!");
        };

        root.Add(a);
    }

    [ContextMenu("t")]
    public void t()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        GetComponent<UIDocument>().Invoke("Awake", 0.5f);
        root.Add(new Button());
    }
}
