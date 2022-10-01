using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_Mesh : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;
    Grid<Mesh> grid;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        vertices = new Vector3[4];
        uv = new Vector2[4];
        triangles = new int[6];


        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 10, 0);
        vertices[2] = new Vector3(10, 10, 0);
        vertices[3] = new Vector3(10, 0, 0);

        uv[0] = new Vector2(.5f, .5f);
        uv[1] = new Vector2(.5f, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, .5f);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh = mesh;

        grid = new Grid<Mesh> (8, 4, 12.5f, new Vector3(-50, -25, 0), () => new Mesh());
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                grid.SetGridObject(i, j, mesh);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                vertices[i] += Vector3.up;
            }
            mesh.vertices = vertices;
        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                vertices[i] -= Vector3.up;
            }
            mesh.vertices = vertices;
        }
    }
}
