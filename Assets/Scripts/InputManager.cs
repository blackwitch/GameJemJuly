using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Player player;
    
    Vector3 target;
    public float speed = 20;
    Vector3[] path;
    int targetIndex = 0;

    public int RescoureLayer { get; private set; } // ????
    private int FishingLayer; // ???? 
    private int BonfireLayer; // ??????
    private int UILayer;
    private int BoatLayer;

    // Start is called before the first frame update
    void Start()
    {
        RescoureLayer = LayerMask.NameToLayer("Resource");
        FishingLayer = LayerMask.NameToLayer("Fish");
        UILayer = LayerMask.NameToLayer("UI");
        BoatLayer = LayerMask.NameToLayer("Boat");
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsDead || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() || GameManager.Instance.bUI) return;
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition; ;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                position = touch.position;
            }
            
            Vector3 transPos = Camera.main.ScreenToWorldPoint(position);
            bool isNeedMove = true;
            if (player.CanCatchFish)
            {
                player.CatchFish();
                return;
            }
        
            RaycastHit2D hit = Physics2D.Raycast(transPos, transform.forward);
            Debug.DrawRay(transPos, Vector3.forward, Color.blue, 1);

            player.TargetPos = new Vector3(transPos.x, transPos.y, 0);
            if (hit)
            {
                int hitLayer = hit.transform.gameObject.layer;

                if (hitLayer == RescoureLayer)
                {
                    if (Vector3.Distance(hit.collider.transform.position,player.transform.position) < 2.0f)
                    {
                        player.Collect(hit.collider);
                        isNeedMove = false;
                    } 
                }
                else if (hitLayer == FishingLayer)
                {
                    Vector2 dir = new Vector2(transPos.x - transform.position.x, transPos.y - transform.position.y);
                    RaycastHit2D fishHit = Physics2D.Raycast(transform.position, dir, 2.0f, 1 << LayerMask.NameToLayer("Fish"));
                    if (fishHit)
                    {
                        isNeedMove = false;
                        player.Fising(hit.collider.GetComponent<Resource>());
                    }
                 
                }
                else if (hitLayer == BonfireLayer)
                {

                }
                else if(hitLayer == UILayer)
                {
                    isNeedMove = false;
                    UIManager.instance.OpenCloseUI(UIManager.instance.combinationUI);
                }
                else if(hitLayer == BoatLayer)
                {
                    isNeedMove = false;
                    GameManager.Instance.LoadExitScene();
                }
            }
            if(isNeedMove)
            {
                if(player.curSceneName != "SeaScene")
                {
                    PathRequestManager.ReqeustPath(transform.position, transPos, OnPathFound);
                 
                }

            }
            player.SetPlayerFilp(transPos);
        }

    }
    
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length == 0) yield break;
        Vector3 currentWayPoint = path[0];

        targetIndex = 0;

        while (true)
        {
            if (transform.position == currentWayPoint)
            {
                ++targetIndex;
                if (targetIndex >= path.Length)
                    yield break;
                currentWayPoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);
            yield return null;
        }
    }

}
