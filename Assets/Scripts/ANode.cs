using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode
{
    public ANode(bool isWalkAble, Vector3 worldPos, int gridX, int gridY)
    {
        this.isWalkAble = isWalkAble;
        this.worldPos = worldPos;
        grid.x = gridX;
        grid.y = gridY;
    }
    public bool isWalkAble;
    public Vector3 worldPos;
    public Vector3Int grid;

    public int gCost;
    public int hCost;
    public ANode parentNode;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
