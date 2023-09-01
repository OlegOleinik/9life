using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesController : AController
{
    [SerializeField] private float spawnDelay = 0.1f;
    [SerializeField] private int maxEnemiesCount = 10;
    [SerializeField] private List<SpawnArea> spawnAreas;
    [SerializeField] private Transform enemiesPoolTransform;
    [SerializeField] private List<AEnemy> enemiesPrefabs;
    
    public Vector2 TargetPoint => player.transform.position;

    private List<EEnemyType> enemies = new List<EEnemyType>();
    private Pool<AEnemy> enemiesPool;
    private int enemiesCount = 0;
    private float nextEnemySpawn = 0;
    private EEnemyType NextEnemyType {get; set; }
    
    private Player player;

    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Player, out player);

        foreach (var enemy in enemiesPrefabs)
        {
            enemies.Add(enemy.Type);
        }

        enemiesPool = new Pool<AEnemy>(() =>
        {
            var enemy = Instantiate(enemiesPrefabs.Find(e => e.Type == NextEnemyType), enemiesPoolTransform);
            return enemy;
        }, Enum.GetValues(typeof(EEnemyType)).Cast<Enum>().ToList(), EnemyInit);
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
        enemiesPool.TryAddFreeObject(enemy.Type, enemy);
        enemiesCount--;
    }
    
    private void EnemyInit(AEnemy enemy)
    {
        enemy.Init(OnEnemyDie);
    }
    
    private void SpawnEnemy()
    {
        nextEnemySpawn = Time.time + spawnDelay;
        NextEnemyType = enemies[UnityEngine.Random.Range(0, enemies.Count)];
        var enemy = enemiesPool.GetFreeObject(NextEnemyType);
        enemy.OnSpawnEnemy();
        var spawnArea = spawnAreas[UnityEngine.Random.Range(0, spawnAreas.Count)];
        enemy.transform.position = spawnArea.GetSpawnPoint();
        enemiesCount++;
    }
}
