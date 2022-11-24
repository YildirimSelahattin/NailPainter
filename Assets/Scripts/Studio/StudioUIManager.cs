using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using static DataLists;
using UnityEngine.UI;
using DG.Tweening;
using System.Data.SqlTypes;
using System.Linq;
using System.Diagnostics.Tracing;
using UnityEngine.Rendering;

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

    [SerializeField] Material shiningMaterial;
    public static StudioUIManager Instance;

    [SerializeField] List<GameObject> roomParent;
    public float fillAmount;
    [SerializeField] Button upgradeWithMoneyButton;
    [SerializeField] Button upgradeFreelyButton;
    [SerializeField] Button upgradeWithAdButton;
    [SerializeField] GameObject priceTextParent;
    float upgradableObjectsNumberPerTheme = 11;
    [SerializeField] GameObject footerContentParent;

    // bu ilk chil veya son child olabilir, upgrade buttonlar kapal� ba�lamal�
    int UPGRADE_CHILD_INDEX = 5;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI generalThemeText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Slider percentBar;
    [SerializeField] GameObject themeFinishedPanel;
    [SerializeField] ParticleSystem cloudParticle;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerPrefs.SetInt("NumberOfDiamondsKey", 250);
            // update money text
            moneyText.text = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0).ToString();
            
            //FOOTER RINGS AND BRACELET JOBS
            for(int i = 0; i <( GameDataManager.Instance.dataLists.room.generalThemeIndex*2)-2; i++)
            {
                //for ring
                GameObject parent = footerContentParent.transform.GetChild(i).gameObject;
                parent.GetComponent<Button>().interactable = true; // open button
                parent.transform.GetChild(1).gameObject.SetActive(false);//close transparency
                Debug.Log("sa");
        
            }
            ///ROOM JOBS
            //update slider
            percentBar.DOValue((float)GameDataManager.Instance.dataLists.room.nextUpgradeIndex / (float)upgradableObjectsNumberPerTheme, 1f);
            //write general theme index
            generalThemeText.text = (GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).ToString();
            //if there is no objects to upgrade
            if (GameDataManager.Instance.dataLists.room.generalThemeIndex > 4)
            {
                //close all buttons
                upgradeFreelyButton.gameObject.SetActive(false);
                upgradeWithMoneyButton.gameObject.SetActive(false);
                priceTextParent.gameObject.SetActive(false);
            }
            else // if there is objects left to upgrade //stacked changes TODO
            {
                
                int upgradeParentIndex = GameDataManager.Instance.dataLists.room.nextUpgradeIndex;
                // open the relative arrows
                roomParent[upgradeParentIndex].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(true);
                //current object outline opened
                //  OUTL�NE CODE BELOW
                //roomParent[upgradeParentIndex].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex]).gameObject.GetComponent<Outline>().enabled = true;
                FadeInFadeOut(roomParent[upgradeParentIndex].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex]).gameObject);
                if(GameDataManager.Instance.dataLists.freeUpgradesLeft==0)// if there is no free update;
                {
                    if(GameDataManager.Instance.upgradeAmountInSession < 2)
                    {
                        upgradeWithMoneyButton.gameObject.SetActive(true);
                        priceTextParent.SetActive(true);
                        float price = GameDataManager.Instance.objectsByIndexArray[upgradeParentIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex] + 1].price;
                        //show price
                        priceText.text = price.ToString();
                        // if overpriced UIManager.Instance.NumberOfDiamonds
                        if (price > PlayerPrefs.GetInt("NumberOfDiamondsKey", 0))
                        {
                            upgradeWithMoneyButton.interactable = false;
                        }
                    }
                    else
                    {
                        upgradeWithAdButton.gameObject.SetActive(true);
                    }
                }
                else // if there is free upgrades
                {
                    priceTextParent.SetActive(false);
                    upgradeFreelyButton.gameObject.SetActive(true);
                }
            }
            // open the current childs, all the childs should start closed
            for (int i = 0; i < roomParent.Count; i++)
            {
                //FadeIn(roomParent[i].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[i]).gameObject);
                roomParent[i].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[i]).gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        Debug.Log(GameDataManager.Instance.dataLists.room.generalThemeIndex);
    }
    public void PrevScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OnUpgradeWithMoneyBtnClicked()
    {
        GameDataManager.Instance.upgradeAmountInSession++;
        // DECREASE MONEY  TDOO
        int remainingMoney = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) - GameDataManager.Instance.objectsByIndexArray[GameDataManager.Instance.dataLists.room.nextUpgradeIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[GameDataManager.Instance.dataLists.room.nextUpgradeIndex] + 1].price;
        PlayerPrefs.SetInt("NumberOfDiamondsKey", remainingMoney);
        moneyText.text = remainingMoney.ToString();
        Upgrade();
    }

    public void OnUpgradeWithAdButtonClicked()
    {
        RewardedAdManager.Instance.UpgradeRewardAd();
        
    }

    public void OnUpgradeStackedBtn()
    {
        Upgrade();
    }

    public void OnCloseThemeFinishedPanelBtnClicked()
    {
        themeFinishedPanel.SetActive(false);
        generalThemeText.text = (GameDataManager.Instance.dataLists.room.generalThemeIndex).ToString();
        //FOOTER CALCULATİONS 
        //FOOTER RINGS AND BRACELET JOBS
        for (int i = 2; i < (GameDataManager.Instance.dataLists.room.generalThemeIndex * 2) - 2; i++)
        {
            //for ring
            GameObject parent = footerContentParent.transform.GetChild(i).gameObject;
            parent.GetComponent<Button>().interactable = true; // open button
            parent.transform.GetChild(1).gameObject.SetActive(false);//close transparency


        }
        //if all themes finished
        if (GameDataManager.Instance.dataLists.room.generalThemeIndex == 5)
        {
            priceText.gameObject.SetActive(false);
            upgradeFreelyButton.gameObject.SetActive(false);
            upgradeWithMoneyButton.gameObject.SetActive(false);
        }
        else
        {
            //reset upgrades left
            GameDataManager.Instance.dataLists.room.nextUpgradeIndex = 0;
            //Open next upgrades button and outline
            //roomParent[0].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[0]).gameObject.GetComponent<Outline>().enabled = true;
            FadeInFadeOut(roomParent[0].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[0]).gameObject);
            //increase general theme index
            percentBar.DOValue(0, 1f);
            if (GameDataManager.Instance.dataLists.freeUpgradesLeft == 0)//there is no free upgrade
            {
                priceText.text = GameDataManager.Instance.objectsByIndexArray[0][GameDataManager.Instance.dataLists.room.currentRoomIndexes[0] + 1].price.ToString();
                upgradeFreelyButton.gameObject.SetActive(false);
                upgradeWithMoneyButton.gameObject.SetActive(true);
                priceTextParent.SetActive(true);
            }
            else //there is free update
            {
                priceTextParent.SetActive(false);
                upgradeFreelyButton.gameObject.SetActive(true);
                upgradeWithMoneyButton.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
     public void Upgrade()
    {
        
        //get parent object index 
        int parentIndexToUpgrade = GameDataManager.Instance.dataLists.room.nextUpgradeIndex;
        int upgradableParentsCurrentObjectIndex = GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade];
        //disable current parents arrow and outline
        roomParent[parentIndexToUpgrade].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(false);
        //OUTL�NE CODE BELOW
        //roomParent[parentIndexToUpgrade].transform.GetChild(upgradableParentsCurrentObjectIndex).gameObject.GetComponent<Outline>().enabled = false;
        // open and close indexes 
        OpenAndCloseObject(parentIndexToUpgrade, upgradableParentsCurrentObjectIndex);

        //CONTROLL �F CURRENT THEME �S F�N�SHED 
        if (GameDataManager.Instance.dataLists.room.nextUpgradeIndex == roomParent.Count - 1)
        {
            GameDataManager.Instance.dataLists.room.nextUpgradeIndex=0;
            percentBar.DOValue(1, 1f);
            // theme finished parts done here;
            upgradeWithMoneyButton.gameObject.SetActive(false);
            upgradeFreelyButton.gameObject.SetActive(false);
            priceTextParent.gameObject.SetActive(false);
            GameDataManager.Instance.dataLists.room.generalThemeIndex += 1;
            themeFinishedPanel.SetActive(true);
        }
        else //prepare next update for that theme
        {
            percentBar.DOValue((float)GameDataManager.Instance.dataLists.room.nextUpgradeIndex / (float)upgradableObjectsNumberPerTheme, 1f);
            //outline the next updates object 
            Debug.Log("parent index" + (parentIndexToUpgrade + 1) + "parent child index" + GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade + 1]);
            //roomParent[parentIndexToUpgrade + 1].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade+1]).gameObject.GetComponent<Outline>().enabled = true;
            FadeInFadeOut(roomParent[parentIndexToUpgrade + 1].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade + 1]).gameObject);
            roomParent[parentIndexToUpgrade + 1].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(true);

            if (GameDataManager.Instance.dataLists.freeUpgradesLeft < 1)
            {
                upgradeFreelyButton.gameObject.SetActive(false);
                if (GameDataManager.Instance.upgradeAmountInSession < 2)
                { // if there is option to upgrade with money
                    priceTextParent.SetActive(true);
                    upgradeWithMoneyButton.gameObject.SetActive(true);
                    float price = GameDataManager.Instance.objectsByIndexArray[parentIndexToUpgrade + 1][GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade] + 1].price;
                    priceText.text = price.ToString();
                    if (PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) < price)
                    {
                        upgradeWithMoneyButton.interactable = false;

                    }
                }
                else
                { // if ad button should take place 
                    priceTextParent.SetActive(false);
                    upgradeWithMoneyButton.gameObject.SetActive(false);
                    upgradeWithAdButton.gameObject.SetActive(true);
                }
               
            }
            if (GameDataManager.Instance.dataLists.freeUpgradesLeft > 0)
            {
                GameDataManager.Instance.dataLists.freeUpgradesLeft--;
            }
            
            //increase next upgrade parent
            GameDataManager.Instance.dataLists.room.nextUpgradeIndex += 1;

        }
        // increase relatve parents current child index
        GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade] += 1;
    }
    private void OpenAndCloseObject(int parentIndex, int currentChildIndex)
    {
        //Particles
        cloudParticle.Play();
        GameObject objectToClose = roomParent[parentIndex].transform.GetChild(currentChildIndex).gameObject;
        GameObject objectToOpen = roomParent[parentIndex].transform.GetChild(currentChildIndex + 1).gameObject;
        for (int i = 0; i < objectToClose.transform.childCount; i++)
        {
            Material[] matArray = objectToClose.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials;
            for (int j = 0; j < matArray.Length; j++)
            {
                matArray[j].DOKill();
                matArray[j].DOFade(0, 1f);
            }
            objectToClose.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials = matArray.SkipLast(1).ToArray();
        }
        objectToClose.SetActive(false);
        objectToOpen.SetActive(true);
        // disappear fade
    }

    private void FadeInFadeOut(GameObject relativeObject)
    {
        if (relativeObject.GetComponent<MeshRenderer>() != null)
        {
            Material[] matArray = relativeObject.GetComponent<MeshRenderer>().materials;
            List<Material> matList = matArray.ToList();
            matList.Add(shiningMaterial);
            relativeObject.GetComponent<MeshRenderer>().materials = matList.ToArray();
            matArray = relativeObject.GetComponent<MeshRenderer>().materials;
            FadeInFadeOutLoop(matArray[matArray.Length - 1]);
        }

        for (int i = 0; i < relativeObject.transform.childCount; i++)
        {
            Debug.Log("sui");
            Material[] matArray = relativeObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials;
            List<Material> matList = matArray.ToList();
            Material material = new Material(shiningMaterial);
            material.DOFade(0, 0.1f);
            matList.Add(material);
            relativeObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials = matList.ToArray();
            matArray = relativeObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials;
            FadeInFadeOutLoop(matArray[matArray.Length - 1]);
        }
    }
    private void FadeInFadeOutLoop(Material mat)
    {
        mat.DOFade(0f / 255f, 1).OnComplete(() => mat.DOFade(60f / 255f, 1).OnComplete(() => FadeInFadeOutLoop(mat)));
    }
}