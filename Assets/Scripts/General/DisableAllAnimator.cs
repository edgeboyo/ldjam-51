using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Animator anim in gameObject.GetComponentsInChildren<Animator>())
        {
            anim.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
