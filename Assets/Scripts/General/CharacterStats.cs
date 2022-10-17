using System;
using UnityEngine;

namespace General
{
    /// <summary>
    /// Any character (player/enemy) statistics to be modified by mutations
    /// </summary>
    [Serializable]
    public class CharacterStats
    {
        [field: SerializeField] public float MaxHealth { get; set; }
        [field: SerializeField] public float AttackDamage { get; set; }
        /// <summary>
        /// Shots per second
        /// </summary>
        [field: SerializeField] public float AttackSpeed { get; set; }
        [field: SerializeField] public float ProjectileSpeed { get; set; }
        [field: SerializeField] public float MovementSpeed { get; set; }
        [field: SerializeField] public float CriticalDamageMultiplier { get; set; }
        [field: SerializeField] public float CriticalChance { get; set; }

        // todo add other stuff like movement speed etc
    }
}