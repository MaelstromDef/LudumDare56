using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour, IMenu
{
    [Header("Menu Navigation")]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject btnOpen;
    [SerializeField] GameObject btnClose;
    [SerializeField] GameObject btnNext;
    [SerializeField] GameObject btnPrev;

    [Header("Menu Options")]
    [SerializeField] List<GameObject> upgrades = new List<GameObject>();    // Clickable menu options for upgrades.
    [SerializeField] List<GameObject> pageSlots = new List<GameObject>();   // Number of upgrades to have on each page.
    int currentPage = 0;

    [SerializeField] Shop shop;

    #region Unity

    private void Start()
    {
        btnOpen.GetComponent<Button>().onClick.AddListener(Open);
        btnClose.GetComponent<Button>().onClick.AddListener(Close);
        btnNext.GetComponent<Button>().onClick.AddListener(NextPage);
        btnPrev.GetComponent<Button>().onClick.AddListener(PreviousPage);
    }

    #endregion

    #region IMenu

    /// <summary>
    /// Opens the menu
    /// </summary>
    public void Open()
    {
        btnOpen.SetActive(false);
        menu.SetActive(true);

        currentPage = 0;
        RenderUpgrades();
    }

    /// <summary>
    /// Closes the menu.
    /// </summary>
    public void Close()
    {
        menu.SetActive(false);
        btnOpen.SetActive(true);
    }

    /// <summary>
    /// Goes to the next page.
    /// </summary>
    public void NextPage()
    {
        if (currentPage == GetPageCount()) return;  // Don't go forward too far.
        currentPage++;

        RenderUpgrades();
    }

    /// <summary>
    /// Goes to the previous page.
    /// </summary>
    public void PreviousPage()
    {
        if (currentPage == 0) return;
        currentPage--;

        RenderUpgrades();
    }

    /// <summary>
    /// Renders the current page's upgrades.
    /// </summary>
    private void RenderUpgrades()
    {
        // Remove current upgrades.
        foreach (GameObject slot in pageSlots)
            foreach (Transform child in slot.transform)
                Destroy(child.gameObject);

        // Replace with actual upgrades
        for (int i = 0; i < pageSlots.Count; i++)
        {
            // Instantiate upgrade
            int upgradeIndex = currentPage * pageSlots.Count + i;
            if (upgradeIndex >= upgrades.Count) return;
            GameObject upgradeObj = Instantiate(upgrades[upgradeIndex], pageSlots[i].transform);

            // Bind btnBuy to action.
            UpgradeCard card = upgradeObj.GetComponent<UpgradeCard>();

            GameObject btnBuy = card.GetBtnBuy();
            string action = card.GetAction();

            btnBuy.GetComponent<Button>().onClick.AddListener(() => PerformAction(action));

            // Set price
            card.SetPriceText(shop.GetPrice(action));
        }
    }

    /// <summary>
    /// Gets the number of upgrades per page.
    /// </summary>
    /// <returns>Number of upgrades per page.</returns>
    public int GetPageSize()
    {
        return pageSlots.Count;
    }

    /// <summary>
    /// Sets the number of upgrades per page.
    /// </summary>
    /// <param name="pageSize">Number of upgrades per page.</param>
    /// <exception cref="System.NotImplementedException">This method shouldn't be used.</exception>
    public void SetPageSize(int pageSize)
    {
        throw new System.NotImplementedException("Pages need to have slots to place the upgrades in. Use the pageSlots parameter in the editor.");
    }

    /// <summary>
    /// Gets the number of pages with the upgrade count.
    /// </summary>
    /// <returns>Page count.</returns>
    public int GetPageCount()
    {
        int pageCount = upgrades.Count / pageSlots.Count;
        if (upgrades.Count % pageSlots.Count == 0) pageCount--;

        return pageCount;
    }

    /// <summary>
    /// Gets the options in a page.
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetPageOptions()
    {
        List<GameObject> pageUpgrades = new List<GameObject>();
        for(int i = 0; i < pageSlots.Count; i++)
        {
            if (currentPage * pageSlots.Count + i >= upgrades.Count) return pageUpgrades;
            pageUpgrades.Add(upgrades[currentPage * pageSlots.Count + i]);
        }

        return pageUpgrades;
    }

    /// <summary>
    /// Gets a list of all the upgrade objects.
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetAllOptions()
    {
        return upgrades;
    }

    /// <summary>
    /// Gets a list of the submenus.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException">There's currently no shop submenus.</exception>
    public List<IMenu> GetSubMenus()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Calls the shop to perform an action.
    /// </summary>
    /// <param name="actionName">Action to perform.</param>
    public void PerformAction(string actionName)
    {
        shop.PerformAction(actionName);

        foreach(GameObject slot in pageSlots)
        {
            if (slot.transform.childCount == 0) return;

            UpgradeCard card = slot.transform.GetChild(0).GetComponent<UpgradeCard>();
            if(card.GetAction().Equals(actionName)) card.SetPriceText(shop.GetPrice(actionName));
        }
    }

    #endregion
}
