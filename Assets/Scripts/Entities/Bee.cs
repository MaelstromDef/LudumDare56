using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bee : MonoBehaviour, IEntity
{
    // References
    ISpawner spawner;
    IDestination destination;

    // Values
    [Header("Bee Parameters")]
    [SerializeField] float speed = 1.0f;
    bool move = true;

    [SerializeField] float destinationPauseTime = 1.0f;

    // Flowers


    // Debugging
    [Header("Debugging")]
    [SerializeField] bool debugging = false;

    #region Unity

    private void Update()
    {
        if(move) Move();
    }

    #endregion

    #region Bee

    // Claims a flower from global flower pool
    void FindFlower()
    {
        Flower flower = FlowerManager.instance.ClaimFlower();
        if (flower == null) Kill();
        else SetDestination(flower);
    }

    // Releases claim to flower from global flower pool
    void ReleaseFlower()
    {
        if (!(destination is Flower)) return;
        FlowerManager.instance.ReleaseFlower((Flower)destination);
    }

    void FindHive()
    {
        SetDestination(destination = ((BeeSpawner)spawner).GetHive());
    }

    void DestinationReached()
    {
        if (debugging) Debug.Log("Bee::DestinationReached");

        // Find hive
        if(destination is Hive)
        {
            Kill();
        }
        // Kill self
        else if(destination is Flower)
        {
            FlowerManager.instance.Remove((Flower)destination);
            FindHive();
            move = true;
        }
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
        FindFlower();

        // Activate
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Kill()
    {
        if (debugging) Debug.Log("Bee:Kill");

        ReleaseFlower();
        spawner.Remove(this);

        Destroy(gameObject);
    }

    #endregion

    #region Destination

    public Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public void Move()
    {
        if (destination == null) return;

        float moveDistance = speed * Time.deltaTime;
        float actualDistance = (destination.GetPosition() - this.GetPosition()).magnitude;
        if (moveDistance > actualDistance)
        {
            move = false;
            moveDistance = actualDistance;
            Invoke(nameof(DestinationReached), destinationPauseTime);
        }

        transform.position += transform.right * moveDistance;
    }

    public IDestination GetDestination()
    {
        return destination;
    }

    public void SetDestination(IDestination destination)
    {
        this.destination = destination;
        transform.right = destination.GetPosition() - this.GetPosition();   // Possibly have to force z to be 0, if buggy look into. Also might need a dif direction.
    }

    #endregion
}
