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

    public TextMeshProUGUI movementSpeedLvl;
    public TextMeshProUGUI drawBackSpeedLvl;
    public TextMeshProUGUI arrowSpeedLvl;
    public TextMeshProUGUI flightTimeLvl;
    public TextMeshProUGUI damageLvl;

    public TextMeshProUGUI movementSpeedCost;
    public TextMeshProUGUI drawBackSpeedCost;
    public TextMeshProUGUI arrowSpeedCost;
    public TextMeshProUGUI flightTimeCost;
    public TextMeshProUGUI damageCost;


    private void Awake()
    {
        PlayerHealth.OnPlayerDeath += PlayerDied;
    }

    public void BuyPlayerStat(string statName)
    {
        if (Player.stats.IncrementStat(statName))
        {
            Debug.Log($"You bought upgraded {statName}");
        }
        else
        {
            Debug.Log("FAIL");
        }
        UpdateText();
    }

    private void UpdateText()
    {
        // update coins
        shopCoinText.text = $"You have {Player.stats.coins.ToString()}$ to spent";
        // update level
        movementSpeedLvl.text = Player.stats.movementSpeed.level.ToString();
        drawBackSpeedLvl.text = Player.stats.drawBackDelay.level.ToString();
        arrowSpeedLvl.text = Player.stats.arrowSpeed.level.ToString();
        flightTimeLvl.text = Player.stats.flightTimeMax.level.ToString();
        damageLvl.text = Player.stats.arrowDamage.level.ToString();
        // update cost
        movementSpeedCost.text = Player.stats.movementSpeed.cost.ToString() + "$";
        drawBackSpeedCost.text = Player.stats.drawBackDelay.cost.ToString() + "$";
        arrowSpeedCost.text = Player.stats.arrowSpeed.cost.ToString() + "$";
        flightTimeCost.text = Player.stats.flightTimeMax.cost.ToString() + "$";
        damageCost.text = Player.stats.arrowDamage.cost.ToString() + "$";
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
