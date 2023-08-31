using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : AController
{
    [SerializeField] private float spawnDelay = 0.1f;
    [SerializeField] private int maxEnemiesCount = 10;
    [SerializeField] private List<EEnemyType> enemies;
    [SerializeField] private List<SpawnArea> spawnAreas;
    [SerializeField] private Transform enemiesPoolTransform;
    [SerializeField] private List<AEnemy> enemiesPrefabs;
    public Vector2 TargetPoint => player.transform.position;

    private int enemiesCount = 0;
    private float nextEnemySpawn = 0;
    
    private Dictionary<EEnemyType, LinkedList<AEnemy>> freeEnemiesPool =
        new Dictionary<EEnemyType, LinkedList<AEnemy>>();
    
    private Player player;

    private void Start()
    {
        Controllers.GetController(EControllerType.Player, out player);
        var enemiesNames = (EEnemyType[])Enum.GetValues(typeof(EEnemyType));
        for (int i = 0; i < enemiesNames.Length; i++)
        {
            freeEnemiesPool.Add(enemiesNames[i], new LinkedList<AEnemy>());
        }
    }

    private void Update()
    {
        if (enemiesCount < maxEnemiesCount && Time.time > nextEnemySpawn)
        {
            SpawnEnemy();
        }
    }

    public void OnEnemyDie(AEnemy enemy)
    {
        freeEnemiesPool[enemy.Type].AddLast(enemy);
        enemiesCount--;
    }

    private void SpawnEnemy()
    {
        nextEnemySpawn = Time.time + spawnDelay;
        var enemyType = enemies[UnityEngine.Random.Range(0, enemies.Count)];
        var enemy = GetFreeEnemy(enemyType);
        enemy.OnSpawnEnemy();
        var spawnArea = spawnAreas[UnityEngine.Random.Range(0, spawnAreas.Count)];
        enemy.transform.position = spawnArea.GetSpawnPoint();
        enemiesCount++;
    }

    private AEnemy GetFreeEnemy(EEnemyType type)
    {
        if (freeEnemiesPool[type].Count < 1)
        {
            var enemy = Instantiate(enemiesPrefabs.Find(e => e.Type == type), enemiesPoolTransform);
            enemy.Init(OnEnemyDie);
            return enemy;
        }
        else
        {
            var enemy = freeEnemiesPool[type].First.Value;
            freeEnemiesPool[type].RemoveFirst();
            return enemy;
        }
    }
}
