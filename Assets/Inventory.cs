using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Resource> myInventory;
    public int capacity = 20;    //�ִ� �κ� �뷮

    private void Start()
    {
        myInventory = new List<Resource>();
    }
}
