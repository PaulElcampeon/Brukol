using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private GameObject soundPanel;

    [SerializeField]
    private GameObject difficultyPanel;

    [SerializeField]
    private GameObject controlsPanel;

    [SerializeField]
    private GameObject creditsPanel;

    public void OpenSettingsPanel()
    {
        SoundManager.instance.PlaySFX(1);

        settingsPanel.SetActive(true);
    }

    public void OpenSoundPanel()
    {
        SoundManager.instance.PlaySFX(1);

        soundPanel.SetActive(true);
    }

    public void OpenControlsPanel()
    {
        SoundManager.instance.PlaySFX(1);

        controlsPanel.SetActive(true);
    }

    public void OpenCreditsPanel()
    {
        SoundManager.instance.PlaySFX(1);

        creditsPanel.SetActive(true);
    }

    public void OpenDifficultyPanel()
    {
        SoundManager.instance.PlaySFX(1);

        difficultyPanel.SetActive(true);
    }
    
    public void SetDifficulty(int level)
    {
        SoundManager.instance.PlaySFX(1);

        GameManager.instance.SetDifficulty(level);

        difficultyPanel.SetActive(false);
    }

    public void CloseActivePanel()
    {
        SoundManager.instance.PlaySFX(2);

        if (soundPanel.activeInHierarchy) { soundPanel.SetActive(false); return; }
        if (controlsPanel.activeInHierarchy) { controlsPanel.SetActive(false); return; }
        if (difficultyPanel.activeInHierarchy) { difficultyPanel.SetActive(false); return; }
        if (settingsPanel.activeInHierarchy) { settingsPanel.SetActive(false); return; }
        if (creditsPanel.activeInHierarchy) { creditsPanel.SetActive(false); return; }
    }

    public void LoadGame()
    {
        GameManager.instance.UnPause();
        GameManager.instance.LoadScene("Game");
    }
}
