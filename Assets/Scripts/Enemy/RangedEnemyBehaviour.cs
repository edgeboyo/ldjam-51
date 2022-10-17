using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using Player;

namespace Enemy
{
    public class RangedEnemyBehaviour : MonoBehaviour, IDamageable, IEnemyAttackBehaviour
    {
        private float _lastAttackTime = 0;

        private GameObject player;
        private Vector3 playerLastPosition, playerPos;
        
        public GameObject fancyProjectile;

        [SerializeField] public AttackEffect projectile;
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private EnemyStats stats;
        private float ProjectilePosY => PlayerBehaviour.LookHeight;
        private float MaxHealth => Stats.MaxHealth;
        private float AttackCooldown => 1f / Stats.AttackSpeed;

        public EnemyStats Stats => stats;

        public float CurrentHealth { get; set; }

        private const float projectileLongevity = 5.0f;
        
        private ScoreManager scoreManager;

        private bool isDead = false;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player");
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            playerPos = player.transform.position;
            CurrentHealth = stats.MaxHealth;
            
            transform.Find("Healthbar").localScale = new Vector3(CurrentHealth / 10f, 0.1f, 0.05f);
        }

        // Update is called once per frame
        void Update()
        {
            if(isDead)
            {
                return;
            }
            if (player == null)
            {
                return;
            }
            if (CurrentHealth < 0)
            {
                scoreManager.score += 500;
                gameObject.AddComponent<DeathScript>();
                isDead = true;
            }
            
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            Attack();
        }

        void FixedUpdate()
        {
            if(player == null)
            {
                return;
            }
            playerLastPosition = playerPos;
            playerPos = player.transform.position;
        }

        private Vector3 EstimateFuturePlayerPosition()
        {
            // take player velocity, and distance away
            // first work out player velocity (rb.velocity returns 0)
            Vector3 vel = (playerPos - playerLastPosition) ;

            Vector3 vecToPlayer = playerPos - transform.position;
            var distance = vecToPlayer.magnitude;
            var dist = Vector3.Distance(playerPos, transform.position);
            // work out time it'd take for a projectile to cover that distance
            Debug.LogWarning($"{playerPos}. {playerLastPosition}. {vecToPlayer}. {distance}. {dist}");
            float timeTaken = distance / stats.ProjectileSpeed;

            // add on player position to current velocity times time taken. simples
            Vector3 futurePlayerPos = playerPos + (vel * timeTaken);
            Vector3 dirToFuturePlayerPos = futurePlayerPos - transform.position;

            Debug.Log(string.Format("player pos = {0}, player velocity = {1}, time to player = {2}, estimated future position = {3}", player.transform.position, vel, timeTaken, futurePlayerPos));

            return futurePlayerPos;
        }

        public float GetAttackRange()
        {
            return projectileLongevity * stats.ProjectileSpeed;
        }

        public void Attack()
        {
            // shoot on cooldown at player's position, if in range. quick prototype testing

            if (Time.time - _lastAttackTime > AttackCooldown)
            {
                Debug.Log($"Time Check {Time.time - _lastAttackTime} > {AttackCooldown}");
				
                Vector3 futurePlayerPos = EstimateFuturePlayerPosition();
                Vector3 dirToFuturePlayerPos = futurePlayerPos - transform.position;
                Debug.LogWarning($"futurePlayerPos {futurePlayerPos}, playerPos {playerPos}");
                Debug.LogWarning($"dist = {Vector3.Distance(futurePlayerPos, transform.position)}, attack range = {GetAttackRange()}");
                // check distance estimated is less than range. If so, shoot. else leave it
                if (Vector3.Distance(transform.position, futurePlayerPos) <= GetAttackRange() + Mathf.Epsilon)
                {
                    Debug.Log($"Distance check {Vector3.Distance(transform.position, futurePlayerPos)}, {GetAttackRange()}");
                    _lastAttackTime = Time.time;
                    Vector3 projectileOffset = (dirToFuturePlayerPos.normalized * 3) + Vector3.up * ProjectilePosY;
                    // AttackEffect spawnedProjectile = Instantiate(projectile, gameObject.transform.position + projectileOffset, transform.rotation);
                    // spawnedProjectile.InitSetup(stats.AttackDamage, stats.CriticalChance, stats.CriticalDamageMultiplier, projectileLongevity);
                    Vector3 projDir = projectileOffset.normalized;
                    projDir.y = 0;

                    GameObject projectileInst = Instantiate(fancyProjectile.gameObject,
                        gameObject.transform.position + projectileOffset, Quaternion.LookRotation(projDir));
                    Debug.Log($"projectilePosition {projectileInst.transform.position}");
                    AttackEffect projectileManager = projectileInst.GetComponent<AttackEffect>();
                    projectileManager.InitSetup(stats.AttackDamage, stats.CriticalChance, 
                        stats.CriticalDamageMultiplier, projectileLongevity);
                    ProjectileMover mover = projectileInst.GetComponent<ProjectileMover>();
                    mover.InitSetup(stats.ProjectileSpeed, projectileLongevity);
                    
                    // if (spawnedProjectile.gameObject.GetComponent<ProjectileMover>() != null)
                    // {
                    //     ProjectileMover mover = spawnedProjectile.GetComponent<ProjectileMover>();
                    //     mover.speed = transform.forward.magnitude * stats.ProjectileSpeed;
                    // }
                    
                    // else
                    // {
                    //     Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();
                    //     var directionVector = (player.transform.position - (transform.position + projectileOffset));
                    //     directionVector.y = 0;
                    //     futurePlayerPos.y = 0;
                    //     rb.velocity = (futurePlayerPos + projectileOffset).normalized * stats.ProjectileSpeed;
                    // }
                }
            }

        }
    }
}