using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField] public float movementPerSecond = 2f;
    public Quaternion resetRot;
    // Start is called before the first frame update
    void Start()
    {
        // resetRot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, movementPerSecond * Time.unscaledDeltaTime, 0);
    }

    public void ResetRotation()
    {
        gameObject.transform.rotation = resetRot;
    }
}
