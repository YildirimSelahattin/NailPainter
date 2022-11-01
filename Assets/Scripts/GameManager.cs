using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Data.SqlTypes;

public class GameManager : MonoBehaviour
{
    [Header("Level References")]
    [SerializeField] TextAsset levelDataAsset;
    GameObject LevelsParent;
    [SerializeField] int levelIndex = 1;
    [Header("Shuffling Levels References")]
    GameObject[] spawnPoints;
    GameObject nailColorMachine;
    [Header("UI References :")]
    public Image fillImage;

    [Header("Transform References :")]
    LevelData currentLevel = new LevelData();

    // array that depends on players choices
    public int[] currentColorIndexArray = new int[5] { -1, -1, -1, -1, -1 };
    public int[] currentPatternIndexArray = new int[5] { -1, -1, -1, -1, -1 };
    public int[] currentDiamondIndexArray = new int[5] { -1, -1, -1, -1, -1 }; // pattern color array
    public int currentNailType;
    public List<int> currentBraceletIndexArray;
    public List<int> currentRingIndexArray;
    public bool isCleaned;
    public bool isManicured;

    [SerializeField] Camera CurrentCam;
    [SerializeField] Camera TargetCam;

    [Header("No Name")]
    public float matchRate;
    public static GameManager Instance;
    [HideInInspector] public bool gameStart = false;

    [Header("End Game")]
    public GameObject targetMinimap;
    public GameObject currentMinimap;
    public GameObject currentRightMinimap;
    Image currentMinimapImage;

    private void Start()
    {
        currentMinimapImage = currentMinimap.GetComponent<Image>();
        if (Instance == null)
        {
            Instance = this;
        }

        ReadCSVAndFillTargetArrays(levelIndex);
        StartCoroutine(OffCam());
        //diamond index array is passed to the machine to do
        DiamondMachineManager.diamondIndexArray = currentLevel.nailDiamondArray;
        ColorManager.Instance.ColorTargetHand(currentLevel.nailTypeAfterManicure, currentLevel.nailColorArray, currentLevel.nailPatternArray, currentLevel.nailDiamondArray);
    }

    IEnumerator OffCam()
    {
        yield return new WaitForSeconds(2);
        TargetCam.gameObject.SetActive(false);
        CurrentCam.gameObject.SetActive(false);
    }

    public float CompareTwoHands()
    {
        float totalParameterNumber = 0;
        float progress = 0;

        TargetCam.gameObject.SetActive(true);
        CurrentCam.gameObject.SetActive(true);
        StartCoroutine(OffCam());

        for (int i = 0; i < 5; i++)
        {

            //nail color compare
            if (currentLevel.nailColorArray[i] == currentColorIndexArray[i])
            {
                progress += 1;
            }
            //nail pattern compare
            if (currentLevel.nailPatternArray[i] == currentPatternIndexArray[i])
            {
                progress += 1;
            }
            //diamond compare
            if (currentLevel.nailDiamondArray[i] == currentDiamondIndexArray[i])
            {
                progress += 1;
            }
        }

        //nail type compare
        if (isManicured == true)
        {
            progress += 2.5f;
        }
        //is washed
        if (isCleaned == true)
        {
            progress += 2.5f;
        }

        totalParameterNumber = 20;
        //Debug.Log(progress);
        //Debug.Log(totalParameterNumber);
        matchRate = (float)((progress / totalParameterNumber) * 100);
        currentMinimapImage.fillAmount = matchRate / 100;
        return matchRate;
    }
    //Have TO DECİDE WHETHER LEVELS STARTS 1 OR 0
    public void ReadCSVAndFillTargetArrays(int levelIndex)
    {
        currentLevel.levelNumber = levelIndex;
        currentLevel.nailColorArray = new int[5];
        currentLevel.nailPatternArray = new int[5];
        currentLevel.nailDiamondArray = new int[5];
        int csvoffset = 7;
        int colorPartIndex = 0;
        int patternPartIndex = 1;
        int diamondPartIndex = 2;
        int shapePartIndex = 3;
        string[] wholeLevelData = levelDataAsset.text.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        string[] levelRowData = wholeLevelData[(levelIndex * 2) + 3].Split(new string[] { "," }, System.StringSplitOptions.None);

        Debug.Log(levelRowData[6] + "Thumb  " + levelRowData[0 + csvoffset] +
        "Pointer  " + levelRowData[1 + csvoffset] +
        "Middle  " + levelRowData[2 + csvoffset] +
        "ring  " + levelRowData[3 + csvoffset] +
        "pinkie  " + levelRowData[4 + csvoffset]);

        //FOR EVERY NAİL IN ONE HAND 
        for (int i = 0; i < 5; i++)
        {
            string[] nailData = levelRowData[i + csvoffset].Split(new string[] { "-" }, System.StringSplitOptions.None);
            currentLevel.nailColorArray[i] = Int16.Parse(nailData[colorPartIndex].Substring(1));
            currentLevel.nailPatternArray[i] = Int16.Parse(nailData[patternPartIndex].Substring(1));
            currentLevel.nailDiamondArray[i] = Int16.Parse(nailData[diamondPartIndex].Substring(1));
        }
        currentLevel.nailTypeAfterManicure = Int16.Parse(levelRowData[5 + csvoffset].Substring(1));

        /*
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(currentLevel.nailColorArray[i]);
        }
        */
    }
}