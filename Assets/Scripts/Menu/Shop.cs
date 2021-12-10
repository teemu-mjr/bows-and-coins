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

    // level texts
    public TextMeshProUGUI movementSpeedLvl;
    public TextMeshProUGUI drawBackSpeedLvl;
    public TextMeshProUGUI arrowSpeedLvl;
    public TextMeshProUGUI flightTimeLvl;
    public TextMeshProUGUI damageLvl;

    // cost texts
    public TextMeshProUGUI movementSpeedCost;
    public TextMeshProUGUI drawBackSpeedCost;
    public TextMeshProUGUI arrowSpeedCost;
    public TextMeshProUGUI flightTimeCost;
    public TextMeshProUGUI damageCost;
    public TextMeshProUGUI repeaterCost;
    public TextMeshProUGUI tripleCost;

    // toggle buttons
    public GameObject repeaterToggle;
    public GameObject tripleToggle;

    // events
    public static event EventHandler OnBuy;
    public static event EventHandler OnDecline;


    private void Awake()
    {
        PlayerHealth.OnPlayerDeath += PlayerDied;
        CloseShop();
    }

    public void BuyPlayerStat(string statName)
    {
        if (Player.stats.IncrementStat(statName))
        {
            OnBuy?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnDecline?.Invoke(this, EventArgs.Empty);
        }
        UpdateText();
    }

    public void ToggleStat(string statName)
    {
        if (statName == "repeater")
        {
            if (Player.stats.repeater.inUse)
            {
                Player.stats.repeater.inUse = false;
            }
            else
            {
                Player.stats.repeater.inUse = true;
            }
        }
        if (statName == "triple")
        {
            if (Player.stats.tripleShot.inUse)
            {
                Player.stats.tripleShot.inUse = false;
            }
            else
            {
                Player.stats.tripleShot.inUse = true;
            }
        }
        UpdateText();
    }

    private void UpdateText()
    {
        // update coins
        shopCoinText.text = $"You have {Player.stats.Coins.ToString()}$ to spent";
        // update level
        movementSpeedLvl.text = Player.stats.movementSpeed.level.ToString();
        drawBackSpeedLvl.text = Player.stats.drawBackDelay.level.ToString();
        arrowSpeedLvl.text = Player.stats.arrowSpeed.level.ToString();
        flightTimeLvl.text = Player.stats.flightTimeMax.level.ToString();
        damageLvl.text = Player.stats.arrowDamage.level.ToString();
        // update cost
        movementSpeedCost.text = Player.stats.movementSpeed.UICost;
        drawBackSpeedCost.text = Player.stats.drawBackDelay.UICost;
        arrowSpeedCost.text = Player.stats.arrowSpeed.UICost;
        flightTimeCost.text = Player.stats.flightTimeMax.UICost;
        damageCost.text = Player.stats.arrowDamage.UICost;
        repeaterCost.text = Player.stats.repeater.UICost;
        tripleCost.text = Player.stats.tripleShot.UICost;
        // update toggle buttons
        repeaterToggle.GetComponentInChildren<TextMeshProUGUI>().text = Player.stats.repeater.UIToggle;
        tripleToggle.GetComponentInChildren<TextMeshProUGUI>().text = Player.stats.tripleShot.UIToggle;

        if (Player.stats.repeater.maxed)
        {
            repeaterToggle.SetActive(true);
        }
        else
        {
            repeaterToggle.SetActive(false);
        }
        if (Player.stats.tripleShot.maxed)
        {
            tripleToggle.SetActive(true);
        }
        else
        {
            tripleToggle.SetActive(false);
        }
    }

    private void PlayerDied(object sender, EventArgs e)
    {
        StartCoroutine(OpenWithDelay());
    }

    private IEnumerator OpenWithDelay()
    {
        yield return new WaitForSeconds(2);
        OpenShop();
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
        UpdateText();
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
