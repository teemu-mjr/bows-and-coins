using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Shop shop;

    private PlayerInputActions playerInputActions;
    private Player player;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        // Creating the only player instance of the game
        player = new Player();
        PlayerHealth.PlayerDied += OnPlayerDeath;
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Reset.performed += PlayerLoadScenePerformed;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void PlayerLoadScenePerformed(InputAction.CallbackContext context)
    {
        LoadScene();
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
