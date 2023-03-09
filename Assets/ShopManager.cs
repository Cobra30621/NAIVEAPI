using System.Collections;
using System.Collections.Generic;
using NaiveAPI.ItemSystem;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private SOCraftTable goods;
    [SerializeField] private InventoryUsageSample backpack;
    [SerializeField] private ItemStack money;

    [SerializeField] private Image moneyImage;
    [SerializeField] private Text moneyText;
    
    // Start is called before the first frame update
    void Start()
    {
        money = backpack.inventory.Slots[0];
        UpdateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateInfo()
    {
        moneyImage.sprite = money.Item.Icon;
        moneyText.text = money.Item.DisplayName;
    }
}
