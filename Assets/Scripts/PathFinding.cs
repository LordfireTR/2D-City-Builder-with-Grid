using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    Grid<PathNode> grid;

    public PathFinding(int width, int height)
    {
        grid = new Grid<PathNode>(width, height, 5f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }
}
