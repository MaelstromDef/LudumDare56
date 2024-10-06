using UnityEngine;

public interface IEntity
{
    // Spawner
    ISpawner GetSpawner();
    void SetSpawner(ISpawner spawner);
    void Spawn();           // Activates the entity.
    void Deactivate();      // Makes sure the entity isn't rendered or doing computations.
    void Kill();            // Kills this entity.

    // Destination
    IDestination GetDestination();
    void SetDestination(IDestination destination);

    void Move();         // Moves towards active destination.
    Vector2 GetPosition();  // Returns 2D position.
}
