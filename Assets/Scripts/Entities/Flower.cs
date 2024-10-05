using UnityEngine;

public class Flower : MonoBehaviour, IEntity, IDestination
{
    // Generators
    NectarGenerator nectarGenerator;

    // Spawner
    ISpawner spawner;


    #region Unity

    private void Start()
    {
        nectarGenerator = GetComponentInChildren<NectarGenerator>();
    }

    #endregion

    #region Flower

    public Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public float CollectNectar()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Destination

    public void Move()
    {
        throw new System.NotImplementedException("Flowers don't have destinations.");
    }

    public void SetDestination(IDestination destination)
    {
        throw new System.NotImplementedException("Flowers don't have destinations.");
    }

    public IDestination GetDestination()
    {
        throw new System.NotImplementedException("Flowers don't have destinations.");
    }

    #endregion

    #region Spawner

    public ISpawner GetSpawner()
    {
        return spawner;
    }

    public void SetSpawner(ISpawner spawner)
    {
        this.spawner = spawner;
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    #endregion
}
