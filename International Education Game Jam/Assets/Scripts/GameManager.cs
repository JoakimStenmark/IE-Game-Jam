using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject gameOverScreen;

    public WindowManager windowManager;

    public int numberOfScreens;
    int currentScreen;
    public float spaceBetweenScreens;
    bool goToNextScreen = false;
    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (windowManager.AreAllWindowsClean())
        {
            //Camera.main.transform.position += new Vector3(0, spaceBetweenScreens, 0);
            GameOver();
        }
    }


    public void GameOver()
    {
        Debug.Log("Game is Over");
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
