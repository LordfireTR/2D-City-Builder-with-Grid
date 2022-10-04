using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    Grid<PathNode> grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isWalkable;
    public PathNode cameFromNode;
    public List<PathNode> neighbourList;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isWalkable = true;
    }

    public void GetNeighbourList()
    {
        neighbourList = new List<PathNode>();

        if (x >= 1)
        {
            //Left
            neighbourList.Add(grid.GetGridObject(x - 1, y));
            //DownLeft
            if (y >= 1)
            {
                neighbourList.Add(grid.GetGridObject(x - 1, y - 1));
            }
            //UpLeft
            if (y < grid.GetHeight() - 1)
            {
                neighbourList.Add(grid.GetGridObject(x - 1, y + 1));
            }
        }
        if (x < grid.GetWidth() - 1)
        {
            //Right
            neighbourList.Add(grid.GetGridObject(x + 1, y));
            //DownRight
            if (y >= 1)
            {
                neighbourList.Add(grid.GetGridObject(x + 1, y - 1));
            }
            //UpRight
            if (y < grid.GetHeight() - 1)
            {
                neighbourList.Add(grid.GetGridObject(x + 1, y + 1));
            }
        }
        //Up
        if (y < grid.GetHeight() - 1)
        {
            neighbourList.Add(grid.GetGridObject(x, y + 1));
        }
        //Down
        if (y >= 1)
        {
            neighbourList.Add(grid.GetGridObject(x, y - 1));
        }
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void SetIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
