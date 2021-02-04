using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Singleton pattern
    public static ScoreManager instance;

    //--- Score : Timer ---
    [SerializeField] private TMP_Text timer;
    [SerializeField] private float setMinutes;
    [SerializeField] private float setSeconds;
    
    private bool totalTimePassed = false;
    private bool willGetBonus = true;

    private GameManager gameRef;

    //--- Score : Timer : Add Score From Time

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameRef = GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!totalTimePassed)
        {
            setSeconds -= Time.deltaTime;
        }

        if(setSeconds <= 0)
        {
            setSeconds += 60;
            setMinutes--;

            if(setMinutes <= 0)
            {
                setSeconds = 0;
                setMinutes = 0;

                totalTimePassed = true;
                gameRef.GameOver();
            }
        }

        TotalTime();

        
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

    private void TotalTime()
    {
        timer.text = "Time : " + setMinutes.ToString() + " : " + Mathf.RoundToInt(setSeconds);
    }
}
