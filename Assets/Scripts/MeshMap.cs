using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMap
{
    public Grid<MeshNode> grid;
    Mesh mesh;
    Vector2[] uv;

    public MeshMap(int width, int height, float cellSize, Vector3 originPosition)
    {
        grid = new Grid<MeshNode>(width, height, cellSize, originPosition, (Grid<MeshNode> g, int x, int y) => new MeshNode(g, x, y));
        mesh = new Mesh();
        Vector2[] defaultUV = new Vector2[4];
        defaultUV[0] = new Vector2(.5f, .5f);
        defaultUV[1] = new Vector2(.5f, 1f);
        defaultUV[3] = new Vector2(1f, .5f);
        defaultUV[2] = new Vector2(1f, 1f);


        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                grid.GetGridObject(x, y).SetNodeUV(defaultUV);
            }
        }
    }

    public void UpdateMeshMap()
    {
        Debug.Log(null);
        CreateEmptyMeshArray(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Vector2[] NodeUV = grid.GetGridObject(x, y).GetNodeUV();
                Debug.Log(x + "," + y);
                Debug.Log(NodeUV[0]);
                Debug.Log(NodeUV[3]);
                int index = x * grid.GetHeight() + y;

                int indexV = 4 * index;
                int indexT = 6 * index;

                vertices[indexV + 0] = grid.GetCellPosition(x, y);
                vertices[indexV + 1] = grid.GetCellPosition(x, y) + new Vector3(0, 1, 0) * grid.GetCellSize();
                vertices[indexV + 2] = grid.GetCellPosition(x, y) + new Vector3(1, 1, 0) * grid.GetCellSize();
                vertices[indexV + 3] = grid.GetCellPosition(x, y) + new Vector3(1, 0, 0) * grid.GetCellSize();

                
                uv[indexV + 0] = NodeUV[0];
                uv[indexV + 1] = NodeUV[1];
                uv[indexV + 2] = NodeUV[2];
                uv[indexV + 3] = NodeUV[3];
                

                triangles[indexT + 0] = indexV + 0;
                triangles[indexT + 1] = indexV + 1;
                triangles[indexT + 2] = indexV + 2;
                triangles[indexT + 3] = indexV + 0;
                triangles[indexT + 4] = indexV + 2;
                triangles[indexT + 5] = indexV + 3;
            }
        }
            this.uv = uv;
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
    }

    public void SetMeshUV(int x, int y, Vector2[] uv)
    {
        grid.GetGridObject(x, y).SetNodeUV(uv);
        UpdateMeshMap();
    }

    public void SetMeshUV(Vector3 worldPosition, Vector2[] uv)
    {
        grid.GetXY(worldPosition, out int x, out int y);
        SetMeshUV(x, y, uv);
    }

    public Mesh GetMesh()
    {
        return mesh;
    }

    private void CreateEmptyMeshArray(int quadCount, out Vector3[] vertices, out Vector2[] uv, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uv = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }
}
