using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Purchasing;

[Serializable]
public class ConsumableItem
{
    public string name;
    public string id;
    public string description;
    public string price;
}
[Serializable]
public class NonConsumableItem
{
    public string name;
    public string id;
    public string description;
    public string price;
}
[Serializable]
public class SubscriptionItem
{
    public string name;
    public string id;
    public string description;
    public string price;
    public int timeDuration;
}

public class UnityShop : MonoBehaviour, IStoreListener
{
    IStoreController storeController;

    [SerializeField]
    private TMP_Text txtMoney, txtSubscribe, txtNoAdds;

    [SerializeField]
    private int money = 0, stepMoney;

    [SerializeField]
    private bool isSubscribed, isNoadds;

    [Header("ConsumableItem")]
    public ConsumableItem cItem;

    [Header("NonConsumableItem")]
    public NonConsumableItem ncItem;

    [Header("SubscriptionItem")]
    public SubscriptionItem sItem;

    private void Start()
    {
        txtMoney.text = $"Money: {money}";
        SetupBuilder();
    }

    private void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(cItem.id, ProductType.Consumable);
        builder.AddProduct(ncItem.id, ProductType.NonConsumable);
        builder.AddProduct(sItem.id, ProductType.Subscription);

        UnityPurchasing.Initialize(this, builder);
    }

    private void AddCoins(int coins)
    {
        money += coins;

        txtMoney.text = $"Money: {money}";
    }

    private void RemoveAds()
    {
        isNoadds = true;

        txtSubscribe.text = "NoAds complete!";
    }

    private void ActivatePass()
    {
        isSubscribed = true;

        txtSubscribe.text = "Subscribe complete!";
    }

    public void CheckNonConsumable(string id)
    {
        if (storeController != null)
        {
            var product = storeController.products.WithID(id);

            if (product != null)
            {
                if (product.hasReceipt)
                {
                    BuyNoAds();
                }
                else
                {
                    RemoveAds();
                }
            }
        }
    }

    public void CheckNonSubscribe(string id)
    {
        var subProduct = storeController.products.WithID(id);

        if (subProduct != null)
        {
            try
            {
                if (subProduct.hasReceipt)
                {
                    var subManager = new SubscriptionManager(subProduct, null);
                    var info = subManager.getSubscriptionInfo();
                    Debug.Log($"Product subscribe info time - {info.getExpireDate()}");

                    if (info.isSubscribed() == Result.True)
                    {
                        Debug.Log($"You are subscribed!");
                        BuySubscribe();
                    }
                    else
                    {
                        Debug.Log($"You are not subscribed!");
                        RemoveSubscribe();
                    }
                }
                else
                {
                    Debug.Log($"Receipt not found!");
                }
            }
            catch (Exception)
            {
                Debug.Log($"You are not on phone");
            }
        }
        else
        {
            Debug.Log($"Product subscribe not found!");
        }
    }

    #region Buttons
    public void BuyMoney()
    {
        storeController.InitiatePurchase(cItem.id);
    }

    public void RemoveMoney()
    {
        money = 0;
        txtMoney.text = $"Money: {money}";
    }

    public void BuyNoAds()
    {
        storeController.InitiatePurchase(ncItem.id);
    }

    public void RemoveNoAds()
    {
        isNoadds = false;
        txtSubscribe.text = "NoAds null!";
    }

    public void BuySubscribe()
    {
        storeController.InitiatePurchase(sItem.id);
    }

    public void RemoveSubscribe()
    {
        isSubscribed = false;
        txtSubscribe.text = $"Subscribe null";
    }
    #endregion

    #region Interfase purchase
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"Initialize failed - {error}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log($"Initialize failed - {error}, {message}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log($"Purchase comlete! - {product.definition.id}");

        if (product.definition.id == cItem.id)
        {
            AddCoins(stepMoney);
        }
        else if (product.definition.id == ncItem.id)
        {
            RemoveAds();
        }
        else if (product.definition.id == sItem.id)
        {
            ActivatePass();
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed!");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        CheckNonConsumable(ncItem.id);
        CheckNonSubscribe(sItem.id);

        Debug.Log("Initialize shop purshasing complete");
    }
    #endregion
}
