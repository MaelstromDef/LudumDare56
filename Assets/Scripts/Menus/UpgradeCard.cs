using TMPro;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] TMP_Text txtPrice;
    [SerializeField] GameObject btnBuy;
    [SerializeField] string action;

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
}
