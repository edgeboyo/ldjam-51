using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Enemy;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class EnemyAnimationController : MonoBehaviour
{
    private List<string> monsterNameList = new List<string> { 
        "Bloom",
        "Blossom",
        "Bomb",
        "Bud",
        "Fire Dragon",
        "Inferno Dragon",
        "Spark Dragon",
        "Hermit King",
        "Poison Bomb",
        "Practice Dummy",
        "Shell",
        "Snake",
        "Snake Naga",
        "Snakelet",
        "Snow Bomb",
        "Spike",
        "Sun Blossom",
        "Sunflora Pixie",
        "Sunflower Fairy",
        "Target Dummy",
        "Training Dummy",
        "Werewolf",
        "Wolf",
        "Wolf Pup"
    };

    private List<Enemy.Enemy> enemyList = new List<Enemy.Enemy>
    {

    };

    private List<string> projectileAttackList = new List<string>
    {

    };

    private GameObject EnemyTypeHolder; // holder for all enemy types
    public Enemy.Enemy enemyType;
    public string currAnimation;
    public string currFacialExp;

    private void Start()
    {
        
    }

    public void GetMonsterType()
    {
        
    }


}

