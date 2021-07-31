using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountText : MonoBehaviour
{
    TextMeshProUGUI tmpro;
    void Start()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
    }

    public void SetCountOfItem()
    {
        if (transform.parent.GetComponent<Resource>() != null)
        {
            foreach (var item in Inventory.instance.sItems)
            {
                if (transform.parent.GetComponent<simpleResource>().kind == item.kind)
                {
                    tmpro.text = item.count.ToString();
                }
            }
        }
        else if (transform.parent.GetComponent<Item>() != null)
        {
            foreach (var item in Inventory.instance.sItems)
            {
                if (transform.parent.GetComponent<Item>().info.itemName == item.name)
                {
                    tmpro.text = item.count.ToString();
                }
            }
        }
    }
}
