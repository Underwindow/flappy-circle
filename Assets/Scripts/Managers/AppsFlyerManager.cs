using AppsFlyerSDK;
using System.Collections.Generic;
using UnityEngine;

public class AppsFlyerManager : MonoBehaviourSingletonPersistent<AppsFlyerManager>
{
    public void UserLogsIn()
    {
        var eventValue = new Dictionary<string, string>();
            eventValue.Add(AFInAppEvents.LOGIN, "");

        AppsFlyer.sendEvent(AFInAppEvents.LOGIN, eventValue);
    }

    public void UserRegisters()
    {
        var eventValue = new Dictionary<string, string>();
            eventValue.Add(AFInAppEvents.COMPLETE_REGISTRATION, "");
        
        AppsFlyer.sendEvent(AFInAppEvents.COMPLETE_REGISTRATION, eventValue);
    }

    public void UserPostsShare()
    {
        var time = Time.time;
        var eventValue = new Dictionary<string, string>();
            eventValue.Add(AFInAppEvents.SHARE, $"{time / 60}m {time % 60}s");

        AppsFlyer.sendEvent(AFInAppEvents.SHARE, eventValue);
    }

    public void UserCompletesLevel(DifficultyType difficulty)
    {
        var eventValue = new Dictionary<string, string>();
            eventValue.Add(AFInAppEvents.LEVEL, difficulty.ToString().ToLower());
        
        AppsFlyer.sendEvent(AFInAppEvents.LEVEL_ACHIEVED, eventValue);
    }

    public void UserCompletesPurchase(AFPurchase purchase)
    {
        AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, purchase.GetEventValues());
    }
}

[System.Serializable]
public class AFPurchase
{
    private Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();

    [SerializeField] private string currency;
    [SerializeField] private string revenue;
    [SerializeField] private string quantity;
    [SerializeField] private string contentType;

    public AFPurchase(
        string currency, 
        string revenue, 
        string quantity, 
        string contentType)
    {
        this.currency = currency;
        this.revenue = revenue;
        this.quantity = quantity;
        this.contentType = contentType;

        purchaseEvent.Add(AFInAppEvents.CURRENCY, this.currency);
        purchaseEvent.Add(AFInAppEvents.REVENUE, this.revenue);
        purchaseEvent.Add(AFInAppEvents.QUANTITY, this.quantity);
        purchaseEvent.Add(AFInAppEvents.CONTENT_TYPE, this.contentType);
    }

    public Dictionary<string, string> GetEventValues()
    {
        return purchaseEvent;
    }
}
