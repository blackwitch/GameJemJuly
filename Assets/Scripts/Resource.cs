using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ResourceKind
{
    WOOD,
    SAND,
    FLINT,
    IRON,
    GOLD,
    DIAMOND,
    URANIUM,
    CHICKEN,
    FISH,
    TREASURE,
    BOTTLE,
}

public enum CollectionSite
{
    ISLAND,
    CAVE,
    SEA,
}

/// <summary>
/// ???? Collection???? ?????? RayCast?? ?????? ???? ???????? ???? ??????
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Resource : MonoBehaviour
{
    public ResourceKind ResourceKind;
    public CollectionSite CollectionSite { get; set; }

    public int count = 1;
    public string KoreanName;
    Sprite sprite;
    [HideInInspector]
    public TextMeshProUGUI tmpro;


    //acquisition probability?? ?????? ?????? ???? ????
    int _Ap;
    public int Ap
    {
        get
        {
            return _Ap;
        }
        set
        {
            _Ap = Mathf.Min(101, Mathf.Max(-1, value));
        }
    }

    public void Start()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        for (int i = 0; i < DataManager.instance.resList.resources.Count; i++)
        {
            ResourceInfo obj = DataManager.instance.resList.resources[i];

            if (obj.ResourceKind == this.ResourceKind)
            {
                InitializeResource(obj);
            }
        }

        //tmpro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //tmpro.text = count.ToString();
    }

    void InitializeResource(ResourceInfo _resource, int _count = 1)
    {
        KoreanName = _resource.KoreanName;
        count = _count;
        Ap = _resource.Ap;
    }

    public bool Collection()
    {
        if (GameManager.Instance.CollectionSite != CollectionSite)
        {
            print("CollectionSite is Not Equals");
            return false;
        }

        int rand = Random.Range(0, 101);
       // print($"rand value is [{rand}]");

        bool return_val = false;

        /*       if (Ap > rand)
                {
                    Inventory.instance.AddResourceToInventory(this);
                    StartCoroutine(ResourceManager.Instance.CCreateRandomResources());
                    return_val = true;
                }*/
        Inventory.instance.AddResourceToInventory(this);
        StartCoroutine(ResourceManager.Instance.CCreateRandomResources());
        return_val = true;
        ObjectPool.Instance.ReleaseObj(this);

        return return_val;
    }

    public void UpadateCount()
    {
        print($"{this.name} update count");
        tmpro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if(tmpro != null)
            tmpro.text = count.ToString();
    }
}
