using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bee : MonoBehaviour, IEntity
{
    // References
    ISpawner spawner;
    IDestination destination;

    // Values
    [SerializeField] float speed = 1.0f;

    #region Unity

    private void Update()
    {
        Move();
    }

    #endregion

    #region Bee specific

    // Claims a flower from global flower pool
    IDestination FindFlower()
    {
        Console.WriteLine("FIND FLOWER");
        return destination;
        throw new System.NotImplementedException();
    }

    // Releases claim to flower from global flower pool
    void ReleaseFlower()
    {
        Console.WriteLine("RELEASE FLOWER");
        return;
        throw new System.NotImplementedException ();
    }

    #endregion

    #region Spawner

    public ISpawner GetSpawner() { 
        return spawner; 
    }

    public void SetSpawner(ISpawner spawner)
    {
        this.spawner = spawner;
    }

    public void Spawn()
    {
        // Set initial bee values
        Vector2 spawnerPos = spawner.GetPosition();
        transform.position.Set(spawnerPos.x, spawnerPos.y, transform.position.z);
        destination = FindFlower();
        transform.right = destination.GetPosition() - this.GetPosition();   // Possibly have to force z to be 0, if buggy look into. Also might need a dif direction.

        // Activate
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Kill()
    {
        ReleaseFlower();

        gameObject.SetActive(false);
    }

    #endregion

    #region Destination

    public Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;     // Possibly needs a dif direction.
    }

    public IDestination GetDestination()
    {
        return destination;
    }

    public void SetDestination(IDestination destination)
    {
        this.destination = destination;
    }

    #endregion
}
