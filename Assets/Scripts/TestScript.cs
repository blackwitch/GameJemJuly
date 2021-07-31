using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("asdasd");
            Inventory.instance.AddResourceToInventory(GameObject.Find("Prefabs").transform.GetChild(2).GetComponent<Resource>());

            Debug.Log(Inventory.instance.sResources[0].count);
        }
    }
}