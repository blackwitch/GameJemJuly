using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    PathRequestManager requestManager;
    AGrid grid;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<AGrid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        ANode startNode = grid.GetNodeFromWorldPoint(startPos);
        ANode targetNode = grid.GetNodeFromWorldPoint(targetPos);

        Vector3 add = new Vector3(0, 0, 0);

        Vector3 temp = targetPos - startPos;

        if (Mathf.Abs(temp.x) < 1 && Mathf.Abs(temp.y) < 1)
        {
            if (targetPos.x < startPos.x) add.x = 1;
            else add.x = -1;

            if (targetPos.y > startPos.y) add.y = -1;
            else add.y = 1;
        }
        else
        {
            if (temp.x > temp.y)
            {
                if (targetPos.x < startPos.x) add.x = 1;
                else add.x = -1;
            }
            else
            {
                if (targetPos.y > startPos.y) add.y = -1;
                else add.y = 1;

            }
        }

        int index = 0;

        while (!targetNode.isWalkAble)
        {
            ++index;
            targetPos += add;
            targetNode = grid.GetNodeFromWorldPoint(targetPos);
            if (targetNode.isWalkAble)
                print("isWalkAble");
            if (index >= 500)
            {
                print("∫ÒªÛ≈ª√‚!");
                break;
            }
        }

        if (startNode.isWalkAble && targetNode.isWalkAble)
        {
            List<ANode> openList = new List<ANode>();
            HashSet<ANode> closedList = new HashSet<ANode>();
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                ANode currentNode = openList[0];
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (ANode n in grid.GetNeighbours(currentNode))
                {
                    if (!n.isWalkAble || closedList.Contains(n))
                        continue;

                    int newCurrentToNeighbourCost = currentNode.gCost + GetDistanceCost(currentNode, n);
                    if (newCurrentToNeighbourCost < n.gCost || !openList.Contains(n))
                    {
                        n.gCost = newCurrentToNeighbourCost;
                        n.hCost = GetDistanceCost(n, targetNode);
                        n.parentNode = currentNode;

                        if (!openList.Contains(n))
                            openList.Add(n);
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }

        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(ANode startNode, ANode endNode)
    {
        List<ANode> path = new List<ANode>();
        ANode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<ANode> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].grid.x - path[i].grid.x, path[i - 1].grid.y - path[i].grid.y);
            if (directionNew != directionOld)
                waypoints.Add(path[i].worldPos);
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistanceCost(ANode node1, ANode node2)
    {
        Vector2Int dist = new Vector2Int(Mathf.Abs(node1.grid.x - node2.grid.x), Mathf.Abs(node1.grid.y - node2.grid.y));
        if (dist.x > dist.y)
            return 14 * dist.y + 10 * (dist.x - dist.y);
        return 14 * dist.x + 10 * (dist.y - dist.x);
    }
}
