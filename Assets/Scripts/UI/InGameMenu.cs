using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject playAgainPanel;

    [SerializeField]
    private GameObject inGameMenuPanel;

    public static InGameMenu instance;

    public bool isMenuOpen;

    private void Awake()
    {
        instance = this;
    }

    public void CloseMenu()
    {
        GameManager.instance.UnPause();

        isMenuOpen = false;

        inGameMenuPanel.SetActive(false);
    }

    public void ClosePlayAgainPanel()
    {
        GameManager.instance.UnPause();

        isMenuOpen = false;

        playAgainPanel.SetActive(false);
    }

    public void OpenMenuPanel()
    {
        if (playAgainPanel.activeInHierarchy) return;

        GameManager.instance.Pause();

        isMenuOpen = true;

        inGameMenuPanel.SetActive(true);
    }

    public void OpenPlayAgainPanel()
    {
        GameManager.instance.Pause();

        isMenuOpen = true;

        playAgainPanel.SetActive(true);
    }

    public void ResetGame()
    {
        if (inGameMenuPanel.activeInHierarchy) return;

        GameManager.instance.UnPause();

        GameManager.instance.LoadScene("Game");
    }

    public void LoadScene()
    {
        GameManager.instance.UnPause();

        GameManager.instance.LoadScene("Menu");
    }

    public void ShowPath()
    {
        PathFinder.instance.DisplayPath();
    }
}
