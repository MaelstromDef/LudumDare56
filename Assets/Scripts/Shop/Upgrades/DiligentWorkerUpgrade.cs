using UnityEngine;

public class DiligentWorkerUpgrade : MonoBehaviour, IUpgrade
{
    // Upgrade
    [Header("Upgrades")]
    [SerializeField] string upgradeName = "Diligent Worker";
    [SerializeField] string upgradeDesc = "Workers have a percent chance of being “diligent”. Diligent workers have a percent chance to harvest more flowers.";
    [SerializeField] int upgradeCost = 10;
    [SerializeField] float upgradeCostIncreaseMultiplier = 1.5f;

    // UI Feedback
    [Header("UI")]
    [SerializeField] GameObject popupPrefab;

    // Hive
    Hive hive;

    // Debugging
    [Header("Debugging")]
    [SerializeField] bool debugging = false;

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
        if (debugging) Debug.Log("DiligentWorker::Upgrade\nHive:\t" + hive.name);
        if (CurrencyManager.instance.GetHoney() < GetCost())
        {
            GameObject popup = Instantiate(popupPrefab);
            popup.GetComponent<PopUp>().Open(GetName(), "You don't have enough honey!");
            return;
        }

        CurrencyManager.instance.ClaimHoney(GetCost());
        upgradeCost = (int)((float)upgradeCost * upgradeCostIncreaseMultiplier);
        hive.PurchaseFlowerVisitIncrease();
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
