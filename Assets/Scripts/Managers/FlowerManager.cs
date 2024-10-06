using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class FlowerManager : MonoBehaviour, ISpawner
{
    // Singleton
    public static FlowerManager instance;

    // Flowers
    [SerializeField] int maxFlowers = 1;
    [SerializeField] GameObject flowerPrefab;

    Queue<IEntity> unclaimedFlowers = new Queue<IEntity>();     // Flower set that a bee is not already going to
    HashSet<IEntity> claimedFlowers = new HashSet<IEntity>();   // Flower set that a bee is already going to 

    // Values
    [SerializeField] float respawnDelay = 1f;
    bool keepDead = false;

    // Debugging
    [SerializeField] bool debugging = false;

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
        if (unclaimedFlowers.Count == 0) return null;

        Flower flower = (Flower)unclaimedFlowers.Dequeue();
        claimedFlowers.Add(flower);
        return flower;
    }

    public void ReleaseFlower(Flower flower)
    {
        if (debugging) Debug.Log("FlowerManager::ReleaseFlower");

        claimedFlowers.Remove(flower);
        unclaimedFlowers.Enqueue(flower);
    }

    #endregion

    #region ISpawner

    public Vector2 GetPosition()
    {
        return Vector2.zero;
    }

    public void Remove(IEntity entity)
    {
        // Claimed
        if (claimedFlowers.Contains(entity))
        {
            
            claimedFlowers.Remove(entity);
        }
        else
        {
            // Unclaimed
            Queue<IEntity> temp = new Queue<IEntity>();
            while (unclaimedFlowers.Count > 0)
            {
                IEntity fl = unclaimedFlowers.Dequeue();
                if (fl != entity) temp.Enqueue(fl);
            }

            unclaimedFlowers = temp;
        }

        entity.Kill();
        if (!keepDead) Invoke(nameof(Spawn), respawnDelay);
    }

    public void Kill()
    {
        List<IEntity> fls = claimedFlowers.ToList();
        IEntity fl = fls[fls.Count - 1];
        fl.Kill();
        fls.Remove(fl);
        
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
        flower.SetSpawner(this);
        flower.Spawn();
        unclaimedFlowers.Enqueue(flower);
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++) Spawn();
    }

    #endregion
}
