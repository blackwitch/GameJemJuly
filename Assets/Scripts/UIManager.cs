using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public GameObject combinationUI, CombDesc;
    public Combination combMng;
    public List<InvenSlot> InvenSlotList;

    private void Awake()
    {
        invenUI.SetActive(false);
        combinationUI.SetActive(false);
        CombDesc.SetActive(false);
    }

    public void OpenCloseUI(GameObject _targetUI)
    {
        if (!_targetUI.activeInHierarchy)
        {
            GameManager.Instance.bUI = true;

            _targetUI.SetActive(true);
            if (_targetUI == combinationUI)
            {
                combMng.UpdateButtons();
            }
        }
        else
        {
            GameManager.Instance.bUI = false;
            _targetUI.SetActive(false);
            if (_targetUI == combinationUI)
                CombDesc.SetActive(false);
        }
    }

    public void AddItemToInventoryUI(GameObject _gameObject, out Item item)
    {
        GameObject go = Instantiate(_gameObject);
        go.transform.SetParent(invenUI.transform.GetChild(0));
        go.transform.localScale = Vector3.one;

        print($"{go.name} was Instantiated !");
        item = go.GetComponent<Item>();
    }

    public void AddItemToInventoryUI(ResourceKind _kind,int _count)
    {
        string kindName = DataManager.instance.ReSourceKindToString(_kind);

        Sprite spriet = GameObject.Find("Prefabs").transform.Find(kindName).gameObject.GetComponent<Image>().sprite;
    }

    public void RemoveItemFromInventoryUI(GameObject _gameObject)
    {

    }
}
