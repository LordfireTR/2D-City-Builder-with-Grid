using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_PathFinding : MonoBehaviour
{
    PathFinding pathfinding;
    void Start()
    {
        pathfinding = new PathFinding(22, 10, new Vector3(-55, -25, 0));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pathfinding.GetGrid().GetXY(Camera.main.ScreenToWorldPoint(Input.mousePosition), out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);

            Debug.Log(x + "," + y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y, 0) * 5f + Vector3.one * 2.5f + new Vector3(-55, -25, 0), new Vector3(path[i+1].x, path[i+1].y, 0) * 5f + Vector3.one * 2.5f + new Vector3(-55, -25, 0), Color.yellow, 100f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            pathfinding.GetGrid().GetXY(Camera.main.ScreenToWorldPoint(Input.mousePosition), out int x, out int y);
            
            List<PathNode> neighbours = pathfinding.GetNeighbourList(pathfinding.GetGrid().GetGridObject(x, y));

            foreach (PathNode item in neighbours)
            {
                Debug.Log(item);
            }
        }
    }
}
