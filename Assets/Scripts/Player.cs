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
        FISHING_CATCH,
        DIE,
        SWIMING,
        SWIMING_CLLECT,
        SWIMING_DIE,
    }


    //????????
    public float _speed = 10.0f;
    [SerializeField]
    private int MaxHp = 100;
    [SerializeField]
    private int MaxThirst = 100;
    //????
    private int _hp;
    private bool bGetTreasureBox = false;

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

    //????
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
    //????, ?????? ????????
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
        bGetTreasureBox = false;
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
            if(CurState != PlayerState.MOVE && CurState != PlayerState.IDLE) // ???? ???????? ???????????? ????.
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
            print("IsTouching res");
            TargetPos = transform.position;
            StartCoroutine(CollectSomeThing(res.GetComponent<Resource>()));
        }
        
    }

    public void SetOffAnimation(PlayerState state)
    {
        if (!playerAnimator)
            return;
        if(state == PlayerState.MOVE)
        {
            playerAnimator.SetBool("IsWalk", false);
        }
        else if(state == PlayerState.COLLECT)
        {
            playerAnimator.SetBool("IsCollect", false);
        }
        else if (state == PlayerState.FISHING)
        {
            playerAnimator.SetBool("Fishing", false);
        }
        else if (state == PlayerState.FISHING)
        {
            playerAnimator.SetBool("FishingCatch", false);
        }

    }

    public void SetOnAnimation(PlayerState state)
    {
        if (!playerAnimator)
            return;

        if (state == PlayerState.MOVE)
        {
            playerAnimator.SetBool("IsWalk", true);
        }
        else if (state == PlayerState.COLLECT)
        {
            playerAnimator.SetBool("IsCollect", true);
        }
        else if (state == PlayerState.FISHING)
        {
            playerAnimator.SetBool("Fishing", true);
        }
        else if (state == PlayerState.FISHING)
        {
            playerAnimator.SetBool("FishingCatch", true);
        }
    }

    //?????? ?????? ??
    IEnumerator CollectSomeThing(Resource res)
    {
        //???? ?????????? ????.
        CurState = PlayerState.COLLECT;
        Debug.Log("collect something..");
        yield return new WaitForSeconds(collectDelay);
        Debug.Log($"collect something finished : time ({collectDelay})");
        res.Collection();
        CurState = PlayerState.IDLE;

    }

    public void Fising()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, 1 << LayerMask.NameToLayer("Fish"));
        if (hit)
        {
            Debug.Log("fish!");
            TargetPos = transform.position;
            CurState = PlayerState.FISHING;
        }
    }


    void MoveToTarget()
    {
     
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, Time.deltaTime * _speed);
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Mob")
        {
            StartCoroutine(onCollision());
        }
        else if (col.gameObject.tag == "Exit_to_Main")
        {
            Debug.Log("Go to SooBin");
            UnityEngine.SceneManagement.SceneManager.LoadScene("SooBin");
        }
        else if (col.gameObject.tag == "Air_Pocket")
        {
            //  Th ??.. 
        }else if(col.gameObject.tag == "Treasure_Box_In_Sea" && bGetTreasureBox == false)
        {
            //  ?? ?? ????.
            bGetTreasureBox = true;
            col.gameObject.GetComponentInChildren<treasurebox_open_event>().StartEvent();
        }
    }

    IEnumerator onCollision()
    {
        //   Hp ?? ??

        //  ?????... UI ??!


        yield return new WaitForFixedUpdate();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f,0f,0f);
        float t = Time.deltaTime;
        while (true)
        {
            t += Time.deltaTime;
            if (t > 1.2f)
            {
                sr.color = new Color(1f, 1f, 1f);
                Hp = 0;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
