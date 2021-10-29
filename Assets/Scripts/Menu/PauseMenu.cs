using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenu;

    private PlayerInputActions inputActions;

    private void OnEnable()
    {
        inputActions.UI.Pause.performed += HandlePause;
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.UI.Enable();
    }

    public void HandlePause(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            Pause();
        }
        else if (isPaused && !Shop.isInShop)
        {
            Continue();
        }
    }

    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.UI.Pause.performed -= HandlePause;
        }
    }
}
