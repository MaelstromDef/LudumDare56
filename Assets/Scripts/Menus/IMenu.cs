using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface IMenu
{
    public void Open();                             // Opens the menu.
    public void Close();                            // Closes the menu.

    public List<IMenu> GetSubMenus();               // Gets a list of any sub menus (we probably will never need this)

    public int GetPageSize();                       // Gets the size of each page.
    public void SetPageSize(int pageSize);          // Set size of the pages.
    public int GetPageCount();                      // Gives a count of the pages.
    public void NextPage();                         // Goes to the next page.
    public void PreviousPage();                     // Goes to the previous page.


    public List<GameObject> GetPageOptions();
    public List<GameObject> GetAllOptions();        // Gets all options available in this menu.

    public void PerformAction(string actionName);   // Performs an action based on name.
}
