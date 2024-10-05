using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hive : MonoBehaviour, IDestination, ISpawner
{
    // Bees
    [SerializeField] int maxBees = 1;
    int numBees = 0;
    private List<Bee> bees = new List<Bee>();
    [SerializeField] GameObject beePrefab;

    // Events
    public UnityEvent beeSpawned;
    public UnityEvent beeKilled;

    // Debugging
    [SerializeField] bool debugging = false;

    #region Unity

    private void Start()
    {
        for (int i = 0; i < maxBees; i++) AddBee();

        Invoke(nameof(Spawn), 1f);
    }

    private void OnDestroy()
    {
        for(int i = 0; i < maxBees; i++) RemoveBee();
    }

    #endregion

    #region Bee List

    /// <summary>
    /// Adds a bee to the list.
    /// </summary>
    private void AddBee()
    {
        if (debugging) Console.WriteLine("Hive::AddBee");

        GameObject beeObj = Instantiate(beePrefab, transform);
        Bee newBee = beeObj.GetComponent<Bee>();
        newBee.SetSpawner(this);
        newBee.Deactivate();
        bees.Add(newBee);
    }

    /// <summary>
    /// Removes a bee from the list.
    /// </summary>
    private void RemoveBee()
    {
        if (bees[bees.Count - 1] != null && bees[bees.Count - 1].isActiveAndEnabled) bees[bees.Count - 1].Kill();
        bees.RemoveAt(bees.Count - 1);
    }

    /// <summary>
    /// Sets the max number of bees and loads in inactive bees.
    /// </summary>
    /// <param name="count">New max number of bees.</param>
    /// <exception cref="Exception">Bee count did not match expected after computations.</exception>
    public void SetMaxBees(int count)
    {
        if (debugging) Debug.Log("Hive::SetMaxBees");

        if (count < 0) count = 0;       // Input correction.
        if(count == maxBees) return;    // Do nothing scenario

        // Count less than max bees
        if(count < maxBees)
        {
            for(int i = maxBees; i > count; i--) RemoveBee();
        }
        // Count more than max bees
        else if(count > maxBees)
        {
            for(int i = maxBees; i < count; i++) AddBee();
        }

        if (bees.Count != count) throw new Exception("Max bees did not end up matching set value.\nDesired value: " + count + "\nActual value: " + bees.Count);
        maxBees = count;
    }

    /// <summary>
    /// Retrieves the max number of bees.
    /// </summary>
    /// <returns></returns>
    public int GetMaxBees() {
        if (debugging) Debug.Log("Hive::GetMaxBees");

        return maxBees;
    }

    #endregion

    #region IDestination

    /// <summary>
    /// Transforms the 3D position into a 2D position.
    /// </summary>
    /// <returns>Hive position.</returns>
    public Vector2 GetPosition()
    {
        if (debugging) Debug.Log("Hive::GetPosition");

        return new Vector2(transform.position.x, transform.position.y);
    }

    #endregion

    #region ISpawner

    /// <summary>
    /// Spawns a single bee.
    /// </summary>
    public void Spawn()
    {
        if (debugging) Debug.Log("Hive::Spawn");  

        if (numBees < maxBees)
        {
            bees[numBees++].Spawn();
        }
    }

    /// <summary>
    /// Spawns a number of bees.
    /// </summary>
    /// <param name="count">Number of bees to spawn.</param>
    public void Spawn(int count)
    {
        if (debugging) Debug.Log("Hive::Spawn(count)");

        for (int i =  0; i < count; i++)
            Spawn();
    }

    /// <summary>
    /// Kills a single bee.
    /// </summary>
    public void Kill()
    {
        if (debugging) Debug.Log("Hive::Kill");

        if (numBees == 0) return;

        if (numBees == maxBees)
        {
            bees[bees.Count - 1].Kill();
            numBees--;
        }
        else bees[numBees--].Kill();
    }

    /// <summary>
    /// Kills a number of active bees.
    /// </summary>
    /// <param name="count">Number of bees to kill.</param>
    public void Kill(int count)
    {
        if (debugging) Debug.Log("Hive::Kill(count)");

        for (int i = 0; i < count; i++) Kill();
    }

    /// <summary>
    /// Kills all active bees.
    /// </summary>
    public void KillAll()
    {
        if (debugging) Debug.Log("Hive::KillAll");

        Kill(numBees);
    }

    #endregion
}
