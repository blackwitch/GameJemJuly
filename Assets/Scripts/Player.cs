using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�̵��ӵ�
    public float _speed = 10.0f;
   
    //ü��
    private int _hp = 100;
    public int Hp
    {
        get => _hp;
        set
        {
            if (value <= 0)
            {
                _hp = 0;
                Die();
            }
            else if (value > 100) _hp = 100;
            else _hp = value;
        }
    }

    //����
    private int _thirst = 100;

    public int Thirst
    {
        get => _thirst;
        set
        {
            if (value <= 0)
            {
                _thirst = 0;
                Die();
            }
            else if (value > 100) _thirst = 100;
            else _thirst = value;
        }
    }

    //�ð��� ���� ü�� ���� �ӵ�
    public float HpDecreaseSpeed;
    //�ð��� ���� ���� ���� �ӵ�
    public float ThirstDecreaseSpeed;


    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 transPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos = new Vector3(transPos.x, transPos.y, 0);
        }
        MoveToTarget();


    }

    
    void Die()
    {

    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * _speed);
    }
}
