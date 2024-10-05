using UnityEngine;

public interface IEntity
{
    Vector2 FindFlower();   // Finds the nearest flower
    Vector2 Move();         // Moves towards active destination

    // Spawner
    void Spawn();           // Activates the entity.
    void Deactivate();      // Makes sure the entity isn't rendered or doing computations.
    void Kill();            // Kills this entity.

    // Destination
    IDestination GetDestination();
    void SetDestination(IDestination destination);
}
