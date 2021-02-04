using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Singleton pattern
    public static ScoreManager instance;

    //--- Score : Timer ---
    public TMP_Text pointsBonus;
    public TMP_Text scoreNow;
    public TMP_Text totalScoreText;

    [SerializeField] private float changePointSpeed;

    private float setPoints;
    private bool totalBonusScoreLost = false;

    private GameManager gameRef;

    private int currentScore = 0;
    [SerializeField] private float totalBonusPoints;
    private int totalScoreCalculate;



    //--- Score : Timer : Add Score From Time ---

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameRef = GetComponent<GameManager>();
        ModifyScore(0);
    }

    private void Update()
    {
        if (!GameManager.instance.gameOver)
        {
            if (!totalBonusScoreLost)
            {
                setPoints += Time.deltaTime;
            }

            if (setPoints >= changePointSpeed)
            {
                totalBonusPoints -= 500;
                if (totalBonusPoints <= 0)
                {
                    totalBonusPoints = 0;
                    totalBonusScoreLost = true;
                }
                setPoints = 0;
            }

            TotalBonusPoints();
        }
        
            
        
    }



    // Modify the current score with a given modification
    public void ModifyScore(int modification)
    {
        currentScore += modification;
        scoreNow.text = "Total Score : " + currentScore.ToString("000000");
    }

    // Returns the current score the player has
    public int GetCurrentScore()
    {
        return currentScore;
    }

    private void TotalBonusPoints()
    {
        pointsBonus.text = "Extra Points : " + totalBonusPoints.ToString("000000");
    }

    public void EmptyText()
    {
        pointsBonus.text = "";
        scoreNow.text = "";
    }

    public void CalculateTotalScore()
    {
        totalScoreCalculate = currentScore + (int)totalBonusPoints;
        totalScoreText.text = "Final Score : " + totalScoreCalculate.ToString();
    }

    /*
     * 10 000 Bonus points start
     *  5 000 for performing a perfect combo
     *  2 000 for repairing a window
     *  1 000 for cleaning & wiping a window
     *    500 for defeating a zombie
    */

}
