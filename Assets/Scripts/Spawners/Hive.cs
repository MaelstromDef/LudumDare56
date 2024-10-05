using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hive : MonoBehaviour, IDestination, ISpawner
{
    // Bees
    int maxBees;
    int numBees;
    private List<Bee> bees;
    [SerializeField] GameObject beePrefab;

    // Events
    public UnityEvent beeSpawned;
    public UnityEvent beeKilled;

    // Debugging
    [SerializeField] bool debugging = false;

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
            for(int i = maxBees; i > count; i--)
            {
                if(bees[bees.Count - 1] != null && bees[bees.Count - 1].isActiveAndEnabled) bees[bees.Count - 1].Kill();
                bees.RemoveAt(bees.Count - 1);
            }
        }
        // Count more than max bees
        else if(count > maxBees)
        {
            for(int i = maxBees; i < count; i++)
            {
                Bee newBee = Instantiate(beePrefab, transform, false).GetComponent<Bee>();
                newBee.Deactivate();
                bees.Add(newBee);
            }
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
