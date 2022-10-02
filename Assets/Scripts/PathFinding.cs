using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    const int MOVE_STRAIGHT_COST = 10;
    const int MOVE_DIAGONAL_COST = 14;

    Grid<PathNode> grid;
    List<PathNode> openList;
    List<PathNode> closedList;

    public PathFinding(int width, int height, Vector3 originPosition)
    {
        grid = new Grid<PathNode>(width, height, 5f, originPosition, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        if (endNode == null)
        {
            return null;
        }

        openList = new List<PathNode>{ startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();

                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);


            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }
                if (neighbourNode != null)
                {
                    Debug.Log("neighbour" + neighbourNode.x + "," + neighbourNode.y);
                }
                if (currentNode == null)
                {
                    Debug.Log("current");
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);


                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost(); 
                }

                if (!openList.Contains(neighbourNode))
                {
                    openList.Add(neighbourNode);
                }
            }
        }

        //out of nodes on the openList
        return null;
    }

    public List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x >= 1)
        {
            //Left
            neighbourList.Add(grid.GetGridObject(currentNode.x - 1, currentNode.y));
            //DownLeft
            if (currentNode.y >= 1)
            {
                neighbourList.Add(grid.GetGridObject(currentNode.x - 1, currentNode.y - 1));
            }
            //UpLeft
            if (currentNode.y < grid.GetHeight() - 1)
            {
                neighbourList.Add(grid.GetGridObject(currentNode.x - 1, currentNode.y + 1));
            }
        }
        if (currentNode.x < grid.GetWidth() - 1)
        {
            //Right
            neighbourList.Add(grid.GetGridObject(currentNode.x + 1, currentNode.y));
            //DownRight
            if (currentNode.y >= 1)
            {
                neighbourList.Add(grid.GetGridObject(currentNode.x + 1, currentNode.y - 1));
            }
            //UpRight
            if (currentNode.y < grid.GetHeight() - 1)
            {
                neighbourList.Add(grid.GetGridObject(currentNode.x + 1, currentNode.y + 1));
            }
        }
        //Up
        if (currentNode.y < grid.GetHeight() - 1)
        {
            neighbourList.Add(grid.GetGridObject(currentNode.x, currentNode.y + 1));
        }
        //Down
        if (currentNode.y >= 1)
        {
            neighbourList.Add(grid.GetGridObject(currentNode.x, currentNode.y - 1));
        }
        
        return neighbourList;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(endNode);
        PathNode currentNode = endNode;

        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();

        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }

        return lowestFCostNode;
    }
}
