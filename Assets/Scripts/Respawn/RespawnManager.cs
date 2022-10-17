using General;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class RespawnManager : SingletonMonoBehaviour<RespawnManager>
{
    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] int maxEnemies = 3;
    [SerializeField] float respawnDelay = 3;
    
    [SerializeField] private float percentIncreasedEnemyHealth = 1;
    [SerializeField] private float percentIncreasedEnemyFirerate = 1;
    [SerializeField] private float percentIncreasedEnemyAttackDamage = 1;
    
    private RespawnAnchor[] respawnAnchors;

    private List<GameObject> enemiesSpawned;

    [ShowInInspector] private List<GameObject> EnemiesSpawned { get => enemiesSpawned; }
    // Start is called before the first frame update
    private void Start()
    {
        respawnAnchors = FindObjectsOfType<RespawnAnchor>();

        enemiesSpawned = new List<GameObject>();
        while (enemiesSpawned.Count < maxEnemies)
        {
            SpawnEnemyOnRandomAnchor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (enemiesSpawned.Count < maxEnemies)
        {
            SpawnEnemyOnRandomAnchor();
        }
        CheckEnemyCountAndRespawn();
    }

    void CheckEnemyCountAndRespawn()
    {
        for(var i = 0; i < enemiesSpawned.Count; i++)
        {
            GameObject enemy = enemiesSpawned[i];
            if(enemy == null)
            {
                enemy = SpawnEnemyOnRandomAnchor();
                enemy.SetActive(false);
                enemiesSpawned[i] = enemy;
                StartCoroutine(EnableAfterDelay(respawnDelay, enemy));
            }
        }
    }

    GameObject SpawnEnemyOnRandomAnchor()
    {
        RespawnAnchor anchor = respawnAnchors[Random.Range(0, respawnAnchors.Length)];
        GameObject enemy = anchor.SpawnEnemy(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);

        enemy.GetComponent<RangedEnemyBehaviour>().Stats.MaxHealth *= percentIncreasedEnemyHealth;
        enemy.GetComponent<RangedEnemyBehaviour>().Stats.AttackSpeed *= percentIncreasedEnemyFirerate;
        enemy.GetComponent<RangedEnemyBehaviour>().Stats.AttackDamage *= percentIncreasedEnemyAttackDamage;
        
        enemiesSpawned.Add(enemy);

        return enemy;
    }

    IEnumerator EnableAfterDelay(float time, GameObject enemyObject)
    {
        yield return new WaitForSeconds(time);

        enemyObject.SetActive(true);
    }

    public void IncreaseEnemyCount(int number)
    {
        maxEnemies += number;
    }

    public void IncreaseEnemyMaxHealth(int percent)
    {
        percentIncreasedEnemyHealth *= 1 + (percent / 100);
    }
    
    public void IncreaseEnemyFirerate(int percent)
    {
        percentIncreasedEnemyFirerate *= 1 + (percent / 100);
    }
    
    public void IncreaseEnemyAttackDamage(int percent)
    {
        percentIncreasedEnemyAttackDamage *= 1 + (percent / 100);
    }

}
