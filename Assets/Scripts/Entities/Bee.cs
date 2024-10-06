using System;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bee : MonoBehaviour, IEntity
{
    // References
    ISpawner spawner;
    IDestination destination;

    // Values
    [Header("Bee Parameters")]
    Vector2 moveDir = Vector2.zero;
    [SerializeField] float speed = 1.0f;
    [SerializeField] int flowersToVisit = 1;
    bool move = true;

    [SerializeField] float destinationPauseTime = 1.0f;

    // Flowers
    int collectedNectar = 0;

    // Debugging
    [Header("Debugging")]
    [SerializeField] bool debugging = false;

    int dir;

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
        collectedNectar += ((Flower)destination).CollectNectar();
        FlowerManager.instance.ReleaseFlower((Flower)destination);
    }

    void FindHive()
    {
        SetDestination(destination = ((BeeSpawner)spawner).GetHive());
    }

    void DestinationReached()
    {
        if (debugging) Debug.Log("Bee::DestinationReached");

        if(destination is Flower)
        {
            ReleaseFlower();
            flowersToVisit--;

            if(flowersToVisit > 0) FindFlower();
            else FindHive();

            move = true;
        }else if(destination is Hive)
        {
            ((BeeSpawner)spawner).GetHive().AddNectar(collectedNectar);
            Kill();
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

        // Find move distance
        float moveDistance = speed * Time.deltaTime;
        float actualDistance = (destination.GetPosition() - this.GetPosition()).magnitude;
        if (moveDistance > actualDistance)
        {
            move = false;
            moveDistance = actualDistance;
            Invoke(nameof(DestinationReached), destinationPauseTime);
        }

        // Move transform
        Vector3 movement = new Vector3(moveDir.x, moveDir.y, 0) * moveDistance;
        transform.position += movement;
    }

    public IDestination GetDestination()
    {
        return destination;
    }

    public void SetDestination(IDestination destination)
    {
        // Get movement direction vector
        this.destination = destination;
        moveDir = (destination.GetPosition() - this.GetPosition()).normalized;   // Possibly have to force z to be 0, if buggy look into. Also might need a dif direction.

        // Set sprite.
        dir = MovementDirection();
        int currentDir = Math.Sign(gameObject.transform.localScale.y);
        if (dir != currentDir) gameObject.transform.localScale.Set(transform.localScale.x * 1, transform.localScale.y * -1, transform.localScale.z); ;
    }

    /// <summary>
    /// Retrieves the direction the bee will have to move in to reach its destination.
    /// </summary>
    /// <param name="destination">-1 if left, 1 if right.</param>
    /// <returns></returns>
    public int MovementDirection() 
    {
        if (moveDir.x < 0) {
            return -1;
        } else return 1;
    }

    #endregion
}
