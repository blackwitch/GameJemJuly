using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int Day; //���� ��¥
    public int windchance = 33; // �� Ÿ�� �ٶ��� Ȯ��
    public Player player;
    private float time;
    private int PriateDay;
    private bool mainisland;
    private bool ship;
    // Start is called before the first frame update
    void Start()
    {
        PriateDay = 7 + (Random.Range(1, 6) - 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 600)
        {
            Day++;
            time = 0;
            if (ship)
            {
                if (Random.Range(1, 101) > windchance)
                {
                    //����� �� �ý����� ���ܾ� ����� ������ �մϴ�.
                }
            }
            if (Day >= PriateDay)
            {
                PriateEvent();
                PriateDay = (Day + 10) + (Random.Range(1, 6) - 3);
            }
        }
        if (mainisland)
            time += Time.deltaTime;
        else if (ship)
        {
            time += Time.deltaTime * 10;
        }
    }
    void PriateEvent()
    {
        if (ship)
        {
            //�� �α��� ��ü���� �ý����� ������ �г�Ƽ�� �ο��ҵ� �մϴ�.
        }
        else
        {
            player.Invoke("Pirate", 0);
        }
    }
}
