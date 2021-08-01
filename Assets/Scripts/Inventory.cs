using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    static Inventory _instance;

    public static Inventory instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
            }
            return _instance;
        }
    }

    // public List<Resource> list_MyResource = new List<Resource>();
    // public List<Item> list_MyItem = new List<Item>();
    [SerializeField]
    public List<simpleItem> sItems = new List<simpleItem>();
    [SerializeField]
    public List<simpleResource> sResources = new List<simpleResource>();
    public int capacity = 36;    //???? ???? ????

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

/*    private void Start()
    {
        if (list_MyResource == null)
            list_MyResource = new List<Resource>();

        if (list_MyItem == null)
            list_MyItem = new List<Item>();

    }*/

    // inventory ?? ? ???
    public void RemoveResource()
    {
        //list_MyResource.Clear();
        sItems.Clear();
    }

    public void ResetInventory()
    {
        sItems.Clear();
        sResources.Clear();
    }
    public void ReloadInvetory()
    {
        for(int i = 0; i < sItems.Count;i++)
        {
            //Item temp;
            //UIManager.instance.AddItemToInventoryUI(GameObject.Find("Prefabs").transform.Find(sItems[i].name).gameObject, out temp);
            //list_MyItem.Add(temp);
            //temp.count = sItems[i].count;
        }
        UIManager.instance.InvenSlotList.ForEach((ele) =>
        {
            ele.Clear();
        });
        DataManager.instance.myInven.mySlot.ForEach(ele =>
        {
            if(ele is ResourceSlot)
                AddResourceToInventory(((ResourceSlot)ele).kind, ele.count);
            else
                AddItemToInventory(((ItemSlot)ele).itemName, ele.count);
        });
/*
        for (int i = 0; i < sResources.Count; i++)
        {
            if (i >= UIManager.instance.InvenSlotList.Count)
            {
                Debug.Log(" 아이템 종류가 넘 많아!");
                break;
            }

            UIManager.instance.InvenSlotList[i].Set(sResources[i].kind, sResources[i].count);
        }
*/
    }

    public void AddResourceToInventoryInc(Resource _resource)
    {
        SlotInfo si = DataManager.instance.myInven.mySlot.Find(e => (e is ResourceSlot && ((ResourceSlot)e).kind == _resource.ResourceKind && e.count > 0));
        if (si != null)
        {
            si.count += _resource.count;
        }
        else
        {
            DataManager.instance.myInven.mySlot.Add(new ResourceSlot(_resource.ResourceKind, _resource.count));
        }
        AddResourceToInventory(_resource.ResourceKind, _resource.count);
    }

    public void AddResourceToInventory(ResourceKind _kind, int _count)
    {
        //  같은 종류가 있는지 우선 확인.
        InvenSlot slot = UIManager.instance.InvenSlotList.Find(e => (e.bResource == true && e.kind == _kind && e.count > 0));
        if (slot)
        {
            slot.AddCount(_count);
            return;
        }

        InvenSlot emptySlot = UIManager.instance.InvenSlotList.Find(e => e.count == -1);
        if (emptySlot)
        {
            emptySlot.Set(_kind, _count);
        }
    }

    public void AddItemToInventoryInc(string _itemName, int _count)
    {
        SlotInfo si = DataManager.instance.myInven.mySlot.Find(e => (e is ItemSlot && ((ItemSlot)e).itemName == _itemName && e.count > 0));
        if (si != null)
        {
            si.count += _count;
        }
        else
        {
            DataManager.instance.myInven.mySlot.Add(new ItemSlot(_itemName, _count));
        }
        AddItemToInventory(_itemName, _count);
    }
    public void AddItemToInventory(string _itemName, int _count) { 

        //  같은 종류가 있는지 우선 확인.
        InvenSlot slot = UIManager.instance.InvenSlotList.Find(e => (e.bResource == false && e.itemName == _itemName && e.count > 0));
        if (slot)
        {
            slot.AddCount(_count);
            return;
        }

        InvenSlot emptySlot = UIManager.instance.InvenSlotList.Find(e => e.count == -1);
        if (emptySlot)
        {
            emptySlot.Set(_itemName, _count);
        }
    }
}
