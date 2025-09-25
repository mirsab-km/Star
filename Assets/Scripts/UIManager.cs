using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject HomePanel;
    public GameObject levelsPanel;
    public GameObject PausePanel;
    public void PlayButton()
    {
        HomePanel.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void NewGameButton()
    {
        Debug.Log("New Game");

        // Reset unlocked levels
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();

        // Load Level 1 directly
        SceneManager.LoadScene("Level 1");
    }

    public void BackButton()
    {
        levelsPanel.SetActive(false);
        HomePanel.SetActive(true);
    }

    public void ExitButton()
    {
        Debug.Log("You exit from the game");
        Application.Quit(); 
    }

    public void Retry()
    {
        Time.timeScale = 1;

        // Reload the current active scene
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);

    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name; // e.g. "Level 1"
        int currentLevel = int.Parse(sceneName.Replace("Level ", "")); // removes "Level " and keeps number
        int nextLevel = currentLevel + 1;

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (nextLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        }

        // Load next if exists
        if (nextLevel <= 4) // since you only have 4 levels
        {
            SceneManager.LoadScene("Level " + nextLevel);
        }
        else
        {
            // If finished all - go back to main menu
            SceneManager.LoadScene("Main Menu");
        }
    }
}
