using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using General;
using Enemy;

namespace Managers
{
    public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
    {
        // class to manage enemy spawns, whether each enemy is melee or not
        /// <summary>
        /// All Enemy ScriptableObjects, add the prefab property in these as a child to a new EnemyPrefab gameobject (defined in this class) to have a functional enemy
        /// </summary>
        public List<Enemy.Enemy> EnemiesSpawnable;
        /// <summary>
        /// All fancy projectiles that aren't provided with the enemies themselves
        /// </summary>
        public List<AttackEffect> FancyProjectiles;
        /// <summary>
        /// Parent Object to hold the Enemy AI components + CharacterController + Attack Behaviour for Ranged/Melee
        /// <br></br>Add RangedEnemyBehaviour/MeleeEnemyBehaviour as components, and a WanderBehaviour as a component for a functional setup.
        /// </summary>
        public GameObject EnemyPrefab;
        /// <summary>
        /// Number of enemies to have in the arena.
        /// </summary>
        [SerializeField] private int _desiredEnemyNumber = 5;

        public void Start()
        {

        }

        public void Update()
        {
            _desiredEnemyNumber = (int)Time.time;
        }

        public void SpawnEnemy()
        {
            var loc = ChooseSpawnLocation();
            var enemyTypeToSpawn = EnemiesSpawnable[Random.Range(0, EnemiesSpawnable.Count)];
            GameObject eParent = Instantiate(EnemyPrefab, loc, Quaternion.identity);
            GameObject eChild = Instantiate(enemyTypeToSpawn.EnemyPrefab, loc, Quaternion.identity);
            eChild.transform.parent = eParent.transform;
            if (enemyTypeToSpawn.IsRanged)
            {
                RangedEnemyBehaviour reb = eParent.AddComponent<RangedEnemyBehaviour>();
                if (enemyTypeToSpawn.EnemyProjectilePrefab != null)
                {
                    reb.projectile = enemyTypeToSpawn.EnemyProjectilePrefab;
                }
                else
                {
                    reb.projectile = FancyProjectiles[Random.Range(0, FancyProjectiles.Count)];
                }
            }
            else
            {
                MeleeEnemyBehaviour meb = eParent.AddComponent<MeleeEnemyBehaviour>();
                if (enemyTypeToSpawn.EnemyProjectilePrefab != null)
                {
                    
                }
            }
        }

        public Vector3 ChooseSpawnLocation()
        {
            return Vector3.zero;
        }
    }
}