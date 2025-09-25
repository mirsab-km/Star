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

    private int currentStreak = 0;   
    public int normalScore = 5;     
    public int streakScore = 10;    


    [SerializeField] private GameObject GameWinPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject PauseButton;
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
    }

    void Start()
    {
        currentTries = maxTries;
        UpdateTriesUI();
        UpdateScoreUI();
    }

    void Update()
    {
        
    }
    public void CardMatched()
    {
        currentStreak++; // increase streak

        int pointsToAdd = currentStreak > 1 ? streakScore : normalScore; // if streak > 1, give streak bonus
        score += pointsToAdd;
        matchedPairs++;

        UpdateScoreUI();

        if (matchedPairs >= totalPairs)
        {
            WinGame();
        }
    }


    public void CardMismatched()
    {
        currentStreak = 0;  //streak broken
        score -= normalScore; 
        DeductTry();
        UpdateScoreUI();
    }

    void DeductTry()
    {
        currentTries--;

        if (score < 0)
        {
            score = 0;
        }

        UpdateTriesUI();

        if (currentTries <= 0)
        {
            currentTries = 0;
            LoseGame();
        }
    }
    void UpdateTriesUI()
    {
        if (triesText != null)
        {
            triesText.text = "Tries: " + currentTries;
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void WinGame()
    {
        Debug.Log("You Won!");
        AudioManager.Instance.VictorySound();
        GamePanel.SetActive(false);
        PauseButton.SetActive(false);
        triesText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        StartCoroutine(ShowWinPanel(1f));
    }

    IEnumerator ShowWinPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameWinPanel.SetActive(true);
    }

    void LoseGame()
    {
        Debug.Log("Game Over!");
        AudioManager.Instance.GameOverSound();
        GamePanel.SetActive(false);
        PauseButton.SetActive(false);
        triesText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        StartCoroutine(ShowGameOverPanel(1f));
    }

    IEnumerator ShowGameOverPanel(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameWinPanel.SetActive(true);
    }

}
