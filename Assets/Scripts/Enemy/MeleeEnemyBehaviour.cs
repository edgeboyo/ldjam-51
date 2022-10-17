using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Player;
using Enemy;
using General;
using System.Collections;

namespace Enemy
{
    public class MeleeEnemyBehaviour : MonoBehaviour, IDamageable, IEnemyAttackBehaviour
    {
        private float lastAttackTime = 0;

        [SerializeField] public AttackEffect meleeAttackEffect;
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private EnemyStats stats;

        private GameObject player;

        private float ProjectilePosY => PlayerBehaviour.LookHeight;
        private float MaxHealth => Stats.MaxHealth;
        private float AttackCooldown => 1f / Stats.AttackSpeed;
        public EnemyStats Stats => stats;
        public float CurrentHealth { get; set; }
        private const float projectileLongevity = 5.0f;

        void Start()
        {
            player = GameObject.Find("Player");
            CurrentHealth = stats.MaxHealth;
            
            transform.Find("Healthbar").localScale = new Vector3(CurrentHealth / 10f, 0.1f, 0.05f);
        }

        void Update()
        {
            if (CurrentHealth < 0)
            {
                Destroy(this.gameObject);
            }
            
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            Attack();
        }

        void FixedUpdate()
        {

        }

        public float GetAttackRange() => stats.MeleeAttackReach;

        public void Attack()
        {
            // check if we're in attack range of the player
            if (Vector3.Distance(player.transform.position, transform.position) < stats.MeleeAttackReach)
            {
                Debug.Log("WITHIN RANGE OF PLAYER");

                Vector3 playerPos = player.transform.position;
                // check attack cooldown
                if (Time.time - lastAttackTime > AttackCooldown)
                {
                    Debug.Log("Attack Attack Attack");
                    // attack
                    lastAttackTime = Time.time;
                    Vector3 attackDir = playerPos - transform.position;
                    Vector3 effectOffset = (attackDir.normalized * stats.MeleeAttackReach) + Vector3.up * ProjectilePosY;
                    // spawn the effect at the midway point between the offset and the player
                    Vector3 spawnPos = (playerPos + (transform.position + effectOffset)) * 0.5f;
                    Debug.Log("Spawning Projectile");
                    AttackEffect spawnedAttackEffect = Instantiate(meleeAttackEffect, spawnPos, transform.rotation);
                    spawnedAttackEffect.InitSetup(stats.AttackDamage, stats.CriticalChance, stats.CriticalDamageMultiplier, projectileLongevity);
                }
            }
        }
    }
}
