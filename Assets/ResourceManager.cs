using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance = null;
    public Vector2Int size;
    Resource[,] resources;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        resources = new Resource[size.x, size.y];
    }

    public IEnumerator CCreateRandomResources(ResourceKind resourceKind)
    {
        yield return new WaitForSeconds(60);
        CreateRandomResources(resourceKind);
    }
    void CreateRandomResources(ResourceKind resourceKind)
    {
        AGrid grid = GameObject.Find("A*Manager").GetComponent<AGrid>();

        //grid.gridWorldSize�� ������ ���� ������ ���� ����
        //grid.gridWorldSize�� x�� 20�̰� y�� 20�̶��, x�� -10,10 �׸��� y�� -10, 10�� ������ ������

        Resource resource = new Resource();

        Vector2Int pos = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));

        //�ϴ� �������� pos�� x,y�� 0.5�� ���ϰ� ���� ����
        //�׷��� -9.5 ~ 9.5������ ������ ������

        resource.transform.position = new Vector3(pos.x, pos.y);

        resources[pos.x, pos.y] = resource;
    }
}
