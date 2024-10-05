using UnityEngine;

public class Flower : MonoBehaviour, IEntity, IDestination
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public IDestination GetDestination()
    {
        throw new System.NotImplementedException();
    }

    public Vector2 GetPosition()
    {
        throw new System.NotImplementedException();
    }

    public ISpawner GetSpawner()
    {
        throw new System.NotImplementedException();
    }

    public void Kill()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void SetDestination(IDestination destination)
    {
        throw new System.NotImplementedException();
    }

    public void SetSpawner(ISpawner spawner)
    {
        throw new System.NotImplementedException();
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }
}
