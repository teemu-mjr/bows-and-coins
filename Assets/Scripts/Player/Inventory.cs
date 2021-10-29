using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    // Public fields
    public TextMeshProUGUI coinText;

    // Events
    public static event EventHandler OnBuyStat;

    private void OnEnable()
    {
        GameManager.OnGameStart += GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart(object sender, EventArgs e)
    {
        AddCoins(0);
    }

    public void BuyStat(string statName)
    {
        OnBuyStat?.Invoke(this, EventArgs.Empty);
        Debug.Log(statName);
    }

    public void AddCoins(int amount)
    {
        Player.stats.coins += amount;
        RenderCoins();
    }

    public void RemoveCoins(int amount)
    {
        Player.stats.coins -= amount;
        RenderCoins();
    }

    private void RenderCoins()
    {
        coinText.text = $"{Player.stats.coins.ToString()}$";
    }

    private void OnDisable()
    {
        // Removing subscriptions
        GameManager.OnGameStart -= GameManager_OnGameStart;
    }
}
