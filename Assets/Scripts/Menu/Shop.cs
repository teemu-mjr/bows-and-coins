using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static bool isInShop = false;

    public GameObject shopCanvas;

    private void Start()
    {
        PlayerHealth.PlayerDied += PlayerDied;
    }

    private void PlayerDied()
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

    private void OpenShop()
    {
        shopCanvas.SetActive(true);
        isInShop = true;
    }

    private void CloseShop()
    {
        shopCanvas.SetActive(false);
        isInShop = false;
        Player.stats.coins = 0;
    }

    private void OnDestroy()
    {
        PlayerHealth.PlayerDied -= PlayerDied;
    }
}
