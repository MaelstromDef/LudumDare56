using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour, IMenu
{
    [Header("Menu")]
    [SerializeField] List<GameObject> subMenuObjects = new List<GameObject>();
    List<IMenu> subMenus = new List<IMenu>();

    [SerializeField] List<GameObject> options = new List<GameObject>();

    [Header("Sound Toggles")]
    [SerializeField] Image musicImage;
    [SerializeField] Sprite sprMusicOn;
    [SerializeField] Sprite sprMusicOff;
    bool soundsOn = true;

    #region Unity

    private void Start()
    {
        foreach(GameObject obj in subMenuObjects)
            subMenus.Add(obj.GetComponent<IMenu>());
    }

    #endregion

    #region Sounds

    /// <summary>
    /// Turns sounds on and off
    /// </summary>
    /// <param name="toggle">True if on, false if off</param>
    public void ToggleSounds()
    {
        soundsOn = !soundsOn;
        if (soundsOn) musicImage.sprite = sprMusicOn;
        else musicImage.sprite = sprMusicOff;

        // Call the sound on/off toggle
        SoundsManager.instance.ToggleSound(soundsOn);
    }

    #endregion

    #region IMenu

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
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
        throw new System.NotImplementedException();
    }

    public List<IMenu> GetSubMenus()
    {
        return subMenus;
    }

    public void NextPage()
    {
        throw new System.NotImplementedException();
    }

    public void PerformAction(string actionName)
    {
        int subMenu = int.Parse(actionName);
        Close();
        subMenus[subMenu].Open();
    }

    public void PreviousPage()
    {
        throw new System.NotImplementedException();
    }

    public void SetPageSize(int pageSize)
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
