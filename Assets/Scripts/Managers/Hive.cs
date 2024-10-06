using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hive : MonoBehaviour, IDestination {
    // Spawners
    BeeSpawner beeSpawner;

    // Generators
    HoneyGenerator honeyGenerator;

    // Nectar
    int nectar = 0;

    //Sprite Management
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;

    // Upgrades
    [Header("Upgrades")]
    [SerializeField] Shop shop;

    // Events
    [Header("Unity Events")]
    public UnityEvent beeSpawned;
    public UnityEvent beeKilled;

    // Debugging
    [Header("Debugging")]
    [SerializeField] bool debugging = false;

    #region Unity

    private void Start() {
        BeeSpawnerInit();
        HoneyGeneratorInit();
        UpgradesInit();
    }


    private void OnMouseDown() {
        if (debugging) Debug.Log("Hive::OnMouseDown");
        beeSpawner.Spawn();
    }

    #endregion

    #region Initialization

    private void BeeSpawnerInit()
    {
        beeSpawner = gameObject.GetComponentInChildren<BeeSpawner>();
        beeSpawner.SetHive(this);
    }

    private void HoneyGeneratorInit()
    {
        honeyGenerator = gameObject.GetComponentInChildren<HoneyGenerator>();
        honeyGenerator.SetHive(this);
        honeyGenerator.SetGeneration(false);
    }

    private void UpgradesInit()
    {
        if (debugging) Debug.Log("Hive::UpgradesInit");

        foreach(IUpgrade upgrade in shop.GetUpgrades())
        {
            upgrade.SetHive(this);
        }
    }

    #endregion

    #region Nectar

    public int GetNectar() {
        return nectar;
    }

    public int ClaimNectar() {
        int temp = nectar;
        nectar = 0;
        return temp;
    }

    public int ClaimNectar(int amount) {
        if (amount > nectar) amount = nectar;

        nectar -= amount;
        return amount;
    }

    public void AddNectar(int amount) {
        nectar += amount;

        if (debugging) Debug.Log("Hive::AddNectar\nNectar:\t" + nectar);
    }

    public void SetNectar(int nectar) {
        this.nectar = nectar;
    }

    public void SetSprite(bool isGenerating) 
    {

        if (!isGenerating) spriteRenderer.sprite = spriteArray[0];
        else spriteRenderer.sprite = spriteArray[1];

    }

    #endregion

    #region Upgrades

    public BeeSpawner GetBeeSpawner()
    {
        return beeSpawner;
    }

    #endregion

    #region IDestination

    /// <summary>
    /// Transforms the 3D position into a 2D position.
    /// </summary>
    /// <returns>Hive position.</returns>
    public Vector2 GetPosition() {
        if (debugging) Debug.Log("Hive::GetPosition");

        return new Vector2(transform.position.x, transform.position.y);
    }

    #endregion

}
