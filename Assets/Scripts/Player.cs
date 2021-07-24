using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    Image HpGage;
    [SerializeField]
    Image ThirstGage;

    Animator playerAnimator;
    SpriteRenderer playerRenderer;

    public enum PlayerState
    {
        IDLE,
        MOVE,
        COLLECT,
        FISHING,
        DIE,
        SWIMING,
        SWIMING_CLLECT,
        SWIMING_DIE,
    }


    //�̵��ӵ�
    public float _speed = 10.0f;
    [SerializeField]
    private int MaxHp = 100;
    [SerializeField]
    private int MaxThirst = 100;
    //ü��
    private int _hp;

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
            HpGage.fillAmount = (float)(_hp) / (float)(MaxHp);
        }
    }

    //����
    private int _thirst;

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
            ThirstGage.fillAmount = (float)(_thirst) / (float)(MaxThirst);
        }
    }
    [SerializeField]
    //ü��, �񸶸� �����ֱ�
    private float _decreseTime = 1.0f;

    [SerializeField]
    private float collectDelay = 1.0f;

    PlayerState _curState;
    public PlayerState CurState
    {
        get => _curState;
        set
        {
            if(_curState == value)
            {
                return;
            }
            else
            {
                SetOffAnimation(_curState);
                SetOnAnimation(value);
                _curState = value;
            }
        }
    }


    private Vector3 _targetPos;
    public Vector3 TargetPos
    {
        get => _targetPos;
        set 
        { 
            _targetPos = value;
        }
    }
    private bool _isCollecting = false;


    // Start is called before the first frame update
    void Start()
    {
        _targetPos = transform.position;
        Hp = MaxHp;
        Thirst = MaxThirst;
        InvokeRepeating("DecreaseHpAndThirst", _decreseTime, _decreseTime);
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private Vector3 _prePosition;
    private float _dx = 0;
    private float _dy = 0; 
    // Update is called once per frame
    void Update()
    {
        _dx = transform.position.x - _prePosition.x;
        _dy = transform.position.y - _prePosition.y;
        if(_dx != 0 || _dy != 0)
        {
            if(CurState != PlayerState.MOVE && CurState != PlayerState.IDLE) // �ٸ� �ൿ�߿� �̵������Ƿ� ĵ��.
            {
                StopAllCoroutines();
            }
            CurState = PlayerState.MOVE;
            
        }
        else
        {
            if(CurState == PlayerState.MOVE) CurState = PlayerState.IDLE;
        }

        _prePosition = transform.position;
        MoveToTarget();

    }

    public void SetPlayerFilp(Vector3 tarPos)
    {
        playerRenderer.flipX = tarPos.x > transform.position.x ? true : false;
    }

    private void DecreaseHpAndThirst()
    {
        Hp--;
        Thirst--;
    }

    void Die()
    {
        
    }


    public void Collect(Collider2D res)
    {

        if (GetComponent<CircleCollider2D>().IsTouching(res))
        {
            TargetPos = transform.position;
            StartCoroutine(CollectSomeThing(res.GetComponent<Resource>()));
        }
        
    }

    public void SetOffAnimation(PlayerState state)
    {
        if(state == PlayerState.MOVE)
        {
            playerAnimator.SetBool("IsWalk", false);
        }
        else if(state == PlayerState.COLLECT)
        {
            playerAnimator.SetBool("IsCollect", false);
        }
    }
    
    public void SetOnAnimation(PlayerState state)
    {
        if (state == PlayerState.MOVE)
        {
            playerAnimator.SetBool("IsWalk", true);
        }
        else if (state == PlayerState.COLLECT)
        {
            playerAnimator.SetBool("IsCollect", true);
        }

    }

    //���� ä���� ��
    IEnumerator CollectSomeThing(Resource res)
    {
        //ä�� �ִϸ��̼� ���.
        CurState = PlayerState.COLLECT;
        Debug.Log("collect something..");
        yield return new WaitForSeconds(collectDelay);
        res.Collection();
        CurState = PlayerState.IDLE;

    }

    void MoveToTarget()
    {
     
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, Time.deltaTime * _speed);
        
    }


}
