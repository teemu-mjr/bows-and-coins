using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI waveText;

    void OnEnable()
    {
        // subscriptions
        PlayerStats.OnCoinChange += PlayerStats_OnCoinChange;
        PlayerHealth.OnPlayerDamage += PlayerHealth_OnPlayerDamage;
        ArenaController.OnNextWave += ArenaController_OnNextWave;

        GameManager.OnGameStart += GameManager_OnGameStart;
    }

    private void PlayerStats_OnCoinChange(object sender, CoinEventArgs e)
    {
        UpdateCoin();
    }

    private void GameManager_OnGameStart(object sender, EventArgs e)
    {
        UpdateCoin();
        UpdateHealth(3);
        UpdateWave(0);
    }

    private void UpdateCoin()
    {
        coinText.text = Player.stats.Coins.ToString() + "$";
    }

    private void PlayerHealth_OnPlayerDamage(object sender, PlayerHealthArgs e)
    {
        UpdateHealth(e.health);
    }

    private void UpdateHealth(float health)
    {
        healthText.text = $"HP: {health.ToString()}";
    }

    private void ArenaController_OnNextWave(object sender, ArenaController.WaveArgs e)
    {
        UpdateWave(e.waveNumber);
    }

    private void UpdateWave(int waveNumber)
    {
        waveText.text = $"Wave: {waveNumber.ToString()}";
    }

    private void OnDisable()
    {
        // unsubscribes
        PlayerStats.OnCoinChange -= PlayerStats_OnCoinChange;
        PlayerHealth.OnPlayerDamage -= PlayerHealth_OnPlayerDamage;
        ArenaController.OnNextWave -= ArenaController_OnNextWave;

        GameManager.OnGameStart -= GameManager_OnGameStart;
    }
}
