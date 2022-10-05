using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameManager : MonoBehaviour
{
    public GameObject failPanel;
    [Header("UI References :")]
    public Image fillImage;
    public TextMeshProUGUI tapToStartText;
    [Header("Transform References :")]

    public Transform player;
    public Transform end;

     
    // array that depends on players choices
     public int[] currentColorIndexArray = new int[5] ;
     public int[] currentPatternIndexArray = new int[5];
     public int[] currentPatternColorIndexArray =new  int[5];
     public int[] currentBraceletIndexArray;
     public int[] currentRingIndexArray ;
     public bool isCleaned;
     public bool isManicured;

    [Header("Target Index Arrays")] // based on per nail
    [SerializeField] public int[] targetColorIndexArray = new int[5]; // color array
    [SerializeField] public int[] targetPatternIndexArray = new int[5]; // pattern array 
    [SerializeField] public int[] targetPatternColorIndexArray = new int[5]; // pattern color array
    [SerializeField] public int[] targetBraceletIndexArray = new int[5]; // bracelet array
    [SerializeField] public int[] targetRingIndexArray; // ring array 

    [Header("No Name")]
    int spawnIndex = 0;
    int progress;
    public static GameManager Instance;

    [HideInInspector] public bool gameStart = false;
    private void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    public float CompareTwoHands()
    {
        int totalParameterNumber = 0;
        int progress = 0;
        for (int i = 0; i < 5; i++)
        {
            //nail color compare
            if (targetColorIndexArray[i] == currentBraceletIndexArray[i])
            {
                progress += 1;
            }


            //nail pattern color compare
            if (targetPatternColorIndexArray[i] == currentPatternColorIndexArray[i])
            {
                progress += 1;

            }
            //nail pattern compare
            if (targetPatternIndexArray[i] == currentPatternColorIndexArray[i])
            {
                progress += 1;

            }
            totalParameterNumber += 3;
        }
        //Ring compare
        for(int i = 0;i < currentRingIndexArray.Length; i++)
        {
            if (currentRingIndexArray[i] == targetRingIndexArray[i])
            {
                progress += 1;
            }
            totalParameterNumber += 1;
        }
        //bracelet compare
        for (int i = 0; i < currentBraceletIndexArray.Length; i++)
        {
            if (currentBraceletIndexArray[i] == targetBraceletIndexArray[i])
            {
                progress += 1;
            }
            totalParameterNumber += 1;
        }

        if(isManicured == true)
        {
            progress += 1;
        }
        if(isCleaned == true)
        {
            progress += 1;
        }
        totalParameterNumber += 2;
        return progress / totalParameterNumber;
    }

    public void NailPainted(GameObject nail,int colorIndex)
    {
        
    }
}
