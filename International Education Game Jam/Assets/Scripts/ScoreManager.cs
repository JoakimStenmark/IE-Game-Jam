using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Singleton pattern
    public static ScoreManager instance;

    //--- Score : Timer ---
    [SerializeField] private TMP_Text pointsBonus;
    [SerializeField] private int changePointSpeed;
    [SerializeField] private float totalBonusPoints;

    private float setPoints;
    private bool totalBonusScoreLost = false;
    private bool willGetBonus = true;

    private GameManager gameRef;

    //--- Score : Timer : Add Score From Time ---

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
        if (!totalBonusScoreLost)
        {
            setPoints += Time.deltaTime;
        }

        if (setPoints >= changePointSpeed)
        {
            totalBonusPoints -= 100;
            if(totalBonusPoints <= 100)
            {
                totalBonusPoints = 0;
                totalBonusScoreLost = true;
            }
            setPoints = 0;
        }

        TotalBonusPoints();


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

    private void TotalBonusPoints()
    {
        pointsBonus.text = "Extra Points : " + totalBonusPoints.ToString("000000");
    }

    /*
     * 10 000 Bonus points start
     *  5 000 for performing a perfect combo
     *  2 000 for repairing a window
     *  1 000 for cleaning & wiping a window
     *    500 for defeating a zombie
    */

}
