using System;
using System.Collections.Generic;
using System.Linq;
using General;
using Enums;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy", order = 1)]
    public class Enemy : ScriptableObject
    {
        [Title("Enemy Stats & Data")]
        [ShowInInspector] public string EnemyName;
        [ShowInInspector] public EnemyStats EnemyBaseStats;
        [ShowInInspector] public string AttackAnimName;
        [ShowInInspector] public bool IsRanged { get; set; }
        /// <summary>
        /// The Character Model + collider setup for the enemy, as a prefab.
        /// </summary>
        [ShowInInspector] public GameObject EnemyPrefab { get; set; }
        /// <summary>
        /// Leave empty if enemy doesn't have a custom projectile with it.
        /// </summary>
        [ShowInInspector] public AttackEffect EnemyProjectilePrefab { get; set; }

        [ShowInInspector] public string IdleAnimName { get; set; }
        [ShowInInspector] public string MoveAnimName { get; set; }
    }
}
