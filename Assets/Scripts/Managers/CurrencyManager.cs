using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    // Singleton
    public static CurrencyManager instance;

    // UI components
    [Header("Honey")]
    [SerializeField] TMP_Text txtHoney;
    [SerializeField] string honeyPreamble;

    // Currency
    int honey = 0;

    #region Unity

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }


    private void Start()
    {
        UpdateHoneyText();
    }

    #endregion

    #region Honey

    public int GetHoney()
    {
        return honey;
    }

    public void SetHoney(int honey)
    {
        this.honey = honey;
    }

    public void AddHoney(int amount)
    {
        honey += amount;
        UpdateHoneyText();
    }

    public void UpdateHoneyText()
    {
        txtHoney.text = honeyPreamble + honey;
    }

    public void ClaimHoney(int amount)
    {
        if(amount > honey) amount = honey;

        honey -= amount;
        UpdateHoneyText();
    }

    #endregion
}
