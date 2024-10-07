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
    [SerializeField] GameObject queenSprite;
    [SerializeField] HiveAnimator hiveAnimator;

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
        CurrencyManagerInit();
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

    private void CurrencyManagerInit()
    {
        honeyGenerator.honeyGenerated.AddListener(CurrencyManager.instance.AddHoney);   // Add yield to global
        honeyGenerator.honeyGenerated.AddListener(ClaimHoney);                          // Claim the yield
    }

    #endregion

    #region Hive

    public BeeSpawner GetBeeSpawner()
    {
        return beeSpawner;
    }

    #endregion

    #region Honey

    public HoneyGenerator GetHoneyGenerator() { return honeyGenerator; }

    public int GetHoney() { return honeyGenerator.GetYield(); }

    public void ClaimHoney(int amount) { honeyGenerator.ClaimYield(amount); }

    public void SetSprite(bool isGenerating) {

        if (!isGenerating) {
            spriteRenderer.sprite = spriteArray[0];
            hiveAnimator.ActivateGeneratonIdle(false);
        } else {
            spriteRenderer.sprite = spriteArray[1];
            hiveAnimator.ActivateGeneratonIdle(true);
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

        honeyGenerator.SetGeneration(nectar >= honeyGenerator.GetRequiredNectar()); // Check if honey generator should generate honey.

        if (debugging) Debug.Log("Hive::AddNectar\nNectar:\t" + nectar);
    }

    public void SetNectar(int nectar) {
        this.nectar = nectar;
    }



    #endregion

    #region Upgrades

    public void ActivateQueenBee()
    {
        beeSpawner.ActivateQueenBee();
        queenSprite.SetActive(true);
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
