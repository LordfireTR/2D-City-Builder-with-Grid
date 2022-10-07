using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshNode
{
    Grid<MeshNode> grid;
    int x, y;
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;

    public MeshNode(Grid<MeshNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }
}
