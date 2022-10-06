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

    List<Vector3> pathVectorList = new List<Vector3>();
    int currentPathIndex;
    [SerializeField] float moveSpeed = 1;
    Vector3 startingPosition, debugPosition;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        vertices = new Vector3[4];
        uv = new Vector2[4];
        triangles = new int[6];


        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 5, 0);
        vertices[2] = new Vector3(5, 5, 0);
        vertices[3] = new Vector3(5, 0, 0);

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

        // grid = new Grid<Mesh> (8, 4, 12.5f, new Vector3(-50, -25, 0), () => new Mesh());
        // for (int i = 0; i < 8; i++)
        // {
        //     for (int j = 0; j < 4; j++)
        //     {
        //         grid.SetGridObject(i, j, mesh);
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        if (Input.GetMouseButtonDown(0))
        {
            SetPath(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            // for (int i = 0; i < pathVectorList.Count; i++)
            // {
            //     GetXY(pathVectorList[i], out int x, out int y);
            //     Debug.Log(x + "," + y);
            // }
            // for (int i = 0; i < vertices.GetLength(0); i++)
            // {
            //     vertices[i] += Vector3.up;
            // }
            // mesh.vertices = vertices;
        }

        if (Input.GetMouseButtonDown(1))
        {
            // for (int i = 0; i < vertices.GetLength(0); i++)
            // {
            //     vertices[i] -= Vector3.up;
            // }
            // mesh.vertices = vertices;
            // Debug.Log(transform.position);
        }
        
    }

    public void SetPath(Vector3 targetPosition)
    {
        Vector3 oldPath;
        // GetXY(transform.position, out int x, out int y);
        // Debug.Log("from " + x + "," + y);
        // GetXY(targetPosition, out x, out y);
        // Debug.Log("to " + x + "," + y);
        startingPosition = transform.position;

        if (isMoving)
        {
            oldPath = pathVectorList[currentPathIndex];
            GetXY(oldPath, out int x_, out int y_);
            // Debug.Log("old path: " + x_ + "," + y_);
        }
        else
        {
            oldPath = startingPosition;
        }
        
        if (PathFinding.Instance.FindPath(startingPosition, targetPosition) != null)
        {
            currentPathIndex = 0;
            pathVectorList = PathFinding.Instance.FindPath(oldPath, targetPosition);
        }

        if (pathVectorList == null || pathVectorList.Count < 1)
        {
            // Debug.Log("null?");
            SetPath(transform.position);
        }
    }

    void HandleMovement()
    {
        if (pathVectorList.Count > currentPathIndex)
        {
            isMoving = true;
            Vector3 targetPosition = pathVectorList[currentPathIndex];

            // if (targetPosition != debugPosition)
            // {
            //     debugPosition = targetPosition;
            //     Debug.Log(debugPosition);
            // }

            if (Vector3.Distance(transform.position, targetPosition) > moveSpeed * Time.deltaTime)
            {
                transform.position += (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position = targetPosition;
                currentPathIndex++;
            }
        }
        else
        {
            isMoving = false;
        }
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        PathFinding.Instance.GetGrid().GetXY(worldPosition, out int _x, out int _y);
        x = _x;
        y = _y;
    }
}
