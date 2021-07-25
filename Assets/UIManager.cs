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

 

    public void OpenCloseUI(GameObject _targetUI)
    {
        print("OPENCLOSEUI");

        if (!_targetUI.activeInHierarchy)
        {
            GameManager.Instance.bUI = true;
            _targetUI.SetActive(true);
        }
        else
        {
            GameManager.Instance.bUI = false;
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

        print($"{go.name} was Instantiated !");
        item = go.GetComponent<Item>();
    }

    public void AddItemToInventoryUI(GameObject _gameObject,out Resource res)
    {
        GameObject go = Instantiate(_gameObject);
        go.transform.SetParent(invenUI.transform.GetChild(0));
        go.transform.localScale = Vector3.one;
        print($"{go.name} was Instantiated !");
        res = go.GetComponent<Resource>();
    }

    public void RemoveItemFromInventoryUI(GameObject _gameObject)
    {

    }
}
