using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

[Serializable]
public class Serialization<T>
{
    public Serialization(List<T> target) => this.target = target;
    public List<T> target;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance = null;
    string file_path;
    string text_file_name;

    private void Awake()
    {
        Instance = this;
        text_file_name = "SAVEDATA";
        file_path = $"{Application.persistentDataPath}/{text_file_name}.txt";
    }

    private void Start()
    {
    }

    public void SaveResource()
    {
        string json = "";

        List<Resource.Data> datas = new List<Resource.Data>();

        /*        for (int i = 0; i < Inventory.instance.list_MyItem.Count; i++)
                {
                    var obj_data = from data in Inventory.instance.list_MyResource
                                   select data;

                    datas.AddRange(obj_data);
                }*/

        foreach (var item in Inventory.instance.list_MyResource)
        {
            datas.Add(item.data);
        }

        json = JsonUtility.ToJson(new Serialization<Resource.Data>(datas));
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
        string code = Convert.ToBase64String(bytes);

        text_file_name = "SAVERESOURCE";
        file_path = $"{Application.persistentDataPath}/{text_file_name}.txt";
        File.WriteAllText(file_path, code);
        print($"SAVE TO : {file_path}");
    }
    public void SaveItem()
    {
        string json = "";

        List<Item.Data> datas = new List<Item.Data>();

        foreach (var item in Inventory.instance.list_MyItem)
        {
            datas.Add(item.data);
        }

        json = JsonUtility.ToJson(new Serialization<Item.Data>(datas));
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
        string code = Convert.ToBase64String(bytes);

        text_file_name = "SAVEITEM";
        file_path = $"{Application.persistentDataPath}/{text_file_name}.txt";
        File.WriteAllText(file_path, code);
        print($"SAVE TO : {file_path}");
    }

    public void LoadItem()
    {
        if (!File.Exists(file_path)) { return; }

        string code = File.ReadAllText(file_path);
        byte[] bytes = Convert.FromBase64String(code);
        string json = System.Text.Encoding.UTF8.GetString(bytes);

        text_file_name = "SAVEITEM";
        text_file_name += "_LOAD";
        file_path = $"{Application.persistentDataPath}/{text_file_name}.txt";
        File.WriteAllText(file_path, json);

        List<Item.Data> datas = new List<Item.Data>();

        datas = JsonUtility.FromJson<Serialization<Item.Data>>(json).target;

        Inventory.instance.list_MyItem.Clear();
        //Inventory.instance.list_MyItem.AddRange(datas);

        print($"LOAD FROM : {file_path}");
    }
    public void LoadResource()
    {
        if (!File.Exists(file_path)) { return; }

        string code = File.ReadAllText(file_path);
        byte[] bytes = Convert.FromBase64String(code);
        string json = System.Text.Encoding.UTF8.GetString(bytes);

        text_file_name = "SAVERESOURCE";
        text_file_name += "_LOAD";
        file_path = $"{Application.persistentDataPath}/{text_file_name}.txt";
        File.WriteAllText(file_path, json);

        List<Resource.Data> datas = new List<Resource.Data>();

        datas = JsonUtility.FromJson<Serialization<Resource.Data>>(json).target;

        Inventory.instance.list_MyResource.Clear();
        //Inventory.instance.list_MyResource.AddRange(datas);

        print($"LOAD FROM : {file_path}");
    }
}
