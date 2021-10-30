using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    // public fields
    public static bool isInShop = false;
    public GameObject shopCanvas;
    public TextMeshProUGUI shopCoinText;

    // Events
    public static event EventHandler<ShopEventArgs> OnBuyStat;

    private void Awake()
    {
        PlayerHealth.OnPlayerDeath += PlayerDied;
    }

    public void BuyPlayerStat(string statName)
    {
        OnBuyStat?.Invoke(this, new ShopEventArgs { statName = statName });
    }

    private void PlayerDied(object sender, EventArgs e)
    {
        HandleShop();
    }

    public void HandleShop()
    {
        if (!isInShop)
        {
            OpenShop();
        }
        else if (isInShop)
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopCanvas.SetActive(true);
        isInShop = true;
    }

    private void CloseShop()
    {
        shopCanvas.SetActive(false);
        isInShop = false;
    }


    private void OnDisable()
    {
        // Unsubscribes
        PlayerHealth.OnPlayerDeath -= PlayerDied;
    }
}
