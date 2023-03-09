using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaiveAPI.ItemSystem;
using System.Text;

public class LootExample : MonoBehaviour
{
    public SOItemBase item;
    public SOLootPoolInfo LootPoolInfos;
    public Text Text;
    public SpriteRenderer Sprite;
    [SerializeField]
    LootPool LootPool = new LootPool();

    // Start is called before the first frame update
    void Start()
    {
        LootPool.AddRange(LootPoolInfos.lootBags, LootPoolInfos.EmptyBehavior);
        Debug.Log(LootPool.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRandomLoot()
    {
        item = LootPool.Get();
        if (item != null)
        {
            Sprite.sprite = item.Icon;
            Text.text = string.Format("Congratulation! you won a loot \"{0}\"", item.DisplayName);
        }
        else
        {
            Sprite.sprite = null;
            Text.text = "So sad! you didn't get anything.";
        }
        /*
        float[] actualProbability = LootPool.GetActualProbability();
        if (actualProbability == null) return;
        StringBuilder str = new StringBuilder("Probibilities : [ " + actualProbability[0] + "%");
        for (int i = 1;i < actualProbability.Length; i++)
        {
            str.Append(", " + actualProbability[i] + "%");
        }
        print(str + " ]");
        float[] relatedProbability = LootPool.GetRelatedProbability();
        str = new StringBuilder("[" + relatedProbability[0]);
        for (int i = 1; i < relatedProbability.Length; i++)
        {
           str.Append(", " + relatedProbability[i]);
        }
        print(str + "]");
        */
    }
}
