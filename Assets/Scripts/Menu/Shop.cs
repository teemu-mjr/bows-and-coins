using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // public fields
    public static bool isInShop = false;
    public GameObject shopCanvas;

    // Events
    public event EventHandler<ShopEventArgs> OnBuyStat;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerDied;
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

    public void BuyPlayerStat(string statName)
    {
        OnBuyStat?.Invoke(this, new ShopEventArgs { statName = statName });
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerDied;
    }
}
