using UnityEngine;

public class QueenBeeUpgrade : MonoBehaviour, IUpgrade
{
    // Upgrade
    [Header("Upgrades")]
    [SerializeField] string upgradeName = "Queen Bee";
    [SerializeField] string upgradeDesc = "Automatically sends out workers from a hive at a rate of 1/s (increased at higher levels)";
    [SerializeField] int upgradeCost = 10;

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
        if (debugging) Debug.Log("QueenBeeUpgrade::Upgrade\nHive:\t" + hive.name);
        if(hive.GetHoney() < GetCost())
        {
            GameObject popup = Instantiate(popupPrefab);
            popup.GetComponent<PopUp>().Open(GetName(), "You don't have enough honey!");
            return;
        }

        hive.ClaimHoney(GetCost());
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