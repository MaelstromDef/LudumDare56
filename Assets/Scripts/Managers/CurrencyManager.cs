using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    // Singleton
    public static CurrencyManager instance;

    // UI components
    [SerializeField] TMP_Text txtHoney;
    [SerializeField] TMP_Text txtNectar;

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

    #endregion

    #region Honey



    #endregion

    #region Nectar

    #endregion
}
