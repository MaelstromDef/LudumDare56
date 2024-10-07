using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour, IMenu
{
    [SerializeField] GameObject startMenu;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void NextPage()
    {
        Close();
        startMenu.GetComponent<IMenu>().Open();
    }

    public void PreviousPage()
    {
        Close();
        startMenu.GetComponent<IMenu>().Open();
    }

    public List<GameObject> GetAllOptions()
    {
        throw new System.NotImplementedException();
    }

    public int GetPageCount()
    {
        throw new System.NotImplementedException();
    }

    public List<GameObject> GetPageOptions()
    {
        throw new System.NotImplementedException();
    }

    public int GetPageSize()
    {
        throw new System.NotImplementedException();
    }

    public List<IMenu> GetSubMenus()
    {
        throw new System.NotImplementedException();
    }

    public void PerformAction(string actionName)
    {
        throw new System.NotImplementedException();
    }

    public void SetPageSize(int pageSize)
    {
        throw new System.NotImplementedException();
    }
}
