using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Actions
    [Header("Action Names")]
    [SerializeField] public string queenBee = "Queen Bee";
    [SerializeField] public string diligentWorkers = "Diligent Workers";
    [SerializeField] public string hiveQuantity = "Hive Quantity";
    [SerializeField] public string maxBees = "Max Bees";
    [SerializeField] public string efficiency = "Efficiency";

    [Header("Upgrades")]
    [SerializeField] List<GameObject> upgradeObjs = new List<GameObject>();
    List<IUpgrade> upgrades = new List<IUpgrade>();

    #region Shop

    public void PerformAction(string action)
    {
        foreach(IUpgrade upgrade in GetUpgrades())
        {
            if (upgrade.GetName().Equals(action)) upgrade.Upgrade();
        }
    }

    private void UpgradesInit()
    {
        foreach (GameObject upgradeObj in upgradeObjs)
        {
            upgrades.Add(upgradeObj.GetComponent<IUpgrade>());
        }
    }

    public List<IUpgrade> GetUpgrades()
    {
        if (upgrades.Count != upgradeObjs.Count) UpgradesInit();
        return upgrades;
    }

    #endregion
}
