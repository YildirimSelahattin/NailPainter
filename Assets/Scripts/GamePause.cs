using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool gameState;
    public static GamePause Instance;
    public static bool gamePaused = false;

    void Start()
    {
        gamePaused = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused == true)
            {
                GameReturn();
            }
            else
            {
                PauseGame();
            }
        }

    }

    public void KeyDownPause()
    {
        if (gamePaused == true)
        {
            GameReturn();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameReturn()
    {
        gamePaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

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
}