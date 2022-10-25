using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesignManager : MonoBehaviour
{
    public List<GameObject> Levels = new List<GameObject>();
    public GameObject LevelsParrent;
    GameObject levelPrefab;
    int nextLevelNumber;

    void Start()
    {
        nextLevelNumber =  PlayerPrefs.GetInt("NextLevelNumberKey", 0);
        levelPrefab = Instantiate(Levels[nextLevelNumber], transform.position, transform.rotation);
        levelPrefab.transform.parent = transform;
        
    }

    public void Next()
    {
        PlayerPrefs.SetInt("NextLevelNumberKey", nextLevelNumber+1);
        UIManager.Instance.LoadScene(0);
    }

    public void Restart()
    {
        UIManager.Instance.LoadScene(0);
    }
}
