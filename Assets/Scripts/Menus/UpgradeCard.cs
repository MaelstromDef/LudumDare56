using TMPro;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text txtPrice;
    [SerializeField] GameObject btnBuy;
    [SerializeField] GameObject popupPrefab;

    [Header("Upgrade")]
    [SerializeField] string action;
    [SerializeField] string description;

    public GameObject GetBtnBuy()
    {
        return btnBuy;
    }

    public string GetAction()
    {
        return action;
    }

    public void SetPriceText(int price)
    {
        txtPrice.text = "$" + price.ToString();
    }

    public void ShowDescription()
    {
        GameObject popup = Instantiate(popupPrefab);
        popup.GetComponent<PopUp>().Open(action, description);
    }
}
