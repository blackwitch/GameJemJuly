using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Resource> list_MyResource;
    public List<Item> list_MyItem;
    public int capacity = 36;    //�ִ� �κ� �뷮

    private void Start()
    {
        list_MyResource = new List<Resource>();
        list_MyItem = new List<Item>();
    }

    public void AddResourceToInventory(Resource _resource)
    {
        print("ADDResourceToInven");
        if (list_MyResource.Count + list_MyItem.Count < capacity)
        {
            Resource matchResource = list_MyResource.Find(x => x.ResourceKind == _resource.ResourceKind);

            if (matchResource != null)
            {
                matchResource.count ++;
                //resource.tmpro.text = resource.count.ToString();
            }
            else
            {
                list_MyResource.Add(_resource);
                //resource.tmpro.text = _resource.count.ToString();

                UIManager.instance.AddItemToInventoryUI(_resource.gameObject);
            }
        }
    }
}
