using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour
{
    // this component should be added to the game object. It is then captured in the Start method
    private NavMeshSurface surface;
    
    // this is the road object. MeshRenderer becomes enabled when Vuforia starts tracking
    public MeshRenderer mesh;

    // the object which initialized road - that is ImageTarget which activated arch and the road
    public GameObject tracker;

    // Start is called before the first frame update
    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
    }

    // this method is called when image target is found
    public void UpdateSurface() {
        StartCoroutine(WaitAndUpdate());
    }

    // we need to wait until the road is not at zero position
    // before generating mesh for aligned with the position of the 
    // road meshed in the aumented view
    // 
    IEnumerator WaitAndUpdate() {
        while (!mesh.enabled) {
            Debug.Log("Mesh is not enabled. Waiting...");
            yield return 0; // waiting for 1 frame and then execute code below
        }

        while (tracker.transform.position == Vector3.zero) {
            Debug.Log("Tracked object is at zero point. Waiting...");
            yield return 0; // waiting for 1 frame and then execute code below
        }
        Debug.Log("Mesh is enabled. Building mesh at a runtime. Tracker at position " + tracker.transform.position);
        surface.BuildNavMesh();        
    }
}
