using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationMesh : MonoBehaviour {

    [SerializeField] NavMeshSurface navMeshSurface; // TODO: remove serialize field!
    [SerializeField] private bool bakeAtStart = false;
    
	void Start () {
        navMeshSurface = GetComponent<NavMeshSurface>();
        if(bakeAtStart)
        {
            BuildNavMesh();
        }
	}

    public void BuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
