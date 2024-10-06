using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class BeeSpawner : MonoBehaviour, ISpawner
{
    // Hive
    Hive hive;

    // Bees
    [SerializeField] int maxBees = 1;
    private List<IEntity> bees = new List<IEntity>();
    [SerializeField] GameObject beePrefab;

    // Debugging
    [Header("Debugging")]
    [SerializeField] bool printMethodCalls = false;
    [SerializeField] bool spawnBeeOnStart = false;

    #region Unity

    private void Start()
    {
        if(spawnBeeOnStart) Invoke(nameof(Spawn), 1f);
    }

    private void OnDestroy()
    {
        KillAll();
    }

    #endregion

    #region BeeSpawner

    /// <summary>
    /// Sets the max number of bees and loads in inactive bees.
    /// </summary>
    /// <param name="count">New max number of bees.</param>
    /// <exception cref="Exception">Bee count did not match expected after computations.</exception>
    public void SetMaxBees(int count)
    {
        if (printMethodCalls) Debug.Log("Hive::SetMaxBees");

        if (count < 0) count = 0;       // Input validation.
        maxBees = count;
    }

    /// <summary>
    /// Retrieves the max number of bees.
    /// </summary>
    /// <returns></returns>
    public int GetMaxBees()
    {
        if (printMethodCalls) Debug.Log("Hive::GetMaxBees");

        return maxBees;
    }

    public void SetHive(Hive hive)
    {
        this.hive = hive;
    }

    public Hive GetHive()
    {
        return hive;
    }

    #endregion

    #region ISpawner

    /// <summary>
    /// Spawns a single bee.
    /// </summary>
    public void Spawn()
    {
        if (printMethodCalls) Debug.Log("Hive::Spawn");

        if (bees.Count < maxBees)
        {
            GameObject beeObj = Instantiate(beePrefab, transform);
            Bee bee = beeObj.GetComponent<Bee>();
            bee.SetSpawner(this);
            bees.Add(bee);

            bee.Spawn();
        }
    }

    /// <summary>
    /// Spawns a number of bees.
    /// </summary>
    /// <param name="count">Number of bees to spawn.</param>
    public void Spawn(int count)
    {
        if (printMethodCalls) Debug.Log("Hive::Spawn(count)");

        for (int i = 0; i < count; i++)
            Spawn();
    }

    public void Remove(IEntity entity)
    {
        bees.Remove(entity);
    }

    /// <summary>
    /// Kills a single bee.
    /// </summary>
    public void Kill()
    {
        if (printMethodCalls) Debug.Log("Hive::Kill");

        if (bees.Count == 0) return;

        bees[bees.Count - 1].Kill();
    }

    /// <summary>
    /// Kills a number of active bees.
    /// </summary>
    /// <param name="count">Number of bees to kill.</param>
    public void Kill(int count)
    {
        if (printMethodCalls) Debug.Log("Hive::Kill(count)");

        for (int i = 0; i < count; i++) Kill();
    }

    /// <summary>
    /// Kills all active bees.
    /// </summary>
    public void KillAll()
    {
        if (printMethodCalls) Debug.Log("Hive::KillAll");

        Kill(bees.Count);
    }

    /// <summary>
    /// Transforms the 3D position into a 2D position.
    /// </summary>
    /// <returns>Hive position.</returns>
    public Vector2 GetPosition()
    {
        if (printMethodCalls) Debug.Log("Hive::GetPosition");

        return new Vector2(transform.position.x, transform.position.y);
    }

    #endregion
}