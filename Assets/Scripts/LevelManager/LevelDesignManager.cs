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
        nextLevelNumber = PlayerPrefs.GetInt("NextLevelNumberKey", 0);
        levelPrefab = Instantiate(Levels[nextLevelNumber], transform.position, transform.rotation);
        levelPrefab.transform.parent = transform;
    }

    public void Next()
    {
        UIManager.Instance.rewardItem.SetActive(false);
        PlayerPrefs.SetInt("NextLevelNumberKey", nextLevelNumber + 1);
        UIManager.Instance.rewardItem.gameObject.SetActive(false);
        UIManager.Instance.earnedRewardPanel.gameObject.SetActive(false);
        UIManager.Instance.LoadScene(0);
    }

    public void SkipLevel()
    {
        //dasdsa
    }

    public void StuidoScene()
    {
        UIManager.Instance.rewardItem.gameObject.SetActive(false);
        UIManager.Instance.earnedRewardPanel.gameObject.SetActive(false);
        UIManager.Instance.LoadScene(1);
    }

    public void Restart()
    {
        UIManager.Instance.LoadScene(0);
    }
}
