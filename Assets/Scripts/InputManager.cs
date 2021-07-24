using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Player player;

    public int RescoureLayer { get; private set; } // �ڿ�
    private int FishingLayer; // ���� 
    private int BonfireLayer; // ��ں�

    // Start is called before the first frame update
    void Start()
    {
        RescoureLayer = LayerMask.NameToLayer("Resource");
        FishingLayer = LayerMask.NameToLayer("Fish");
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 Ŭ����
        {

            Vector3 transPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            player.SetPlayerFilp(transPos);
      
            RaycastHit2D hit = Physics2D.Raycast(transPos, transform.forward);
            Debug.DrawRay(transPos, Vector3.forward, Color.blue, 1);

            player.TargetPos = new Vector3(transPos.x, transPos.y, 0);
            if (hit)
            {
                int hitLayer = hit.transform.gameObject.layer;

                if (hitLayer == RescoureLayer)
                {
                   // Debug.Log("resource click");
                    player.Collect(hit.collider);
                }
                else if (hitLayer == FishingLayer)
                {
                   // Debug.Log("fish click");
                    player.Fising();
                }
                else if (hitLayer == BonfireLayer)
                {

                }

            }

        }

    }
}
