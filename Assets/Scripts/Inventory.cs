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

        for (int i = 0; i < sResources.Count; i++)
        {
            if (i >= UIManager.instance.InvenSlotList.Count)
            {
                Debug.Log(" 아이템 종류가 넘 많아!");
                break;
            }

            UIManager.instance.InvenSlotList[i].Set(sResources[i].kind, sResources[i].count);
        }
    }

    public void AddResourceToInventory(Resource _resource)
    {
        //  같은 종류가 있는지 우선 살핀다.
        for (var i = 0; i < UIManager.instance.InvenSlotList.Count; i++)
        {
            if (UIManager.instance.InvenSlotList[i].count > -1 && UIManager.instance.InvenSlotList[i].kind == _resource.ResourceKind)
            {
                UIManager.instance.InvenSlotList[i].AddCount( _resource.count);
                return;
            }
        }

        for (var i = 0; i < UIManager.instance.InvenSlotList.Count; i++)
        {
            if (UIManager.instance.InvenSlotList[i].count == -1)
            {
                UIManager.instance.InvenSlotList[i].Set(_resource.ResourceKind, _resource.count);
                return;
            }
        }
    }
}
