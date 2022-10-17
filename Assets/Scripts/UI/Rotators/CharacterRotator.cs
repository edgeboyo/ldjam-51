using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    public GameObject ObjectToRotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAnimal(GameObject animal)
    {
        GameObject newAnimal = Instantiate(animal, ObjectToRotate.transform.position, ObjectToRotate.transform.rotation, gameObject.transform);
        ObjectRotator rotator = ObjectToRotate.GetComponent<ObjectRotator>();
        ObjectRotator newRotator = newAnimal.AddComponent<ObjectRotator>();
        newRotator.movementPerSecond = rotator.movementPerSecond;
        newRotator.resetRot = rotator.resetRot;
        Destroy(ObjectToRotate);
        ObjectToRotate = newAnimal;

        newAnimal.SetActive(true);

        newRotator.ResetRotation();
    }
}
