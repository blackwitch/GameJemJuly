using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{
    Dictionary<string, List<Resource>> dic_CombinationManual;     // ���� ���� ����. json���� �޾ƿ�

    // ���� �������� üũ
    public bool CheckCombination(string _itemName)
    {
        foreach (var requiredResource in dic_CombinationManual[_itemName])
        {
            Resource resource = Inventory.instance.myInven.Find(x => x.ResourceKind == requiredResource.ResourceKind);
            if (resource == null || resource.count < requiredResource.count)
                return false;
        }
        return true;
    }

    public void CombineResources(string _itemName)
    {
        if (CheckCombination(_itemName))
        {
            //���հ����Ұ�� ���� ���� ���� �ڵ�
        }
    }
}