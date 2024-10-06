using UnityEngine;

public class Shop : MonoBehaviour
{
    // Singleton
    public static Shop instance;

    // Actions
    [Header("Action Names")]
    [SerializeField] public string queenBee = "Queen Bee";
    [SerializeField] public string diligentWorkers = "Diligent Workers";
    [SerializeField] public string hiveQuantity = "Hive Quantity";
    [SerializeField] public string maxBees = "Max Bees";
    [SerializeField] public string efficiency = "Efficiency";

    #region Unity

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    #endregion

    #region Shop

    public void PerformAction(string action)
    {
        Debug.Log(action);
    }

    #endregion
}
