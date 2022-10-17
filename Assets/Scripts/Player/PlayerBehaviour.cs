using System;
using General;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;

namespace Player
{
    public class PlayerBehaviour : SingletonMonoBehaviour<PlayerBehaviour>, IDamageable
    {
        [SerializeField] private GameObject fancyProjectile;
        [SerializeField] private AnimationController animationController;
        [SerializeField] private PlayerStats stats;
        
        // [SerializeField] private float maxHealth = 10;
        // [SerializeField] private float attackDamage = 3;
        // [SerializeField] private float criticalChance = .2f;
        // [SerializeField] private float projectileSpeed = 5.0f;
        // [SerializeField] private float attackCooldown = 1.0f;
        // [SerializeField] private float criticalDamage = 2.5f;
        
        private const float ProjectileLongevity = 5.0f;
        private const float OverhealDecayRate = 1f;

        public const float LookHeight = 1f;
        private const float ProjectileSourceOffset = 2.5f;

        private float _lastAttackTime;

        private float MaxHealth => Stats.MaxHealth;
        private float AttackCooldown => 1f / Stats.AttackSpeed;
        
        public PlayerStats Stats => stats;

        private AudioSource _audioSource;

        public float CurrentHealth { get; set; }

        private bool isDead = false;

        public bool IsDead { get => isDead; }
        
        private void Start()
        {
            CurrentHealth = stats.MaxHealth;
            _audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        }

        private void Update()
        {
            if(isDead)
            {
                return;
            }


            if (Input.GetMouseButton(0) && (Time.time - _lastAttackTime > AttackCooldown))
            {
                _lastAttackTime = Time.time;
                Attack();
                _audioSource.Play();
            }

            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     animationController.randomAnimal();
            //     // animctrl.chooseAnimal("validanimalname");
            // }
            
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth -= OverhealDecayRate * Time.deltaTime;
            }

            Debug.LogWarning("Current health is " + CurrentHealth);

            if(CurrentHealth <= 0f)
            {
                isDead = true;
                ScoreManager.Instance.pauseScoreCount();
                DeathScript ds = gameObject.AddComponent<DeathScript>();
                ds.playerDied();
            }
            else if(CurrentHealth < MaxHealth)
            {
                CurrentHealth = Mathf.Min(CurrentHealth + OverhealDecayRate / 2 * Time.deltaTime, MaxHealth);
            }
        }

        [Obsolete]
        public void IncreaseProjectileLongevity(float increaseAmount)
        {
            // ProjectileLongevity += increaseAmount;
        }
        
        public void ChangeBaseStats(PlayerStats stats)
        {
            this.stats = stats;
        }

        public void Attack()
        {
            animationController.setAnimAttack();

            var position = transform.position + transform.forward * ProjectileSourceOffset;
            position = new Vector3(position.x, LookHeight, position.z);
            

            GameObject projectileInst = Instantiate(fancyProjectile,
                        position, transform.rotation);

            AttackEffect projectileManager = projectileInst.GetComponent<AttackEffect>();
            projectileManager.InitSetup(stats.AttackDamage, stats.CriticalChance,
                stats.CriticalDamageMultiplier, ProjectileLongevity);

            ProjectileMover mover = projectileInst.GetComponent<ProjectileMover>();
            mover.InitSetup(stats.ProjectileSpeed, ProjectileLongevity);

            // AttackProjectile spawnedProjectile = Instantiate(projectileTemplate, position, transform.rotation);
            // spawnedProjectile.InitSetup(stats.AttackDamage, stats.CriticalChance, stats.CriticalDamageMultiplier, ProjectileLongevity);

            // Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();
            // rb.velocity = transform.forward * stats.ProjectileSpeed;
        }
    }
}
