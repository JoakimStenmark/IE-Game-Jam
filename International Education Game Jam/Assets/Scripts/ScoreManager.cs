using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton pattern
    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    private int currentScore = 0;

    public void ModifyScore(int modification)
    {
        currentScore += modification;
        // TODO: Update UI
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
