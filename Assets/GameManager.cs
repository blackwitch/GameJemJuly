using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public CollectionSite CollectionSite { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        // �÷��̾ ��� �ִ� ���� Ȯ���Ͽ� CollectionSite�� ������
    }
}
