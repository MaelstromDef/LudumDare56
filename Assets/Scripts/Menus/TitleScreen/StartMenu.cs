using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour, IMenu
{
    [SerializeField] List<GameObject> subMenuObjects = new List<GameObject>();
    List<IMenu> subMenus = new List<IMenu>();

    [SerializeField] List<GameObject> options = new List<GameObject>();

    [Header("Debugging")]
    [SerializeField] bool debugging = false;

    #region IMenu

    /// <summary>
    /// Grabs all of the sub menus and places them into a list.
    /// </summary>
    public void Open()
    {
        if(subMenus.Count != subMenuObjects.Count)
            foreach (GameObject obj in subMenuObjects)
                subMenus.Add(obj.GetComponent<IMenu>());

        gameObject.SetActive(true);
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void Close()
    {
        Application.Quit();
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void NextPage()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Quits the game. 
    /// </summary>
    public void PreviousPage()
    {
        Application.Quit();
    }

    public List<GameObject> GetAllOptions()
    {
        return options;
    }

    public int GetPageCount()
    {
        return 1;
    }

    public List<GameObject> GetPageOptions()
    {
        return options;
    }

    public int GetPageSize()
    {
        return options.Count;
    }

    public List<IMenu> GetSubMenus()
    {
        return subMenus;
    }


    public void PerformAction(string actionName)
    {
        if (subMenus.Count != subMenuObjects.Count)
            foreach (GameObject obj in subMenuObjects)
                subMenus.Add(obj.GetComponent<IMenu>());

        int subMenu = int.Parse(actionName);
        if(debugging) Debug.Log("StartMenu::PerformAction\n" + subMenu);

        gameObject.SetActive(false);
        subMenus[subMenu].Open();
    }

    public void SetPageSize(int pageSize)
    {
        throw new System.NotImplementedException("Can not change StartMenu page size.");
    }

    #endregion
}
