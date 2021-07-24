using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
    public GameObject invenUI;
    public GameObject combinationUI;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OpenCloseUI(GameObject _targetUI)
    {
        print("OPENCLOSEUI");

        if (!_targetUI.activeInHierarchy)
        {
            _targetUI.SetActive(true);
        }
        else
        {
            _targetUI.SetActive(false);
        }
    }
    public void UpdateItemCount(string itemName, int count)
    {
        GameObject temp = invenUI.transform.GetChild(0).Find(itemName).gameObject;
        
        temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count.ToString();
    }
    public void AddItemToInventoryUI(GameObject _gameObject, out Item item)
    {
        GameObject go = Instantiate(_gameObject);
        go.transform.SetParent(invenUI.transform.GetChild(0));
        go.transform.localScale = Vector3.one;
        //TextMeshProUGUI tmpro = go.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //tmpro.text = _gameObject.GetComponent<Resource>().count.ToString();
        //temp.transform.SetParent(GameObject.Find("Canvas").transform);
        print($"{go.name} was Instantiated !");
        item = go.GetComponent<Item>();
    }

    public void AddItemToInventoryUI(GameObject _gameObject,out Resource res)
    {
        GameObject go = Instantiate(_gameObject);
        go.transform.SetParent(invenUI.transform.GetChild(0));
        go.transform.localScale = Vector3.one;
        //TextMeshProUGUI tmpro = go.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //tmpro.text = _gameObject.GetComponent<Resource>().count.ToString();
        //temp.transform.SetParent(GameObject.Find("Canvas").transform);
        print($"{go.name} was Instantiated !");
        res = go.GetComponent<Resource>();
    }

    public void RemoveItemFromInventoryUI(GameObject _gameObject)
    {

    }
}
