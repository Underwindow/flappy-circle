using UnityEngine;

public class NoAdsPurchase : MonoBehaviour
{
    public AFPurchase aFPurchase;

    void Start()
    {
        if (PlayerPrefs.GetInt("NO_ADS_PURCHASED", 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void Purchase()
    {
        AppsFlyerManager.Instance.UserCompletesPurchase(aFPurchase);
        PlayerPrefs.SetInt("NO_ADS_PURCHASED", 1);
    }
}