using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject infoMenu;
    public bool gameState;
    public static GamePause Instance;
    public static bool gamePaused = false;

    void Start()
    {
        gamePaused = false;
    }

    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true);
        //Time.timeScale = 0;
    }

    public void GameReturn()
    {
        gamePaused = false;
        //Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void GameStatus()
    {
        if (gamePaused == false)
        {
            pauseMenu.SetActive(true);
            gamePaused = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            gamePaused = false;
        }
    }

    public void openInfoPanel()
    {
        infoMenu.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void closeInfoModel()
    {
        infoMenu.gameObject.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
    }

    /*
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) // on close 
        {
            PauseGame();
        }
        else // on come back
        {
            //GameReturn();
        }
    }
    */
}