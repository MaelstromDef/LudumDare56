using UnityEngine;

public class EfficiencyUpgrade : MonoBehaviour, IUpgrade {
    // Upgrade
    [Header("Upgrades")]
    [SerializeField] string upgradeName = "Honey Efficiency";
    [SerializeField] string upgradeDesc = "Decreases the amount of nectar required to poduce honey.";
    [SerializeField] int upgradeCost = 5;

    // UI Feedback
    [Header("UI")]
    [SerializeField] GameObject popupPrefab;

    // Hive
    Hive hive;

    // Debugging
    [Header("Debugging")]
    [SerializeField] bool debugging = false;

    #region IUpgrade

    public string GetName() {
        return upgradeName;
    }

    public string GetDescription() {
        return upgradeDesc;
    }

    public int GetCost() {
        return upgradeCost;
    }

    public void Upgrade() {
        if (debugging) Debug.Log("EfficiencyUpgrade::Upgrade\nHive:\t" + hive.name);
        if (CurrencyManager.instance.GetHoney() < GetCost()) {
            GameObject popup = Instantiate(popupPrefab);
            popup.GetComponent<PopUp>().Open(GetName(), "You don't have enough honey!");
            return;
        }

        CurrencyManager.instance.ClaimHoney(GetCost());
        hive.IncreaseEfficiecy();
    }

    public Hive GetHive() {
        return hive;
    }

    public void SetHive(Hive hive) {
        this.hive = hive;
    }

    #endregion
}