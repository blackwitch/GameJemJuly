using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    static Prefabs _instance;
    public static Prefabs instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Prefabs>();
            }
            return _instance;
        }
    }

    public GameObject wood;
    public GameObject sand;
    public GameObject chicken;
    public GameObject flint;
    public GameObject iron;
    public GameObject gold;
    public GameObject diamond;
    public GameObject treasure;
    public GameObject uranium;
    public GameObject fish;
    public GameObject fishing_rod;
    public GameObject wood_knife;
    public GameObject knife;
    public GameObject bonfire;
    public GameObject fish_dishes;
    public GameObject roasted_chicken;
    public GameObject bottle;
    public GameObject wood_house;
    public GameObject iron_house;
    public GameObject gold_house;
    public GameObject wooden_boat;
    public GameObject boat;
    public GameObject ship;
    public GameObject cruise;
    public GameObject nuclear;
    public GameObject ring;
    public GameObject chicken_dishes;

    void Start()
    {

    }
}
