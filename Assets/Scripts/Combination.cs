using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{
    public Dictionary<string, List<simpleResource>> dic_CombinationManual = new Dictionary<string, List<simpleResource>>();

    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;

    public List<CombinationUI> cuis;


    public void UpdateButtons()
    {
        cuis.ForEach(e =>
        {
            e.ButtonIsInteractable();
        });

    }
    public bool CheckCombination(string _itemName)
    {
        ItemInfo ii = DataManager.instance.itemList.items.Find(e => e.itemName == _itemName);
        if(ii == null)
            return false;
        bool allyouhave = true;
        foreach (NeedResourceInfo ri in ii.list_requiredResource)
        {
            SlotInfo si = DataManager.instance.myInven.mySlot.Find(e => (e is ResourceSlot && ((ResourceSlot)e).kind.ToString() == ri.ResourceKind));
            if (si == null || si.count < ri.count)
            {
                allyouhave = false;
            }
        }
        return allyouhave;
    }

    public void CombineResources(string _itemName)
    {
        ItemInfo ii = DataManager.instance.itemList.items.Find(e => e.itemName == _itemName);
        if (ii == null)
            return;

        foreach (NeedResourceInfo ri in ii.list_requiredResource)
        {
            SlotInfo si = DataManager.instance.myInven.mySlot.Find(e => (e is ResourceSlot && ((ResourceSlot)e).kind.ToString() == ri.ResourceKind));
            if (si.count < ri.count)
            {
                GameManager.Instance.player.FailSomeThing();
                return;
            }
            si.count -= ri.count;
            if(si.count <=0 )
            {
                DataManager.instance.myInven.mySlot.Remove(si);
            }
            InvenSlot slot = UIManager.instance.InvenSlotList.Find(e => (e.bResource == true && e.kind.ToString() == ri.ResourceKind && e.count > 0));
            if (slot)
            {
                slot.AddCount(-ri.count);
            }
        }
        GameManager.Instance.player.SuccessSomeThing();

        if (ii.itemType == "EXPENDABLE")     //  인벤에 추가
        {
            Inventory.instance.AddItemToInventoryInc(_itemName, 1);
        }else if (ii.itemType == "HOUSE")   //  정해진 위치에 활성화
        {
            Item item;
            UIManager.instance.AddItemToInventoryUI(GameObject.Find("Prefabs").transform.Find(_itemName).gameObject, out item);
            // 처리 안함.
        }
        else if (ii.itemType == "BOAT")     //  정해진 위치에 활성화
        {
            Item item;
            UIManager.instance.AddItemToInventoryUI(GameObject.Find("Prefabs").transform.Find(_itemName).gameObject, out item);

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
        UpdateButtons();
    }
}