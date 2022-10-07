using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_Tiles : MonoBehaviour
{
   MeshMap MeshMap;
   Vector3 meshmapPos = new Vector3(-50, -25, 0);

    void Awake()
    {
        MeshMap = new MeshMap(10, 10, 5, meshmapPos);
        GetComponent<MeshFilter>().mesh = MeshMap.GetMesh();
    }

    void Start()
    {
        MeshMap.UpdateMeshMap();
    }

    void Update()
    {
        
    }
}
