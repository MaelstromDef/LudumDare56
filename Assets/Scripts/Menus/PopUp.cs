using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        Destroy(gameObject);
    }
}
