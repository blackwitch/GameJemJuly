using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OpenCloseUI(GameObject _targetUI)
    {
        if (!_targetUI.activeInHierarchy)
        {
            _targetUI.SetActive(true);
        }
        else
        {
            _targetUI.SetActive(false);
        }
    }

    public void AddItemToInventoryUI(GameObject _gameObject)
    {
        GameObject go = Instantiate(_gameObject);
        go.transform.SetParent(invenUI.transform.GetChild(0));
        //temp.transform.SetParent(GameObject.Find("Canvas").transform);
        print($"{go.name} was Instantiated !");

    }
}
