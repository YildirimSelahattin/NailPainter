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
        levelPrefab = Instantiate(Levels[1], transform.position, transform.rotation);
        levelPrefab.transform.parent = transform;
        Destroy(LevelsParrent.gameObject.transform.GetChild(0).gameObject);
    }

    public void Next()
    {
        SceneManager.LoadScene(0);
    }
}
