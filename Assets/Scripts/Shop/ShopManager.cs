using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopManager : MonoBehaviour
{
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "com.gamezmonster.shop_money_small":
                AddSmallCoins();
                break;
            case "com.gamezmonster.shop_money_medium":
                AddMediumCoins();
                break;
            case "com.gamezmonster.shop_money_big":
                AddBigCoins();
                break;
        }
    }

    private void AddSmallCoins()
    {
        int coins = PlayerPrefs.GetInt("Money");
        coins += 500;
        PlayerPrefs.SetInt("Money", coins);
    }
    
    private void AddMediumCoins()
    {
        int coins = PlayerPrefs.GetInt("Money");
        coins += 1000;
        PlayerPrefs.SetInt("Money", coins);
    }
    
    private void AddBigCoins()
    {
        int coins = PlayerPrefs.GetInt("Money");
        coins += 5000;
        PlayerPrefs.SetInt("Money", coins);
    }
}
