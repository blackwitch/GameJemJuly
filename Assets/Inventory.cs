using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static Inventory _instance;
    public static Inventory instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
            }
            return _instance;
        }
    }

    public List<Resource> myInven;
    public int capacity = 20;    //�ִ� �κ� �뷮

    private void Start()
    {
        myInven = new List<Resource>();
    }
}
