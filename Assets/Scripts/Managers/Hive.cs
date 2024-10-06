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

    // Events
    [Header("Unity Events")]
    public UnityEvent beeSpawned;
    public UnityEvent beeKilled;

    // Animation
    Animator hiveAnimator;

    // Debugging
    [Header("Debugging")]
    [SerializeField] bool debugging = false;

    #region Unity

    private void Start() {
        // Initialize Bee Spawner.
        beeSpawner = gameObject.GetComponentInChildren<BeeSpawner>();
        beeSpawner.SetHive(this);

        // Initialize Honey Generator.
        honeyGenerator = gameObject.GetComponentInChildren<HoneyGenerator>();
        honeyGenerator.SetHive(this);
        honeyGenerator.SetGeneration(false);

        //Get animator component
        hiveAnimator = gameObject.GetComponent<Animator>();
    }


    private void OnMouseDown() {
        if (debugging) Debug.Log("Hive::OnMouseDown");
        beeSpawner.Spawn();
        hiveAnimator.SetTrigger("isClicked");
    }

    #endregion

    #region Hive

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
