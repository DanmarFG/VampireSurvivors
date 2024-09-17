using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshHelper : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navMesh;
    private void Start()
    {
        navMesh.BuildNavMesh();
    }
}
