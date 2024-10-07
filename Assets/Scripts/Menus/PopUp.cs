using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PopUp : MonoBehaviour
{
    [SerializeField] TMP_Text txtTitle;
    [SerializeField] TMP_Text txtDescription;

    public void Open(string title, string description)
    {
        txtTitle.text = title;
        txtDescription.text = description;
    }

    public void Close()
    {
        Debug.Log("POPUP CLOSE!!!");

        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        gameObject.SetActive(false);

        Invoke(nameof(DestroySelf), 1f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
