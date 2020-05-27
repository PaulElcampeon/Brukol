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
        SoundManager.instance.PlaySFX(2);

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

        SoundManager.instance.PlaySFX(1);

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

        SoundManager.instance.PlaySFX(1);

        GameManager.instance.UnPause();

        GameManager.instance.LoadScene("Game");
    }

    public void LoadMenu()
    {
        SoundManager.instance.PlaySFX(1);

        GameManager.instance.UnPause();

        GameManager.instance.LoadScene("Menu");
    }

    public void ShowPath()
    {
        PathFinder.instance.DisplayPath();
    }

    public void ChangeDifficulty(int level)
    {
        GameManager.instance.difficulty = level;

        ResetGame();
    }
}
