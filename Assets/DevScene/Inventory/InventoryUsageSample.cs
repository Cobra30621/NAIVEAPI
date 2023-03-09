using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaiveAPI.ItemSystem;
using UnityEngine.UI;

public class InventoryUsageSample : MonoBehaviour
{
    [SerializeField] SOItemBase circle,square,correct,target;
    [SerializeField] GameObject focusHighlight;
    [SerializeField] SOCraftRecipe testRecipe;
    [SerializeField] public Inventory inventory = new Inventory(9);
    Image[] images = new Image[6];
    Text[] counts = new Text[6];

    public InventorySlot slot = new InventorySlot();
    public ItemStack slot2 = new ItemStack();

    [ItemTag]
    public string tags;
    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            Transform child = transform.GetChild(i);
            images[i] = child.GetComponent<Image>();
            counts[i] = child.transform.GetChild(0).GetComponent<Text>();
        }
    }
    private void Update()
    {
        for(int i = 0; i < inventory.Slots.Count; i++)
        {
            if (inventory.Slots[i] == null) continue;
            if (!(inventory.Slots[i].IsEmpty))
            {
                images[i].sprite = inventory.Slots[i].Item.Icon;
                counts[i].text = inventory.Slots[i].Count.ToString();
            }
            else
            {
                images[i].sprite = null;
                counts[i].text = "0";
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            inventory.TryCraft(testRecipe);
            inventory.SortEmpty();
            //foreach (var i in inventory.TryCraft(testRecipe))
            //{
            //    inventory.Push(i);
            //}
        }
    }

    public void SetTarget(int index)
    {
        if (index == 0)
        {
            target = square;
            focusHighlight.GetComponent<RectTransform>().anchoredPosition = new Vector2(-87, 0);
        }
        else if (index == 1)
        {
            target = circle;
            focusHighlight.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        else
        {
            target = correct;
            focusHighlight.GetComponent<RectTransform>().anchoredPosition = new Vector2(87, 0);
        }
    }

    public void PushAItem()
    {
        Debug.Log( inventory.Push(target));
    }
    public void PopAItem()
    {
        inventory.Pop(target,1);
    }
    
}
