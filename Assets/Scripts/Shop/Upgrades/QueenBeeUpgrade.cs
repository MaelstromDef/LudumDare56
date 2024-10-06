using UnityEngine;

public class QueenBeeUpgrade : MonoBehaviour, IUpgrade
{
    [Header("Upgrade Values")]
    [SerializeField] string upgradeName = "Queen Bee";
    [SerializeField] string upgradeDesc = "Automatically sends out workers from a hive at a rate of 1/s (increased at higher levels)";
    [SerializeField] int upgradeCost = 10;

    // Hive
    Hive hive;

    #region IUpgrade

    public string GetName()
    {
        return upgradeName;
    }

    public string GetDescription()
    {
        return upgradeDesc;
    }

    public int GetCost()
    {
        return upgradeCost;
    }

    public void Upgrade()
    {
        hive.GetBeeSpawner().ActivateQueenBee();
    }

    public Hive GetHive()
    {
        return hive;
    }

    public void SetHive(Hive hive)
    {
        this.hive = hive;
    }

    #endregion
}