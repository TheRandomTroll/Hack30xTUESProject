using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationMesh : MonoBehaviour {

    [SerializeField] NavMeshSurface navMeshSurface; // TODO: remove serialize field!
    
	void Start () {
        navMeshSurface = GetComponent<NavMeshSurface>();
	}

    public void BuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
