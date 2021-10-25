using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Player player;

    private void Start()
    {
        Application.targetFrameRate = 60;
        // Creating the only player instance of the game
        player = new Player();
        PlayerHealth.PlayerDied += OnPlayerDeath;
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Reset.performed += LoadScene;
    }

    private void LoadScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Player.stats.coins = 0;
    }

    private void OnPlayerDeath()
    {
        player.SavePlayer();
    }

    private void OnDestroy()
    {
        // Remove subscriptions
        PlayerHealth.PlayerDied -= OnPlayerDeath;
    }
}
