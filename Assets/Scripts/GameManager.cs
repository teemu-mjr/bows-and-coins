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

    private void OnEnable()
    {
        playerInputActions.Player.Reset.performed += PlayerLoadScene_performed;
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;
        Inventory.OnBuyStat += Inventory_OnBuyStat;
    }


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }

    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        // Creating the only player instance of the game
        player = new Player();
        OnGameStart(this, EventArgs.Empty);
    }

    public void LoadScene()
    {
        Player.stats.coins = 0;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    private void Inventory_OnBuyStat(object sender, EventArgs e)
    {
        player.SavePlayer();
    }

    private void PlayerLoadScene_performed(InputAction.CallbackContext context)
    {
        LoadScene();
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        player.SavePlayer();
        shop.OpenShop();
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        // Remove subscriptions
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
        playerInputActions.Player.Reset.performed -= PlayerLoadScene_performed;
        Inventory.OnBuyStat -= Inventory_OnBuyStat;
    }
}
