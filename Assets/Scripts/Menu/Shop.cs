using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static bool isInShop = false;

    public GameObject shopCanvas;

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

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerDied;
    }
}
