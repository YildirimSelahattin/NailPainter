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
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;

public class StudioUIManager : MonoBehaviour
{
    //RULES
    /*
        int wallParentIndex = 0;
        int floorParentIndex = 1;
        int platformParentIndex = 2;
        int pictureParentIndex = 3;
        int tabureParentIndex = 4;
        int komodinParentIndex = 5;
        int sofaParentIndex = 6;
        int flowerParentIndex = 7;
        int chairParentIndex = 8;
        int tableParentIndex = 9;
        int mirrorParentIndex = 10;
};
*/
    public static StudioUIManager Instance;

    [SerializeField] List<GameObject> roomParent;
    public float fillAmount;
    [SerializeField] Button upgradeButton;

    public GeneralDataStructure[][] objectsByIndexArray;
    // bu ilk chil veya son child olabilir, upgrade buttonlar kapal� ba�lamal�
    int UPGRADE_CHILD_INDEX = 5;
    [SerializeField] TextMeshProUGUI priceText;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            objectsByIndexArray = new GeneralDataStructure[][] {
            GameDataManager.Instance.dataLists.wall,
            GameDataManager.Instance.dataLists.floor,
            GameDataManager.Instance.dataLists.platform,
            GameDataManager.Instance.dataLists.picture,
            GameDataManager.Instance.dataLists.tabure,
            GameDataManager.Instance.dataLists.komodin,
            GameDataManager.Instance.dataLists.sofa,
            GameDataManager.Instance.dataLists.flower,
            GameDataManager.Instance.dataLists.chair,
            GameDataManager.Instance.dataLists.table,
            GameDataManager.Instance.dataLists.mirror};
            //if there is no objects to upgrade
            if (GameDataManager.Instance.dataLists.room.generalThemeIndex > 6)
            {
                //open the next upgrades arrow
                upgradeButton.gameObject.SetActive(false);
                priceText.gameObject.SetActive(false);
            }
            else // if there is objects left to upgrade //stacked changes TODO
            {
                int upgradeParentIndex = GameDataManager.Instance.dataLists.room.nextUpgradeIndex;
                GameObject upgradeParent = roomParent[upgradeParentIndex];
                // open the relative arrows
                upgradeParent.transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(true);

                float price = objectsByIndexArray[upgradeParentIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex] + 1].price;
                //show price
                priceText.text = price.ToString();
                // if overpriced UIManager.Instance.NumberOfDiamonds
                /*if (price > PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) )
                {
                upgradeButton.interactable = false;
                }
                */
            }
            // open the current childs, all the childs should start closed
            for (int i = 0; i < roomParent.Count; i++)
            {
               FadeIn( roomParent[i].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[i]).gameObject);
            }



        }
    }
    public void PrevScene()
    {
        SceneManager.LoadScene(0);
    }


    public void OnUpgradeWithMoneyBtnClicked()
    {
        // DECREASE MONEY  TDOO
        PlayerPrefs.SetInt("NumberOfDiamondsKey", PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) - objectsByIndexArray[GameDataManager.Instance.dataLists.room.nextUpgradeIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[GameDataManager.Instance.dataLists.room.nextUpgradeIndex] + 1].price);
        Upgrade();
    }
    public void OnUpgradeWithAdButtonClicked()
    {
        Upgrade();
    }
    // Update is called once per frame
    private void Upgrade()
    {
        //get parent object index 
        int parentIndexToUpgrade = GameDataManager.Instance.dataLists.room.nextUpgradeIndex;
        // open and close indexes 
        OpenAndCloseObject(parentIndexToUpgrade, GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade]);
        // increase relatve parents current child index
        GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade] += 1;
        //CONTROLL �F THEME �S F�N�SHED 
        if (GameDataManager.Instance.dataLists.room.nextUpgradeIndex == roomParent.Count - 1)
        {
            //this theme is finished, increase general theme number
            GameDataManager.Instance.dataLists.room.generalThemeIndex += 1;
            //reset upgrades left
            GameDataManager.Instance.dataLists.room.nextUpgradeIndex = 0;
            //increase general theme index
            generalThemeText.text = GameDataManager.Instance.dataLists.room.generalThemeIndex.ToString();
        }
        else // prepare u� for next update
        {
            roomParent[parentIndexToUpgrade + 1].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(true);
            //increase next upgrade parent
            GameDataManager.Instance.dataLists.room.nextUpgradeIndex += 1;
        }
        //disable current parents arrow
        roomParent[parentIndexToUpgrade].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(false);

        
        
    }

    private void OpenAndCloseObject(int parentIndex, int currentChildIndex)
    {
        GameObject objectToClose = roomParent[parentIndex].transform.GetChild(currentChildIndex).gameObject;
        GameObject objectToOpen = roomParent[parentIndex].transform.GetChild(currentChildIndex + 1).gameObject;
        // disappear fade

        StartCoroutine( FadeOut(objectToClose));
        FadeIn(objectToOpen);
    }
    public void FadeIn(GameObject objectToOpen)
    {
        

        for (int i = 0; i < objectToOpen.transform.childCount; i++)
        {
            Material[] matArray = objectToOpen.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials;
            for (int j = 0; j < matArray.Length; j++)
            {
                Color color = matArray[j].color;
                color.a = 0;
                matArray[j].SetColor("_BaseColor", color);
                Debug.Log(matArray[j].color);
            }
            objectToOpen.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials = matArray;
        }
        
        objectToOpen.SetActive(true);

        for (int i = 0; i < objectToOpen.transform.childCount; i++)
        {
            Material[] matArray = objectToOpen.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials;
            for (int j = 0; j < matArray.Length; j++)
            {
                matArray[j].DOFade(1, 1f);
            }
        }
    }

    public IEnumerator FadeOut(GameObject objectToClose)
    {
        for (int i = 0; i < objectToClose.transform.childCount; i++)
        {
            Material[] matArray = objectToClose.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials;
            for (int j = 0; j < matArray.Length; j++)
            {
                matArray[j].DOFade(0, 1f);
                Debug.Log("hi");
            }
        }
        yield return new WaitForSeconds(1);
        objectToClose.SetActive(false);
    }
}