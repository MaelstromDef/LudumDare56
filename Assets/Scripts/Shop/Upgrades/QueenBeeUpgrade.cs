using UnityEngine;

public class QueenBeeUpgrade : MonoBehaviour, IUpgrade
{
    [SerializeField] string upgradeName = "Queen Bee";
    [SerializeField] string upgradeDesc = "Automatically sends out workers from a hive at a rate of 1/s (increased at higher levels)";
    [SerializeField] int upgradeCost = 10;

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
        throw new System.NotImplementedException();
    }
}