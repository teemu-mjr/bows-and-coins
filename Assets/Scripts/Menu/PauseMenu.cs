using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenu;
    public GameObject pauseObject;
    public GameObject optionsObject;

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

    public void HandleOptions()
    {
        pauseObject.SetActive(!pauseObject.activeSelf);
        optionsObject.SetActive(!optionsObject.activeSelf);
    }

    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseObject.SetActive(true);
        optionsObject.SetActive(false);
    }

    public void Continue()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.UI.Pause.performed -= HandlePause;
        }
    }
}
