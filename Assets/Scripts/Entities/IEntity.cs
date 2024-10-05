using UnityEngine;

public interface IEntity
{
    Vector2 FindFlower();   // Finds the nearest flower
    Vector2 Move();         // Moves towards active destination

    // Hive
    void SetHive(GameObject hive);
    GameObject GetHive();
}
