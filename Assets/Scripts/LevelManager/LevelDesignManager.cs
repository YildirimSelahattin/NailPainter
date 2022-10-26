using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesignManager : MonoBehaviour
{//masa sandalye koltuk ayna komodin duvar zemin platform, pervaz rengi
    public List<GameObject> Levels = new List<GameObject>();
    public GameObject LevelsParrent;
    GameObject levelPrefab;
    int nextLevelNumber;

    void Start()
    {
        // PlayerPrefs.GetInt("NextLevelNumberKey", 0)
        nextLevelNumber = 0;
        levelPrefab = Instantiate(Levels[nextLevelNumber], transform.position, transform.rotation);
        levelPrefab.transform.parent = transform;
        
    }

    public void Next()
    {
        PlayerPrefs.SetInt("NextLevelNumberKey", nextLevelNumber+1);
        UIManager.Instance.LoadScene(0);
    }
}
