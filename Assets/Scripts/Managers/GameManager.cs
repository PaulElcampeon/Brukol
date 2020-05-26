using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int difficulty { get; set;}

    public static GameManager instance;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);

        difficulty = 1;
    }

    public void SetDifficulty(int level)
    {
        difficulty = level;

        Debug.Log("Difficulty set to " + level);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Debug.Log("Quiting Game...");

        Application.Quit();
    }
}
