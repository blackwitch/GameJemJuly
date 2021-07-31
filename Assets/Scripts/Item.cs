using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI tmpro;
    public ItemInfo info;
    public List<NeedResourceInfo> list_requiredResource;

    public void Awake()
    {
        for (int i = 0; i < DataManager.instance.itemList.items.Count; i++)
        {
            ItemInfo ii = DataManager.instance.itemList.items[i];

            if (ii.itemName == this.name)
            {
                Debug.Log(" SET COMB ITEM : " + ii.itemName + "," + this.info.itemName);
                InitializeItem(i);
            }
        }
    }

    void InitializeItem( int idx)
    {
        if (DataManager.instance.itemList.items.Count <= idx)
            return;
        ItemInfo ii = DataManager.instance.itemList.items[idx];
        {
            info.KoreanName = ii.KoreanName;
            info.itemName = ii.itemName;
            info.itemType = ii.itemType;
            info.description = ii.description;
            info.durability = ii.durability;
            info.value = ii.value;
            info.defense = ii.defense;
            list_requiredResource = ii.list_requiredResource;
        }
    }
}
