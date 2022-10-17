using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if(meshRenderer)
        {
            meshRenderer.enabled = false;
        }
    }

    public GameObject SpawnEnemy(GameObject enemy)
    {
        return Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
    }
}
