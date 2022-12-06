using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesignManager : MonoBehaviour
{//masa sandalye koltuk ayna komodin duvar zemin platform, pervaz rengi
    public List<GameObject> Levels = new List<GameObject>();
    public GameObject LevelsParrent;
    GameObject levelPrefab;
    [SerializeField]int nextLevelNumber;
    [SerializeField] Material[] skyMat;
    [SerializeField] Material[] roadMat;

    void Start()
    {
        nextLevelNumber = PlayerPrefs.GetInt("NextLevelNumberKey", 0);
        Debug.Log(nextLevelNumber);
        levelPrefab = Instantiate(Levels[9], transform.position, transform.rotation);

      
        Material[] levelSkyboxMat = levelPrefab.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().materials;
        //levelSkyboxMat[0] = roadMat[nextLevelNumber % 4];
        //levelPrefab.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().materials = levelSkyboxMat;

        RenderSettings.skybox = skyMat[nextLevelNumber % 4];

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
