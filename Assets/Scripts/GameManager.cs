using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Public field
    public Shop shop;

    // Private fields
    private PlayerInputActions playerInputActions;
    private Player player;

    // Events
    public static event EventHandler OnGameStart;


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;

        // creating player input actions
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // subscriptions
        playerInputActions.Player.Reset.performed += PlayerLoadScene_performed;
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;

        // creating the only player instance of the game
        player = new Player();
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    public void LoadScene(int sceneIndex)
    {
        // TODO: Reset money / open the shop better
        Player.stats.ResetCoins();
        player.SavePlayer();

        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    private void PlayerLoadScene_performed(InputAction.CallbackContext context)
    {
        LoadScene(0);
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        playerInputActions.Player.Disable();
        player.SavePlayer();
    }

    private void OnDisable()
    {
        // remove subscriptions
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
        playerInputActions.Player.Reset.performed -= PlayerLoadScene_performed; 
    }
}
