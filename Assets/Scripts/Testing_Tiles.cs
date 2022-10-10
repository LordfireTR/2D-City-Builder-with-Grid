using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_Tiles : MonoBehaviour
{
    MeshMap MeshMap;
    Vector3 meshmapPos = new Vector3(-50, -25, 0);

    [SerializeField] Vector2[] uvNew = new Vector2[4];


    void Awake()
    {
        MeshMap = new MeshMap(4, 2, 5, meshmapPos);
        GetComponent<MeshFilter>().mesh = MeshMap.GetMesh();
    }

    void Start()
    {
        MeshMap.UpdateMeshMap();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MeshMap.grid.GetXY(Camera.main.ScreenToWorldPoint(Input.mousePosition), out int x, out int y);

            MeshMap.SetMeshUV(worldPosition, uvNew);
        }
    }
}
