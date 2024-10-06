using System.Collections.Generic;
using UnityEngine;

public class HivePurchasingMenu : MonoBehaviour, IMenu
{
    // UI
    [Header("UI")]
    [SerializeField] GameObject btnOpen;
    [SerializeField] GameObject btnClose;

    [SerializeField] GameObject menu;

    [SerializeField] GameObject popupPrefab;

    // Hives
    [Header("Hives")]
    [SerializeField] List<GameObject> hives = new List<GameObject>();
    [SerializeField] int hiveCost = 100;
    [SerializeField] float hiveCostMultiplier = 1.5f;
    int currentUnlocked = 0;

    List<GameObject> hiveCards = new List<GameObject>();
    [SerializeField] GameObject hiveCardPrefab;

    #region HivePurchasingMenu

    public void Open()
    {
        btnOpen.SetActive(false);
        menu.SetActive(true);
    }

    public void Close()
    {
        menu.SetActive(false);
        btnOpen.SetActive(true);
    }

    public List<GameObject> GetAllOptions()
    {
        return hives;
    }

    public void PerformAction(string actionName)
    {
        // Have max hives
        if (currentUnlocked >= hives.Count)
        {
            GameObject obj = Instantiate(popupPrefab);
            obj.GetComponent<PopUp>().Open("Hive Purchasing Menu", "You have the max hive count, congrats!");
            return;
        }

        // Don't have honey
        if(CurrencyManager.instance.GetHoney() < hiveCost)
        {
            GameObject obj = Instantiate(popupPrefab);
            obj.GetComponent<PopUp>().Open("Hive Purchasing Menu", "You don't have enough honey!");
            return;
        }

        // Purchase
        CurrencyManager.instance.ClaimHoney(hiveCost);
        hives[currentUnlocked].SetActive(true);
        hiveCost = (int)((float)hiveCost * hiveCostMultiplier);
    }

    public int GetPageCount()
    {
        throw new System.NotImplementedException("Only one page on HivePurchasing Menu.");
    }

    public List<GameObject> GetPageOptions()
    {
        throw new System.NotImplementedException("Only one page on HivePurchasing Menu.");
    }

    public int GetPageSize()
    {
        throw new System.NotImplementedException("Only one page on HivePurchasing Menu.");
    }

    public List<IMenu> GetSubMenus()
    {
        throw new System.NotImplementedException("Only one page on HivePurchasing Menu.");
    }

    public void NextPage()
    {
        throw new System.NotImplementedException("Only one page on HivePurchasing Menu.");
    }

    public void PreviousPage()
    {
        throw new System.NotImplementedException("Only one page on HivePurchasing Menu.");
    }

    public void SetPageSize(int pageSize)
    {
        throw new System.NotImplementedException("Only one page on HivePurchasing Menu.");
    }

    #endregion
}
