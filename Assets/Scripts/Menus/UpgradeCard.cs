using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
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
}
