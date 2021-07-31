using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; } = null;

    public List<Resource> prefabs = new List<Resource>();

    readonly List<Resource> pool = new List<Resource>();

    private void Awake()
    {
        Instance = this;
    }

    public Resource GetObj(ResourceKind resourceKind)
    {
        List<Resource> resources = pool.FindAll((o)=> { return o.ResourceKind == resourceKind; });

        if (resources.Count > 0)
        {
            resources[0].name = $"{resourceKind}";
            pool.Remove(resources[0]);
            return resources[0];
        }
        else
        {
            Resource resource = Instantiate(prefabs[(int)resourceKind]);
            resource.name = $"{resourceKind}";
            resource.transform.SetParent(transform);
            resource.transform.localScale = Vector3.one;
            return resource;
        }
    }

    public void ReleaseObj(Resource resource)
    {
        pool.Add(resource);
        resource.name = $"-----Un_{resource.ResourceKind}-----";
        resource.transform.position = new Vector3(resource.transform.position.x, resource.transform.position.y, -3000);
    }
}
