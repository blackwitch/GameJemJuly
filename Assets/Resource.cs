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
}

public enum CollectionSite
{
    ISLAND,
    CAVE,
    SEA,
}

/// <summary>
/// �Ƹ� Collection�Լ� ������ RayCast�� ���콺 Ŭ�� �����Ͽ� ���� ������
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


    //acquisition probability�� ���ڸ� ����� ȹ�� Ȯ��
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
        for (int i = 0; i < DataManager.instance.list_resourceInfo.Count; i++)
        {
            Resource obj = DataManager.instance.list_resourceInfo[i];

            if (obj.ResourceKind == this.ResourceKind)
            {
                InitializeResource(obj);
            }
        }

        //tmpro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //tmpro.text = count.ToString();
    }

    void InitializeResource(Resource _resource)
    {
        KoreanName = _resource.KoreanName;
        count = _resource.count;
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
        print($"rand value is [{rand}]");

        if (Ap > rand)
        {
            Inventory.instance.AddResourceToInventory(this);
            StartCoroutine(ResourceManager.Instance.CCreateRandomResources(ResourceKind));
            print("1�е� �ٽ� �����ǰ� ���� obj�� ObjectPool�� ���ư�");
            ObjectPool.Instance.ReleaseObj(this);
            return true;
        }

        return false;
    }
}
