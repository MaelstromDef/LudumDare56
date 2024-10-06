/*
 * Ryan Carpenter
 * Saturday 11:32am
 */
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
    // References
    ISpawner spawner;
    IDestination destination;

    // Values
    [Header("Enemy Parameters")]
    Vector2 moveDir = Vector2.zero;
    [SerializeField] float speed = 1.0f;
    bool move = true;

    [SerializeField] float destinationPauseTime = 1.0f;

    // Debugging
    [Header("Debugging")]
    [SerializeField] bool debugging = false;

    int dir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region IEntity
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public IDestination GetDestination()
    {
        return destination;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public ISpawner GetSpawner()
    {
        return spawner;
    }

    public void Kill()
    {
        if (debugging) Debug.Log("Enemy:Kill");

        spawner.Remove(this);

        Destroy(gameObject);
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
        this.spawner = spawner;
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
