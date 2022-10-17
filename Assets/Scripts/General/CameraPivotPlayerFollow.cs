using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraPivotPlayerFollow : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private BoxCollider[] cameraBoundaries;
    // [SerializeField] private Vector3 positionOffset = new Vector3(-5, 5, -5);

    private Vector3 positionOffset;
    private Quaternion rotationOffset;
    
    private void Start()
    {
        
        playerObject = GameObject.FindGameObjectWithTag("Player");
        cameraBoundaries = GameObject.Find("Camera Net").GetComponentsInChildren<BoxCollider>();
        
        Debug.LogWarning(cameraBoundaries);
        
        // The goal position/angle
        positionOffset = (transform.position - playerObject.transform.position);
        rotationOffset = transform.rotation;
    }

    Quaternion quaternionEMA(Quaternion a, Quaternion b)
    {
        float aw = 0.9f;
        float bw = 0.1f;
        return new Quaternion(a.x * aw + b.x * bw, a.y * aw + b.y * bw, a.z * aw + b.z * bw, a.w * bw + b.w * bw);
    }
    
    void LateUpdate()
    {
        if (playerObject == null)
            return;
        Vector3 newPosition = playerObject.transform.position + positionOffset;
    
        Assert.IsTrue(cameraBoundaries.Length == 2);
        if (cameraBoundaries.Length > 0)
        {
            float shortestDistance = 9999999;
            Vector3 finalPosition = newPosition;
            
            foreach (BoxCollider cameraBoundary in cameraBoundaries)
            {
                if (cameraBoundary.bounds.Contains(newPosition))
                {
                    // We are OK where we are
                    finalPosition = newPosition;
                    break;
                }
            
                // Debug.Log("Camera tried going out of bounds, correcting.");
                Vector3 potentialPosition = cameraBoundary.bounds.ClosestPoint(newPosition);
                float distance = (potentialPosition - newPosition).magnitude;
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    finalPosition = potentialPosition;
                }
            }
            
            // We also angle the camera down so that the player is centered!
            Quaternion newRotation = Quaternion.LookRotation(playerObject.transform.position - finalPosition);
            transform.rotation = newRotation;
            // if (!cameraBoundary.bounds.Contains(newPosition))
            // {
            //     // Debug.Log("Camera tried going out of bounds, correcting.");
            //     newPosition = cameraBoundary.bounds.ClosestPoint(newPosition);
            //     
            //     // We also angle the camera down so that the player is centered!
            //     Quaternion newRotation = Quaternion.LookRotation(playerObject.transform.position - newPosition);
            //     transform.rotation = newRotation;
            // }

            transform.position = finalPosition;
        }

    }
}
