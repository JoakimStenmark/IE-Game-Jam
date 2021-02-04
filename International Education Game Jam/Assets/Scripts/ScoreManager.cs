using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Singleton pattern
    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    private int currentScore = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Modify the current score with a given modification
    public void ModifyScore(int modification)
    {
        currentScore += modification;
        scoreText.text = "Score: " + currentScore.ToString("0000");
    }

    // Returns the current score the player has
    public int GetCurrentScore()
    {
        return currentScore;
    }
}
