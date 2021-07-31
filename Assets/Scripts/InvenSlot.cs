using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvenSlot : MonoBehaviour
{
    [HideInInspector] public ResourceKind kind;
    [HideInInspector] public int count = -1;
    public TMP_Text txtCount;
    public Image slotImage;

    // Start is called before the first frame update
    void Awake()
    {
        count = -1;
        txtCount.gameObject.SetActive(false);
        slotImage.color = Color.black;
    }

    public void Set(ResourceKind _kind, int _count)
    {
        if (_count == -1)
            return;
        kind = _kind;
        count = _count;
        string kindName = DataManager.instance.ReSourceKindToString(_kind);
        slotImage.sprite = GameObject.Find("Prefabs").transform.Find(kindName).gameObject.GetComponent<Image>().sprite;
        slotImage.color = Color.white;
        txtCount.text = count.ToString();
        txtCount.gameObject.SetActive(true);
    }

    public void SetCount(int _count)
    {
        count = _count;
        txtCount.text = count.ToString();
    }

    public void AddCount(int _count)
    {
        count += _count;
        txtCount.text = count.ToString();
    }

    public void Clear()
    {
        count = -1;
        slotImage.color = Color.black;
        txtCount.gameObject.SetActive(false);
    }
}
