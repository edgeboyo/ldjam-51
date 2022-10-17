using System;
using General;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Enemy statistics to be modified by mutations
    /// </summary>
    [Serializable]
    public class EnemyStats : CharacterStats
    {
        // todo add other stuff like detection range etc
        [field: SerializeField] public float DetectionRange { get; set; }
        [field: SerializeField] public float MeleeAttackReach { get; set; }
    }
}