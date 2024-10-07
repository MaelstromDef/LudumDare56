using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour, IMenu
{
    [SerializeField] List<GameObject> options;
    [SerializeField] GameObject gameMenu;

    public void Open()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        gameMenu.GetComponent<IMenu>().Open();
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
        throw new System.NotImplementedException();
    }

    public void NextPage()
    {
        Close();
        gameMenu.GetComponent<IMenu>().Open();
    }

    public void PerformAction(string actionName)
    {
        if (actionName.Equals("Quit")) Application.Quit();
        else if (actionName.Equals("Title")) SceneManager.LoadScene("TitleScreen");
    }

    public void PreviousPage()
    {
        Close();
        gameMenu.GetComponent<IMenu>().Open();
    }

    public void SetPageSize(int pageSize)
    {
        throw new System.NotImplementedException();
    }
}
