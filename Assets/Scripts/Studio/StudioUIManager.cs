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

    [SerializeField]Material shiningMaterial;
    public static StudioUIManager Instance;

    [SerializeField] List<GameObject> roomParent;
    public float fillAmount;
    [SerializeField] Button upgradeButton;
    float upgradableObjectsNumberPerTheme = 11;

    // bu ilk chil veya son child olabilir, upgrade buttonlar kapal� ba�lamal�
    int UPGRADE_CHILD_INDEX = 5;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI generalThemeText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Slider percentBar;
    [SerializeField] GameObject themeFinishedPanel;
    
    void Start()
    {
        

        if (Instance == null)
        {
            Instance = this;
            if(GameDataManager.Instance.dataLists.showThemeFinishedPanel == 1)
            {
                themeFinishedPanel.SetActive(true);
                GameDataManager.Instance.dataLists.showThemeFinishedPanel = 0;
            }
            // update money text
            PlayerPrefs.SetInt("NumberOfDiamondsKey", 250);
            moneyText.text = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0).ToString();
            //update slider
            percentBar.DOValue((float)GameDataManager.Instance.dataLists.room.nextUpgradeIndex/ (float)upgradableObjectsNumberPerTheme,1f);
            //if there is no objects to upgrade
            if (GameDataManager.Instance.dataLists.room.generalThemeIndex > 4)
            {
                //open the next upgrades arrow
                upgradeButton.gameObject.SetActive(false);
                priceText.gameObject.SetActive(false);
            }
            else // if there is objects left to upgrade //stacked changes TODO
            {
                generalThemeText.text = (GameDataManager.Instance.dataLists.room.generalThemeIndex-1).ToString();
                int upgradeParentIndex = GameDataManager.Instance.dataLists.room.nextUpgradeIndex;
                // open the relative arrows
                 roomParent[upgradeParentIndex].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(true);
                //current object outline opened
                //  OUTL�NE CODE BELOW
                //roomParent[upgradeParentIndex].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex]).gameObject.GetComponent<Outline>().enabled = true;
                FadeInFadeOut(roomParent[upgradeParentIndex].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex]).gameObject);
                float price = GameDataManager.Instance.objectsByIndexArray[upgradeParentIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex] + 1].price;
                //show price
                priceText.text = price.ToString();
                // if overpriced UIManager.Instance.NumberOfDiamonds
                if (price > PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) )
                {
                upgradeButton.interactable = false;
                StartCoroutine(UpgradeStackedChanges());
                }
            }
            // open the current childs, all the childs should start closed
            for (int i = 0; i < roomParent.Count; i++)
            {
                //FadeIn(roomParent[i].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[i]).gameObject);
                roomParent[i].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[i]).gameObject.SetActive(true);
            }
            //stackedchanges
            
            

            
        }
    }
    public void PrevScene()
    {
        SceneManager.LoadScene(0);
    }


    public void OnUpgradeWithMoneyBtnClicked()
    {
        // DECREASE MONEY  TDOO

        int remainingMoney = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) - GameDataManager.Instance.objectsByIndexArray[GameDataManager.Instance.dataLists.room.nextUpgradeIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[GameDataManager.Instance.dataLists.room.nextUpgradeIndex] + 1].price;
        PlayerPrefs.SetInt("NumberOfDiamondsKey",remainingMoney);
        moneyText.text = remainingMoney.ToString();
        Upgrade();
    }
    public void OnUpgradeWithAdButtonClicked()
    {
        Upgrade();
    }
    private IEnumerator UpgradeStackedChanges()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < GameDataManager.Instance.dataLists.stackedChangeParentIndexes.Count; i++)
        {
            int upgradeIndex = GameDataManager.Instance.dataLists.stackedChangeParentIndexes[i];
            OpenAndCloseObject(upgradeIndex, GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeIndex]);
            GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeIndex] += 1;
            yield return new WaitForSeconds(1);
        }
        //empty stacked changes;
        GameDataManager.Instance.dataLists.stackedChangeParentIndexes = null;
    }

    public void OnCloseThemeFinishedPanelBtnClicked()
    {
        themeFinishedPanel.SetActive(false);
        //this theme is finished, increase general theme number
        GameDataManager.Instance.dataLists.room.generalThemeIndex += 1;
        //if all themes finished
        if (GameDataManager.Instance.dataLists.room.generalThemeIndex == 5)
        {
            priceText.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(false);
            generalThemeText.text = (GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).ToString();
        }
        else
        {
            //reset upgrades left
            GameDataManager.Instance.dataLists.room.nextUpgradeIndex = 0;
            //Open next upgrades button and outline
            //roomParent[0].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[0]).gameObject.GetComponent<Outline>().enabled = true;
            roomParent[0].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(true);
            FadeInFadeOut(roomParent[0].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[0]).gameObject);
            //increase general theme index
            generalThemeText.text = (GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).ToString();
            percentBar.DOValue((float)GameDataManager.Instance.dataLists.room.nextUpgradeIndex / (float)upgradableObjectsNumberPerTheme, 1f);
        }
    }
    // Update is called once per frame
    private void Upgrade()
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
            percentBar.DOValue(1, 1f);
            // theme finished parts done here;
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
          
            float price = GameDataManager.Instance.objectsByIndexArray[parentIndexToUpgrade + 1][GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade] + 1].price;
            priceText.text = price.ToString();
            if (PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) < price)
            {
                upgradeButton.interactable = false;
            }
            //increase next upgrade parent
            GameDataManager.Instance.dataLists.room.nextUpgradeIndex += 1;

        }
        // increase relatve parents current child index
        GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade] += 1;
    }

    private void OpenAndCloseObject(int parentIndex, int currentChildIndex)
    {
        GameObject objectToClose= roomParent[parentIndex].transform.GetChild(currentChildIndex).gameObject;
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
            List<Material> matList=matArray.ToList();
            Material material = new Material(shiningMaterial);
            material.DOFade(0,0.1f);
            matList.Add(material);
            relativeObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials = matList.ToArray();
            matArray = relativeObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials;
            FadeInFadeOutLoop(matArray[matArray.Length-1]);
        }
    }
    private void FadeInFadeOutLoop(Material mat )
    {
        mat.DOFade(0f/255f, 1).OnComplete(() =>mat.DOFade(60f/255f,1).OnComplete(()=>FadeInFadeOutLoop(mat)));
        
    }
  
}