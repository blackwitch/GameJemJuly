using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    public LayerMask unWalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public ANode[,] grid;

    public float nodeDiameter;
    public Vector2Int gridSize;

    public List<ANode> path = new List<ANode>();

    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSize.x = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSize.y = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public void CreateGrid()
    {
        print("CreateGrid");
        grid = new ANode[gridSize.x, gridSize.y];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        Vector3 worldPoint;
        bool walkable = false;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint - new Vector3(0, 0, 0.5f), Vector3.forward, 1, unWalkableMask);
                //Debug.DrawRay(worldPoint - new Vector3(0, 0, 0.5f), Vector3.forward, Color.red, 1f);
                if (hit.collider == null)
                    walkable = true;
                else
                    walkable = false;
                grid[x, y] = new ANode(walkable, worldPoint, x, y);
            }
        }
    }

    public List<ANode> GetNeighbours(ANode node)
    {
        List<ANode> neighbours = new List<ANode>();

        Vector2Int check;

        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                if (x == 0 && y == 0) continue;
                check = new Vector2Int(node.grid.x + x, node.grid.y + y);
                if (check.x >= 0 && check.x < gridSize.x && check.y >= 0 && check.y < gridSize.y)
                {
                    neighbours.Add(grid[check.x, check.y]);
                }
            }
        }

        return neighbours;
    }

    public ANode GetNodeFromWorldPoint(Vector3 worldPos)
    {
        Vector2 percent = new Vector3((worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x, (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y);
        percent.x = Mathf.Clamp01(percent.x);
        percent.y = Mathf.Clamp01(percent.y);

        Vector2Int pos = new Vector2Int(Mathf.RoundToInt((gridSize.x - 1) * percent.x), Mathf.RoundToInt((gridSize.y) * percent.y));
        return grid[pos.x, pos.y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        return;
        /*
        if (grid != null)
        {
            foreach (var n in grid)
            {
                if (!n.isWalkAble)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(n.worldPos, Vector3.one * nodeDiameter);
                }
            }
        }
        */
    }
}
