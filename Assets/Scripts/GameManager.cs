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
     public int[] currentBraceletIndexArray =new  int[5];
     public int[] currentRingIndexArray = new int[5];
     public bool isCleaned;
     public bool isManicured;

    [Header("Target Index Arrays")] // based on per nail
    [SerializeField] public int[] targetColorIndexArray = new int[5]; // color array
    [SerializeField] public int[] targetPatternIndexArray = new int[5]; // pattern array 
    [SerializeField] public int[] targetPatternColorIndexArray = new int[5]; // pattern color array
    [SerializeField] public int[] targetBraceletIndexArray = new int[5]; // bracelet array
    [SerializeField] public int[] targetRingIndex = new int[5]; // ring array 


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

    public void CompareTwoHands()
    {

    }
}
