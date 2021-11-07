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
        Coin.OnCoinPickup += Coin_OnCoinPickup;
        PlayerHealth.OnPlayerDamage += PlayerHealth_OnPlayerDamage;
        ArenaController.OnNextWave += ArenaController_OnNextWave;

        GameManager.OnGameStart += GameManager_OnGameStart;
    }


    private void GameManager_OnGameStart(object sender, EventArgs e)
    {
        UpdateCoin();
        UpdateHealth(1);
        UpdateWave(0);
    }

    private void Coin_OnCoinPickup(object sender, EventArgs e)
    {
        UpdateCoin();
    }

    private void UpdateCoin()
    {
        coinText.text = Player.stats.coins.ToString() + "$";
    }

    private void PlayerHealth_OnPlayerDamage(object sender, PlayerHealth.HealthArgs e)
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
        Coin.OnCoinPickup -= Coin_OnCoinPickup;
        PlayerHealth.OnPlayerDamage -= PlayerHealth_OnPlayerDamage;
        ArenaController.OnNextWave -= ArenaController_OnNextWave;

        GameManager.OnGameStart -= GameManager_OnGameStart;
    }
}
