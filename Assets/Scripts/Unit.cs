using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Vector3 target;
    public float speed = 20;
    Vector3[] path;
    int targetIndex = 0;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            target = Camera.main.ScreenToWorldPoint(touch.position);
            PathRequestManager.ReqeustPath(transform.position, target, OnPathFound);
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

    public void StopMove()
    {
        StopCoroutine("FollowPath");
    }
}
