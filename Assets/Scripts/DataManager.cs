using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public enum eItemType
{
    EXPENDABLE,
    HOUSE,
    BOAT,
    WEAPON,
}
[System.Serializable]
public class ItemInfo
{
    public int count = 1;
    public string itemName;
    public string KoreanName;
    public string description;
    public string itemType;
    public int durability = 0;
    public int value = 0;
    public int defense = 0;
    public List<NeedResourceInfo> list_requiredResource;
}

[System.Serializable]
public class ItemInfoList
{
    public List<ItemInfo> items = new List<ItemInfo>();
}

[System.Serializable]
public class NeedResourceInfo
{
    public string ResourceKind;
    public int count;
}

[System.Serializable]
public class ResourceInfo
{
    //    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    //    public ResourceKind ResourceKind { get; set; }
    public string ResourceKind;
    public string KoreanName;
    public int Ap;
};

[System.Serializable]
public class ResourceInfoList
{
    public List<ResourceInfo> resources = new List<ResourceInfo>();
}

[System.Serializable]
public class SlotInfo
{
    public int count;
}

[System.Serializable]
public class ResourceSlot : SlotInfo
{
    public ResourceKind kind;

    public ResourceSlot(ResourceKind _kind, int _count) { base.count = _count;  kind = _kind; }
}

[System.Serializable]
public class ItemSlot : SlotInfo
{
    public string itemName;

    public ItemSlot(string _name, int _count) { base.count = _count; itemName = _name; }

}

[System.Serializable]
public class MyInven
{
    public List<SlotInfo> mySlot = new List<SlotInfo>();
}

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>();
            }
            return _instance;
        }
    }

    public ResourceInfoList resList = new ResourceInfoList();
    public ItemInfoList itemList = new ItemInfoList();
    public MyInven myInven = new MyInven();

    void Awake()
    {
        // todo : ???????? itemdata, resourcedata json ???? ??????
        resList = LoadData<ResourceInfoList>("resource");
        itemList = LoadData<ItemInfoList>("item");
    }

    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public string ReSourceKindToString(ResourceKind kind)
    {
        switch (kind)
        {
            case ResourceKind.WOOD: return "wood";
            case ResourceKind.SAND: return "sand";
            case ResourceKind.CHICKEN: return "chicken";
            case ResourceKind.FLINT: return "flint";
            case ResourceKind.IRON: return "iron";
            case ResourceKind.GOLD: return "gold";
            case ResourceKind.DIAMOND: return "diamond";
            case ResourceKind.TREASURE: return "treasure";
            case ResourceKind.URANIUM: return "uranium";
            case ResourceKind.FISH: return "fish";
            default: return "NONAME";
        }
    }

    public T LoadData<T>(string _dataType)
    {
        string fileName = "";
        switch (_dataType)
        {
            case "item":
                fileName = "items.json";
                break;
            default:
                fileName = "resources.json";
                break;
        }

        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        string data = File.ReadAllText(path);

        return JsonToOject<T>(data);
    }
}
