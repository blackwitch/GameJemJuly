using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationUI : MonoBehaviour
{
    public GameObject targetItem;
    public Combination combination;

    void Start()
    {
        combination = FindObjectOfType<Combination>();
        ButtonIsInteractable();
    }

    bool ButtonIsInteractable()
    {
        if (combination.CheckCombination(targetItem.GetComponent<Item>().info.itemName))
        {
            return true;
        }
        GetComponent<Button>().interactable = false;
        return false;
    }
}
