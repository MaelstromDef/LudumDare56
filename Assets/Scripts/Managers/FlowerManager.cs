using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlowerManager : MonoBehaviour, ISpawner
{
    // Singleton
    public static FlowerManager instance;

    // Flowers
    [SerializeField] int maxFlowers = 1;
    [SerializeField] GameObject flowerPrefab;

    Queue<Flower> unclaimedFlowers = new Queue<Flower>();
    Queue<Flower> claimedFlowers = new Queue<Flower>();    

    // Values
    [SerializeField] float respawnDelay = 1f;
    bool keepDead = false;

    #region Unity

    public void Awake()
    {
        // Singleton behavior
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        Spawn(maxFlowers);
    }

    #endregion

    #region Flowers

    public Flower ClaimFlower()
    {
        Flower flower = unclaimedFlowers.Dequeue();
        claimedFlowers.Enqueue(flower);
        return flower;
    }

    public void ReleaseFlower(Flower flower)
    {
        Queue<Flower> temp = new Queue<Flower>();
        while(claimedFlowers.Count > 0)
        {
            Flower fl = claimedFlowers.Dequeue();
            if (fl != flower) temp.Enqueue(fl);
        }

        unclaimedFlowers.Enqueue(flower);
    }

    #endregion

    #region ISpawner

    public Vector2 GetPosition()
    {
        return Vector2.zero;
    }

    public void Kill()
    {
        claimedFlowers.Dequeue().Kill();
        if (!keepDead) Invoke(nameof(Spawn), respawnDelay);
    }

    public void Kill(int count)
    {
        for (int i = 0; i < count; i++) Kill();
    }

    public void KillAll()
    {
        keepDead = true;
        Kill(maxFlowers);

        while (unclaimedFlowers.Count > 0) unclaimedFlowers.Dequeue().Kill();
    }

    public void Spawn()
    {
        GameObject flowerObj = Instantiate(flowerPrefab, transform);
        Flower flower = flowerObj.GetComponent<Flower>();
        flower.Spawn();
        unclaimedFlowers.Enqueue(flower);
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++) Spawn();
    }

    #endregion
}
