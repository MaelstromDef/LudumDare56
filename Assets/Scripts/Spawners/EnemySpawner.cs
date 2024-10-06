/*
 * Ryan Carpenter
 * Saturday 11:32am
 */
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISpawner
{
   // Used to grab enemy indexes from the EnemyPrefabs list
    private enum EnemyIdx   // idx#0 = squirrel, idx#1 = racoon, idx#3 = skunk
    {
        squirrel = 0,
        racoon = 1,
        skunk = 2
    }

    // Hive
    Hive hive;

    // Bees
    [SerializeField] int maxEnemies = 1;
    [SerializeField] GameObject squirrelPrefab;
    [SerializeField] GameObject racoonPrefab;
    [SerializeField] GameObject skunkPrefab;

    private List<IEntity> enemies = new List<IEntity>();
    private List<GameObject> enemyPrefabs;

    private int unlockedEnemyIndex = -1;

    // Debugging
    [Header("Debugging")]
    [SerializeField] bool printMethodCalls = false;
    [SerializeField] bool randomSpawn = false;
    [SerializeField] bool spawnSquirrel = false;
    [SerializeField] bool spawnRacoon = false;
    [SerializeField] bool spawnSkunk = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize Enemy List
        enemyPrefabs = new List<GameObject>()
        {
            squirrelPrefab,
            racoonPrefab,
            skunkPrefab
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region ISpawner
    /// <summary>
    /// Spawns an enemy based on certain parameters
    /// </summary>
    public void Spawn()
    {
        if (printMethodCalls) Debug.Log("Enemy::Spawn");

        if (enemies.Count < maxEnemies)
        {
            if (randomSpawn)
                SpawnRandom();

            // For debugging
            if (spawnSquirrel)
                Spawn((int)EnemyIdx.squirrel);
            if (spawnRacoon)
                Spawn((int)EnemyIdx.racoon);
            if (spawnSkunk)
                Spawn((int)EnemyIdx.skunk);
        }
    }

    /// <summary>
    /// Spawn a enemy from the enemy list based on the index passed into the function
    /// idx#0 = squirrel, idx#1 = racoon, idx#3 = skunk
    /// </summary>
    /// <param name="index">The index of the enemy in the list</param>
    public void Spawn(int index)
    {
        // Make sure the index is in range of the list
        if (index < 0 || index > 2)
            return;
        if (enemyPrefabs[index] == null)
            return;

        if (printMethodCalls) Debug.Log("EnemySpawner::Spawn(index)");

        GameObject enemyObj = Instantiate(enemyPrefabs[index], transform);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.SetSpawner(this);
        enemies.Add(enemy);

        enemy.Spawn();
    }

    /// <summary>
    /// Select a random index from the unlocked enemies and spawn a 
    /// random enemy using the Spawn(int) method
    /// </summary>
    public void SpawnRandom()
    {
        if (unlockedEnemyIndex < 0)
            return;

        int randIdx = UnityEngine.Random.Range(0, unlockedEnemyIndex);

        Spawn(randIdx);
    }

    public void Remove(IEntity entity)
    {
        enemies.Remove(entity);
    }

    /// <summary>
    /// Kills a single enemy.
    /// </summary>
    public void Kill()
    {
        if (printMethodCalls) Debug.Log("EnemySpawner::Kill");

        if (enemies.Count == 0)
            return;

        enemies[enemies.Count - 1].Kill();
    }

    /// <summary>
    /// Kills a number of active enemies.
    /// </summary>
    /// <param name="count">Number of enemies to kill.</param>
    public void Kill(int count)
    {
        if (printMethodCalls) Debug.Log("EnemySpawner::Kill(count)");

        for (int i = 0; i < count; i++) 
            Kill();
    }

    /// <summary>
    /// Kills all active enemies.
    /// </summary>
    public void KillAll()
    {
        if (printMethodCalls) Debug.Log("EnemySpawner::KillAll");

        Kill(enemies.Count);
    }

    /// <summary>
    /// Transforms the 3D position into a 2D position.
    /// </summary>
    /// <returns>Hive position.</returns>
    public Vector2 GetPosition()
    {
        if (printMethodCalls) Debug.Log("EnemySpawner::GetPosition");

        return new Vector2(transform.position.x, transform.position.y);
    }

    #endregion
}
