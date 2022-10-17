using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsoController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool lookAtScene = false; // if false: look at plane at LookPlaneHeight

    private Vector3 _input;
    private float timeToSit;
    private AnimationController _animctrl;

    private const float StrafeSpeedMultiplier = 0.8f;
    private const float TimeToSit = 2f;

    private Vector3 CameraDirection => Camera.main.transform.forward;

    private float Speed => PlayerBehaviour.Instance.Stats.MovementSpeed;
    private float StrafeSpeed => Speed * StrafeSpeedMultiplier;

    private float LookPlaneHeight => PlayerBehaviour.LookHeight;
    
    private void Start()
    {
        timeToSit = TimeToSit;
        Cursor.lockState = CursorLockMode.None;
        _animctrl = GetComponent<AnimationController>();
    }

    private void Update()
    {
        Move();
        Look();

        if (rb.velocity.magnitude < 0.01)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Move()
    {
        float forwardAxis = Input.GetAxisRaw("Vertical");
        float strafeAxis = Input.GetAxisRaw("Horizontal");
        
        // tranform.forward takes into account current rotation

        // move forward
        if (forwardAxis != 0f || strafeAxis != 0f)
        {
            timeToSit = TimeToSit;
            
            // var positionChangeVector = Speed * forwardAxis * transform.forward 
            //                            + StrafeSpeed * strafeAxis * transform.right;

            var orientation = new Vector3(CameraDirection.x, 0f, CameraDirection.z);
            var normal = Quaternion.Euler(0f, 90f, 0f) * orientation;
            
            var positionChangeVector = (forwardAxis * orientation + strafeAxis * normal).normalized;

            var positionChange = Time.deltaTime * Speed * positionChangeVector;
            
            Vector3 newPosition = transform.position + positionChange;
            
            rb.MovePosition(newPosition);
            
            _animctrl.setAnimWalk();
        }
        else // idle
        {
            timeToSit -= Time.deltaTime;
            
            if (timeToSit <= 0f)
            {
                _animctrl.setAnimSit();
            }
            else
            {
                _animctrl.setAnimIdle();
            }
        }
    }
    
    private void Look()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if (lookAtScene)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))//, 1000, 6))
            {
                Vector3 hitPoint = hit.point;
                
                var rot = Quaternion.LookRotation(hitPoint - transform.position,Vector3.up);
            
                rot.x = 0f;
                rot.z = 0f;
                transform.rotation = rot;
            }
        }
        else
        {
            Plane floorPlane = new Plane(Vector3.up, Vector3.up * LookPlaneHeight);
        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            float rayDistance;
            if (floorPlane.Raycast(ray, out rayDistance))
            {
                Vector3 hitPoint = ray.GetPoint(rayDistance);
            
                // Rotate to look torwards the hitPoint
                var rot = Quaternion.LookRotation(hitPoint - transform.position,Vector3.up);
            
                rot.x = 0f;
                rot.z = 0f;
                transform.rotation = rot;
            }
        }

    }
    
}
