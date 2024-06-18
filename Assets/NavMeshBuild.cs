using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshBuild : MonoBehaviour
{
    private NavMeshSurface nav;

    void Awake()
    {
        nav = GetComponent<NavMeshSurface>();
        BuildNavMesh();
    }

    [Button]
    public void BuildNavMesh()
    {
        nav.BuildNavMesh();
    }

}
