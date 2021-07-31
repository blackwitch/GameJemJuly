using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{
    public Dictionary<string, List<simpleResource>> dic_CombinationManual = new Dictionary<string, List<simpleResource>>();

    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    
    public bool CheckCombination(string _itemName)
    {
        if (!dic_CombinationManual.ContainsKey(_itemName))
            return false;
        foreach (var requiredResource in dic_CombinationManual[_itemName])
        {
            simpleResource matchResource = Inventory.instance.sResources.Find(x => x.kind == requiredResource.kind);

            if (matchResource != null && matchResource.count >= requiredResource.count)
            {
                return true;
            }
        }
        return false;
    }

    public void CombineResources(string _itemName)
    {
        if (CheckCombination(_itemName))
        {
            foreach (var requiredResource in dic_CombinationManual[_itemName])
            {
                simpleResource matchResource = Inventory.instance.sResources.Find(x => x.kind == requiredResource.kind);

                if (matchResource != null)
                {
                    matchResource.count -= requiredResource.count;


                    simpleItem itemToMake = Inventory.instance.sItems.Find(x => x.name == _itemName);

                    if (itemToMake != null)
                    {
                        itemToMake.count++;
                    }
                    else
                    {
                        //Inventory.instance.list_MyItem.Add(DataManager.instance.list_itemInfo.Find(x => x.itemName == _itemName));
                        Item item;
                        UIManager.instance.AddItemToInventoryUI(GameObject.Find("Prefabs").transform.Find(_itemName).gameObject,out item);
                        if (item.info.itemType == eItemType.BOAT)
                        {
                            print("ship!!");
                            if (item.name == "boat")
                            {
                                ship1.SetActive(true);
                            }
                            else if (item.name == "ship")
                            {
                                ship2.SetActive(true);
                            }
                            else
                            {
                                ship3.SetActive(true);
                            }
                            GameManager.Instance.boatName = item.name;
                        }
                        //Inventory.instance.list_MyItem.Add(item);
                    }

                }
            }
        }
    }
}