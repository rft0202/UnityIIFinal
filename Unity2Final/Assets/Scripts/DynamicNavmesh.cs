using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class DynamicNavmesh : MonoBehaviour
{
    [SerializeField]
    bool everyFrame = false;
    NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        StartCoroutine(buildSurface());
    }

    // Update is called once per frame
    void Update()
    {
        if(everyFrame)
            surface.BuildNavMesh();
    }

    public void BuildSurface()
    {
        StartCoroutine(buildSurface());
    }

    IEnumerator buildSurface()
    {
        yield return new WaitForSeconds(3);
        surface.BuildNavMesh();
        StartCoroutine(buildSurface());
    }
}
