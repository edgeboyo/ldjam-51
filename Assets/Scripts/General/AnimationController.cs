using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationController : MonoBehaviour
{
	private List<string> animalList = new List<string> {
		"Cat",
		"Dog",
		"Dove",
		"Goldfish",
		"Mouse",
		"Parrot",
		"Pigeon",
		"Rabbit",
		"Tortoise",
		"Buffalo",
		"Chick",
		"Cow",
		"Dog",
		"Donkey",
		"Duck",
		"Elephant",
		"Fox",
		"Hen",
		"Hippo",
		"Hog",
		"Pig",
		"Raccoon",
		"Rooster",
		"Sheep",
		"Wolf",
		"Zebra"
	};
	
	private List<string> animationList = new List<string> {
		"Attack",
		"Bounce",
		"Clicked",
		"Death",
		"Eat",
		"Fear",
		"Fly",
		"Hit",
		"Idle_A", "Idle_B", "Idle_C",
		"Jump",
		"Roll",
		"Run",
		"Sit",
		"Spin/Splash",
		"Swim",
		"Walk"
                                                              
	};
    private List<string> facialExpList = new List<string> {
	    "Eyes_Annoyed",
	    "Eyes_Blink",
	    "Eyes_Cry",
	    "Eyes_Dead",
	    "Eyes_Excited",
	    "Eyes_Happy",
	    "Eyes_LookDown",
	    "Eyes_LookIn",
	    "Eyes_LookOut",
	    "Eyes_LookUp",
	    "Eyes_Rabid",
	    "Eyes_Sad",
	    "Eyes_Shrink",
	    "Eyes_Sleep",
	    "Eyes_Spin",
	    "Eyes_Squint",
	    "Eyes_Trauma",
	    "Sweat_L",
	    "Sweat_R",
	    "Teardrop_L",
	    "Teardrop_R"
    };
    
	public string currAnimal;
    public string currAnimation;
    public string currFacialExp;

    private void Start()
    {
	    getCurrAnimal();
    }

    public void getCurrAnimal()
    {
	    for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).GameObject();
            if (child.activeSelf)
            {
        	    currAnimal = child.name;
            } else if (currAnimal != null)
            {
        	    child.SetActive(false);
            }
	    }
    }

    public void randomAnimal()
    {
	    int randInd = Random.Range(0,transform.childCount);
	    for (int i = 0; i < transform.childCount; i++)
	    {
		    GameObject child = transform.GetChild(i).GameObject();
		    child.SetActive(false);
	    }
	    transform.GetChild(randInd).GameObject().SetActive(true);
	    currAnimal = transform.GetChild(randInd).GameObject().name;
    }

    public void chooseAnimal(string name)
    {
		Debug.LogWarning("Choose animal");
	    if (currAnimal == name)
	    {
		    return;
	    }
	    
	    for (int i = 0; i < transform.childCount; i++)
	    {
		    GameObject child = transform.GetChild(i).GameObject();
		    if (child.name == name)
		    {
			    child.SetActive(true);
			    continue;
		    }
		    child.SetActive(false);
	    }
    }

    public void setAnimIdle() { changeAnimtion("Idle_A"); }
    public void setAnimWalk() { changeAnimtion("Walk"); }
    public void setAnimSit() { changeAnimtion("Sit"); }

    public void setAnimAttack()
    {
	    // needs to take priority
	    changeAnimtion("Attack");
    }

    public void changeAnimtion(string name, bool reversed = false, bool priority = false, int count = 0)
    {
	    int speed;
	    List<Animator> animators = GetComponentsInChildren<Animator>().ToList();
	    
	    if (reversed) { speed = -1; } else { speed = 1; }
	    if (animationList.Contains(name)) { currAnimation = name; }
	    if (facialExpList.Contains(name)) { currFacialExp = name; }

	    foreach (Animator anim in animators)
	    {
		    anim.Play(name);
		    anim.speed = speed;
	    }
    }
}
