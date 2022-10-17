using System.Collections.Generic;
using System.Linq;
using Enums;
using General;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Animals
{
    [CreateAssetMenu(fileName = "Animal", menuName = "Animals/Animal", order = 1)]
    public class Animal : ScriptableObject
    {
        [Title("Animal Stats & Data")]
        [ShowInInspector] public string AnimalName;
        [ShowInInspector] public GameObject AnimalPrefab;
        [ShowInInspector] public PlayerStats AnimalBaseStats;
    }
}

