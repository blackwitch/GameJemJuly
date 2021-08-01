using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Site
{
    public string Name;
    public Vector2 LT;
    public Vector2 RB;
}

public class ResourceManager : MonoBehaviour
{
    public static int MaxResourceCount = 15;
    public static ResourceManager Instance = null;
    public Vector2 size;
    public List<Site> rects = new List<Site>();
    AGrid grid;
    Resource[,] resources;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        grid = GameObject.Find("A*Manager").GetComponent<AGrid>();
        size.x = grid.gridWorldSize.x;
        size.y = grid.gridWorldSize.y;
        resources = new Resource[(int)size.x, (int)size.y];

        for (int i = 0; i < MaxResourceCount; i++)
            CreateRandomResources();
    }

    public IEnumerator CCreateRandomResources()
    {
        StopCoroutine("DelayCall");
        StartCoroutine("DelayCall");
        yield return new WaitForSeconds(1);
        CreateRandomResources();
    }
    void CreateRandomResources()
    {
        Vector2 index = new Vector2(Random.Range(0, (int)size.x), Random.Range(0, (int)size.y));

        int i = 0;
        while (resources[(int)index.x, (int)index.y] != null)
        {
            index = new Vector2(Random.Range(0, (int)size.x), Random.Range(0, (int)size.y));

            if (index == Vector2.zero)  //  플레이어 시작점.
                continue;

            ++i;
            if (i >= size.x * size.y)
            {
                return;
            }
        }

        Vector2 pos = new Vector2(size.x / 2 * -1 + 0.5f, size.y / 2 * -1 + 0.5f);
        pos += index;

        Resource resource = gameObject.AddComponent<Resource>();

        foreach (var site in rects)
        {
            if (site.LT.x < pos.x && pos.x < site.RB.x &&
                site.RB.y < pos.y && pos.y < site.LT.y)
            {
                if (site.Name == "ISLAND")
                {
                    int resIdx = Random.Range(0, 6);
                    switch (resIdx)
                    {
                        case 0:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.WOOD);
                            break;
                        case 1:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.SAND);
                            break;
                        case 2:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.FLINT);
                            break;
                        case 3:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.CHICKEN);
                            break;
                        case 4:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.IRON);
                            break;
                        case 5:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.GOLD);
                            break;
                    }
                }
                else if (site.Name == "SEA")
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.FISH);
                            break;
/*
                        case 1:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.TREASURE);
                            break;
*/
                    }
                }
                else if (site.Name == "MINERAL")
                {
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.IRON);
                            break;
                        case 1:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.GOLD);
                            break;
                        case 2:
                            resource = ObjectPool.Instance.GetObj(ResourceKind.DIAMOND);
                            break;
                    }
                }
                else if (site.Name == "URANIUM")
                {
                    resource = ObjectPool.Instance.GetObj(ResourceKind.URANIUM);
                }
            }
        }

        resource.transform.position = new Vector3(pos.x, pos.y, 0);
        ResourceInfo info = DataManager.instance.resList.resources.Find(e => e.ResourceKind == resource.ResourceKind.ToString());
        if(info != null)
            resource.Ap = info.Ap;
        else
        {
            Debug.Log("Incorrect resource type " + resource.ResourceKind.ToString());
        }

        resources[(int)index.x, (int)index.y] = resource;
        StopCoroutine("DelayCall");
        StartCoroutine("DelayCall");
    }

    IEnumerator DelayCall()
    {
        yield return new WaitForSeconds(1);
        grid.CreateGrid();
        yield return null;
    }
}
