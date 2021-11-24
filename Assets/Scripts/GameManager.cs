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
        // creating player input actions
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // subscriptions
        playerInputActions.Player.Reset.performed += PlayerLoadScene_performed;
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;
    }

    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

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
        Time.timeScale = 0;
        player.SavePlayer();
        shop.OpenShop();
    }

    private void OnDisable()
    {
        // remove subscriptions
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
        playerInputActions.Player.Reset.performed -= PlayerLoadScene_performed; 
    }
}
