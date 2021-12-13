using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public fields
    private static bool gameOver;

    // Private fields
    private PlayerInputActions playerInputActions;
    private Player player;
    private Options options;

    public static bool GameOver { get { return gameOver; } }

    // Events
    public static event EventHandler OnGameStart;


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        // creating player input actions
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // subscriptions
        playerInputActions.Player.Reset.performed += PlayerLoadScene_performed;
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;

        // creating the only player instance of the game
        player = new Player();
        OnGameStart?.Invoke(this, EventArgs.Empty);

        // creating the only OptionData instance
        options = new Options();

        gameOver = false;
    }

    public void LoadScene(int sceneIndex)
    {
        Player.stats.ResetCoins();
        player.SavePlayer();

        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    private void PlayerLoadScene_performed(InputAction.CallbackContext context)
    {
        LoadScene(1);
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        playerInputActions.Player.Disable();
        player.SavePlayer();
        gameOver = true;
    }

    private void OnDisable()
    {
        // remove subscriptions
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
        playerInputActions.Player.Reset.performed -= PlayerLoadScene_performed; 
    }
}
