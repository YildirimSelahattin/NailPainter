using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameManager : MonoBehaviour
{

    [Header("Level References")]
    GameObject LevelsParent;
    [Header("UI References :")]
    public Image fillImage;
    public TextMeshProUGUI tapToStartText;
    [Header("Transform References :")]

    public Transform player;
    public Transform end;

     
    // array that depends on players choices
     public int[] currentColorIndexArray = new int[5]  { -1,  -1, -1, -1, -1}; 
     public int[] currentPatternIndexArray = new int[5] { -1, -1, -1, -1, -1 };
     public int[] currentPatternColorIndexArray = new int[5]  { -1,  -1, -1, -1, -1};
     public int[] currentDiamondIndexArray = new int[5] { -1,-1,-1,-1,-1}; // pattern color array
     public List<int> currentBraceletIndexArray;
     public List<int> currentRingIndexArray;
     public bool isCleaned;
     public bool isManicured;

    [Header("Target Index Arrays")] // based on per nail
    [SerializeField] public int[] targetColorIndexArray = new int[5]; // color array
    [SerializeField] public int[] targetPatternIndexArray = new int[5]; // pattern array 
    [SerializeField] public int[] targetPatternColorIndexArray = new int[5]; // pattern color array
    [SerializeField] public int[] targetDiamondIndexArray = new int[5]; // pattern color array
    [SerializeField] public List<int> targetBraceletIndexArray; // bracelet array
    [SerializeField] public List<int> targetRingIndexArray; // ring array 

    [Header("No Name")]
    int spawnIndex = 0;
    public int matchRate;
    public static GameManager Instance;

    [HideInInspector] public bool gameStart = false;
    private void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        //diamond index array is passed to the machine to do
        DiamondMachineManager.diamondIndexArray = targetDiamondIndexArray;
    }

    public float CompareTwoHands()
    {


        float totalParameterNumber = 0;
        float progress = 0;
        for (int i = 0; i < 5; i++)
        {
            //nail color compare
            if (targetColorIndexArray[i] == currentColorIndexArray[i])
            {
                progress += 1;
            }


            //nail pattern color compare
            if (targetPatternColorIndexArray[i] == currentPatternColorIndexArray[i])
            {
                progress += 1;
            }
            //nail pattern compare
            if (targetPatternIndexArray[i] == currentPatternIndexArray[i])
            {
                progress += 1;

            }
            totalParameterNumber += 3;
            //look for diamond
           /*
            if (targetDiamondIndexArray[i] == currentDiamondIndexArray[i])
            {
                progress += 1;
            }*/
            //totalParameterNumber += 3;
        }
        /*
         * 
         * //Ring compare
        for (int i = 0; i < currentRingIndexArray.Count; i++)
        {
            if (currentRingIndexArray[i] == targetRingIndexArray[i])
            {
                progress += 1;
            }
            totalParameterNumber += 1;
        }
        //bracelet compare
        for (int i = 0; i < currentBraceletIndexArray.Count; i++)
        {
            if (currentBraceletIndexArray[i] == targetBraceletIndexArray[i])
            {
                progress += 1;
            }
            totalParameterNumber += 1;
        }*/
        // manicure compare
        if (isManicured == true)
        {
            progress += 1;
        }
        if (isCleaned == true)
        {
            progress += 1;
        }
        totalParameterNumber += 2;
        Debug.Log(progress);
        Debug.Log(totalParameterNumber);
        matchRate = (int)((progress / totalParameterNumber)*100);
        return matchRate;
    }

 
}
