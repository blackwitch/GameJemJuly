using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

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

    public List<Resource> list_resourceInfo = new List<Resource>();    // ���� ���� ����. json���� �޾ƿ�
    public List<Item> list_itemInfo = new List<Item>();

    void Awake()
    {
        // todo : �������� itemdata, resourcedata json ���� ��ġ��
        list_resourceInfo = LoadData<Resource>("item")["resources"];
        list_itemInfo = LoadData<Item>("item")["items"];
    }

    public Dictionary<string, List<T>> LoadData<T>(string _dataType)
    {
        string fileName = "";
        switch (_dataType)
        {
            case "item":
                {
                    fileName = "itemData.json";
                }
                break;
            default:
                {
                    Debug.LogError("�߸��� ������ Ÿ�� �Է�");
                }
                break;
        }

        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        string data = File.ReadAllText(path);


        var deserializedData = JsonConvert.DeserializeObject<Dictionary<string, List<T>>>(data);
        return deserializedData;
    }
}
