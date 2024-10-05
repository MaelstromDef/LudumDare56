using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour, ISpawner
{
    // Singleton
    public static FlowerManager instance;

    // Flowers
    [SerializeField] int maxFlowers = 1;
    int numFlowers = 0;
    [SerializeField] GameObject flowerPrefab;
    List<Flower> flowers = new List<Flower>();

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

    #region ISpawner

    public Vector2 GetPosition()
    {
        return Vector2.zero;
    }

    public void Kill()
    {
        flowers[--numFlowers].Kill();
    }

    public void Kill(int count)
    {
        for (int i = 0; i < count; i++) Kill();
    }

    public void KillAll()
    {
        Kill(numFlowers);
    }

    public void Spawn()
    {
        GameObject flowerObj = Instantiate(flowerPrefab, transform);
        Flower newFlower = flowerObj.GetComponent<Flower>();
        newFlower.Spawn();
        
        flowers.Add(newFlower);
        numFlowers++;
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++) Spawn();
    }

    #endregion
}
