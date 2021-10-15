using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Player player;

    private void Start()
    {
        Application.targetFrameRate = 60;
        player = new Player();
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Action.performed += LoadScene;
    }

    private void LoadScene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
