using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public CollectionSite CollectionSite { get; private set; }

    private int _day = 1;
    public int Day
    {
        get => _day;
        set
        {
            _day = value;
        }
    }
    private float OneDay = 600.0f;

    public float CurTime
    {
        get => _curTime;
        set
        {
            _curTime = value;
            // 10���� ������ �Ϸ�.
            if (_curTime > OneDay)
            {
                Day++;
                _curTime = 0;
            }
            
        }
    }
    private float _curTime = 0;
    public bool isPlaying = true;


    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(isPlaying) CurTime += Time.deltaTime;
        // �÷��̾ ��� �ִ� ���� Ȯ���Ͽ� CollectionSite�� ������.
    }
    
}
