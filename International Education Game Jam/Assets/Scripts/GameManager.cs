using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Text gameOverScreen;

    public WindowManager windowManager;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    


    public void GameOver()
    {
        Debug.Log("Game is Over");
        gameOverScreen.enabled = true;
        Time.timeScale = 0;
    }

}
