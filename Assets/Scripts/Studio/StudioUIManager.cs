using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEditor.Rendering.Universal;
using TMPro;
using static DataLists;
using static UnityEngine.Rendering.DebugUI;

public class StudioUIManager : MonoBehaviour
{

    public static StudioUIManager Instance;
    // Start is called before the first frame update

    // Update is called once per frame
    [SerializeField]List<GameObject> roomParent;
    public float fillAmount;
    int mirrorParentIndex=0;
    int windowParentIndex = 1;
    int flowerParentIndex = 2;
    int wallParentIndex = 3;
    int sofaParentIndex = 4;
    int komodinParentIndex = 5;
    int platformParentIndex = 6;
    int chairParentIndex = 7;
    int pictureParentIndex = 8;
    int tabureParentIndex = 9;
    int floorParentIndex = 10;
    int tableParentIndex = 11;

    public GeneralDataStructure[][] objectsByIndexArray;
    // bu ilk chil veya son child olabilir, upgrade buttonlar kapalý baþlamalý
    int upgradeButtonChildIndex;

    void Start()
    {
        if (Instance == null)
        {
        objectsByIndexArray = new GeneralDataStructure[][] {
        GameDataManager.Instance.dataLists.mirror,
        GameDataManager.Instance.dataLists.window,
        GameDataManager.Instance.dataLists.flower,
        GameDataManager.Instance.dataLists.wall,
        GameDataManager.Instance.dataLists.sofa,
        GameDataManager.Instance.dataLists.komodin,
        GameDataManager.Instance.dataLists.platform,
        GameDataManager.Instance.dataLists.chair,
        GameDataManager.Instance.dataLists.picture,
        GameDataManager.Instance.dataLists.tabure,
        GameDataManager.Instance.dataLists.floor,
        GameDataManager.Instance.dataLists.table,};
            Instance = this;

            for(int i = 0; i < roomParent.Count; i++)
            {
                //decide to show or not show the upgrade button depending on if it sreached general theme or not
                if (GameDataManager.Instance.dataLists.room.currentRoomIndexes[i] < GameDataManager.Instance.dataLists.room.generalThemeIndex)
                {
                    GameObject upgradeButton = roomParent[i].transform.GetChild(upgradeButtonChildIndex).gameObject;
                    upgradeButton.SetActive(true);
                    //get the next items upgrade  
                    upgradeButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = objectsByIndexArray[i][GameDataManager.Instance.dataLists.room.currentRoomIndexes[i]+1].price.ToString();
                }
            }
            // open the current childs, all the childs should be closed first
            for (int i = 0; i < roomParent.Count; i++)
            {
                roomParent[i].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[i]).gameObject.SetActive(true);
            }

            //see 
        }
    }
    public void PrevScene()
    {
        SceneManager.LoadScene(0);
    }

  

    // Update is called once per frame
    void Update()
    {

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;
        //Physics.Raycast(_rayPoint.transform.position, _rayPoint.transform.forward, out _raycastHit, range)
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.tag.Contains("Upgrade"))
                {
                    //get parent object index 
                    int parentIndexToUpgrade = Int16.Parse(hit.collider.transform.tag.Substring("upgrade".Length-1));
                    //get open and close indexes 
                    OpenAndCloseObject(parentIndexToUpgrade, GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade]);
                    // increase relatve parents current child index
                    GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade]+=1;
                    //decrease the updates left
                    GameDataManager.Instance.dataLists.room.upgradesLeft -= 1; 
                    //CONTROLL ÝF THEME ÝS FÝNÝSHED 
                    if(GameDataManager.Instance.dataLists.room.upgradesLeft == 0)
                    {
                        //this theme is finished
                        GameDataManager.Instance.dataLists.room.generalThemeIndex += 1;
                        //reset upgrades left
                        GameDataManager.Instance.dataLists.room.upgradesLeft = roomParent.Count-2;
                        for(int i = 0;i < roomParent.Count;i++)
                        {
                            roomParent[i].transform.GetChild(upgradeButtonChildIndex).gameObject.SetActive(true);
                        }
                    }
                    
                    //disable this upgradeObject
                    hit.collider.gameObject.SetActive(false);
                }
            }
            else
            {
                //m_MainCamera.transform.DOLocalMove(camOriginPos,1f);
            }
        }
    }

    public void OpenAndCloseObject(int parentIndex, int currentChildIndex)
    {
        roomParent[parentIndex].transform.GetChild(currentChildIndex).gameObject.SetActive(false);
        roomParent[parentIndex].transform.GetChild(currentChildIndex+1).gameObject.SetActive(true);
    }

   
    

}