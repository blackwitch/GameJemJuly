using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionUI : MonoBehaviour
{
    public TMP_Text itemname, desc, detail, combDesc;

    public void Set(string _name)
    {
        ItemInfo itemInfo = DataManager.instance.itemList.items.Find(e => e.itemName == _name);
        if(itemInfo != null)
        {
            itemname.text = itemInfo.KoreanName;
            desc.text = itemInfo.description;
            switch (itemInfo.itemType)
            {
                default: // case eItemType.EXPENDABLE:
                    {
                        detail.text = "??? : " + itemInfo.durability;
                    }
                    break;
                case eItemType.HOUSE:
                    {
                        detail.text = "HP ?? " + itemInfo.value + "/s \n" + "?? ?? ?? : " + itemInfo.defense;
                    }
                    break;
                case eItemType.BOAT:
                    {
                        detail.text = "?? ??? : " + itemInfo.value + "\n" + "?? ?? ?? : " + itemInfo.defense;
                    }
                    break;
                case eItemType.WEAPON:
                    {
                        detail.text = "?? ?? ??  : " + itemInfo.value;
                    }
                    break;
            }

            var str = "";
            for (int i = 0; i < itemInfo.list_requiredResource.Count; i++)
            {
                str += itemInfo.list_requiredResource[i].ResourceKind.ToString() + " : " + itemInfo.list_requiredResource[i].count.ToString();
            }
            combDesc.text = str;
        }
    }
}