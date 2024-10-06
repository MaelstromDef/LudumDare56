using UnityEngine;

public interface IUpgrade
{
    public void Upgrade();          // Performs the upgrade.
    public string GetName();        // Gets the name of the upgrade.
    public string GetDescription(); // Gets the description of the upgrade.
    public int GetCost();           // Gets the cost of the upgrade.
}
