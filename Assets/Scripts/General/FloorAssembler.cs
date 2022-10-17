using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FloorAssembler : MonoBehaviour
{

    public List<MeshFilter> meshes;
    public Bounds localBounds;
    public Bounds sharedBounds;

    void Start()
    {
        meshes = GetComponentsInChildren<MeshFilter>().ToList();
        
        foreach (MeshFilter mesh in meshes)
        {
            mesh.GameObject().AddComponent<BoxCollider>();
        }
        
        // idk why it has to be done like this but referencing any earlier deletes the component
        
        foreach (MeshFilter mesh in meshes)
        {
            BoxCollider collider = mesh.GameObject().AddComponent<BoxCollider>();
            collider.enabled = false;
            
            // breaks things, but is necessary. pain.
            // mesh.sharedMesh.RecalculateBounds();

            localBounds = mesh.mesh.bounds;
            sharedBounds = mesh.sharedMesh.bounds;
            
            Bounds bounds = mesh.mesh.bounds;

            // either of these works ONLY if mesh.sharedMesh.RecalculateBounds() is not called
            bounds.size = collider.GameObject().transform.rotation * mesh.sharedMesh.bounds.size;
            //bounds.size = collider.GameObject().transform.rotation * mesh.mesh.bounds.size;

            collider.center = Vector3.zero;
            collider.size = bounds.size;

            collider.enabled = true;
        }
    }
}
