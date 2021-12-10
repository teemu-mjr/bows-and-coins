using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public fields
    public GameObject mainMenu;
    public GameObject optionsMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();

#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Menu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
