using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxTries = 5;
    private int currentTries;

    public TextMeshProUGUI triesText;
    public TextMeshProUGUI scoreText;

    public int score;
    public int totalPairs;
    private int matchedPairs;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentTries = maxTries;
        UpdateTriesUI();
        UpdateScoreUI();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void CardMatched(int point)
    {
        score += point;
        matchedPairs++;
        DeductTry();

        UpdateScoreUI();

        if (matchedPairs >= totalPairs)
            WinGame();
    }

    public void CardMismatched(int point)  // call when player flips two cards that don’t match
    {
        score -= point;
        DeductTry();
        UpdateScoreUI();
    }

    void DeductTry()
    {
        currentTries--;
        UpdateTriesUI();

        if (currentTries <= 0)
            LoseGame();
    }
    void UpdateTriesUI()
    {
        if (triesText != null)
            triesText.text = "Tries: " + currentTries;
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void WinGame()
    {
        Debug.Log("You Won!");
        // Show Win UI panel here
    }

    void LoseGame()
    {
        Debug.Log("Game Over!");
        // Show Game Over UI panel here
    }

}
