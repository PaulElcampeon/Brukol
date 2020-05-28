using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject playAgainPanel;

    [SerializeField]
    private GameObject inGameMenuPanel;

    [SerializeField]
    private GameObject one;

    [SerializeField]
    private GameObject two;

    [SerializeField]
    private GameObject three;

    private Color normalColour = new Color(255f, 255f, 255f, 255f);
    private Color highlightColour = new Color(135f, 97f, 64f, 255f);
    private Color textColour = new Color(0, 0, 0, 255f);

    public static InGameUI instance;

    public bool isMenuOpen;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        int difficulty = GameManager.instance.difficulty;

        ColorBlock colors;

        if (difficulty == 1)
        {
            colors = one.GetComponent<Button>().colors;
            colors.normalColor = normalColour;
            colors.normalColor = highlightColour;
            one.GetComponent<Button>().colors = colors;
            one.GetComponentInChildren<Text>().color = textColour;
        } else if (difficulty == 2)
        {
            colors = two.GetComponent<Button>().colors;
            colors.normalColor = normalColour;
            colors.normalColor = highlightColour;
            two.GetComponent<Button>().colors = colors;
            two.GetComponentInChildren<Text>().color = textColour;
        } else if (difficulty == 3)
        {
            colors = three.GetComponent<Button>().colors;
            colors.normalColor = normalColour;
            colors.normalColor = highlightColour;
            three.GetComponent<Button>().colors = colors;
            three.GetComponentInChildren<Text>().color = textColour;
        }
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
