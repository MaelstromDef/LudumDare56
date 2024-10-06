using UnityEngine;

public class Flower : MonoBehaviour, IEntity, IDestination
{
    // Generators
    NectarGenerator nectarGenerator;

    // Spawner
    ISpawner spawner;
    [SerializeField] Vector2 hiveLocation;
    [SerializeField] float minDistance;

    // Debugging
    [SerializeField] bool debugging = false;


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

    public int CollectNectar()
    {
        return nectarGenerator.ClaimFullYield();
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
        // Get ray direction
        Vector2 dir = Random.insideUnitCircle;

        // Shoot ray
        RaycastHit2D hit = Physics2D.Raycast(hiveLocation, dir, Mathf.Infinity, LayerMask.GetMask(new string[] { "Bounds" }));
        float distance = hit.distance;
        if (distance < minDistance) Debug.LogError("Flower::Spawn\nDistance was less than minimum distance:\t" + distance);

        // Get random distance and place
        Ray2D ray = new Ray2D(hiveLocation, dir);
        float randDist = Random.Range(minDistance, distance);
        Vector2 point = ray.GetPoint(randDist);

        transform.position = point;

        if (debugging) Debug.Log("Flower::Spawn\nPosition:\t" + transform.position + "\nPoint:\t" + point);

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
