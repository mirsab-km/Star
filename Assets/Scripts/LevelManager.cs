using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // Level 1 unlocked by default

        for (int i = 0; i < levels.Length; i++)
        {
            int levelNumber = i + 1;
            Image img = levels[i].button.GetComponent<Image>();

            if (levelNumber <= unlockedLevel)
            {
                // Unlocked
                img.sprite = levels[i].unlockedSprite;
                levels[i].button.interactable = true;

                int sceneIndex = levelNumber; // capture for listener
                levels[i].button.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("Level " + sceneIndex); // note the space
                });
            }
            else
            {
                // Locked
                img.sprite = levels[i].lockedSprite;
                levels[i].button.interactable = false;
            }
        }
    }
}
