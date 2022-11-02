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

    public GeneralDataStructure[][] objectsByIndexArray;
    // bu ilk chil veya son child olabilir, upgrade buttonlar kapalý baþlamalý
    int UPGRADE_CHILD_INDEX = 5;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI generalThemeText;
    [SerializeField] TextMeshProUGUI moneyText;
    
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
            // update money text
            PlayerPrefs.SetInt("NumberOfDiamondsKey", 250);
            moneyText.text = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0).ToString();
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
                //  OUTLÝNE CODE BELOW
                //roomParent[upgradeParentIndex].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex]).gameObject.GetComponent<Outline>().enabled = true;
                FadeInFadeOut(roomParent[upgradeParentIndex].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex]).gameObject);
                float price = objectsByIndexArray[upgradeParentIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[upgradeParentIndex] + 1].price;
                //show price
                priceText.text = price.ToString();
                // if overpriced UIManager.Instance.NumberOfDiamonds
                if (price > PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) )
                {
                upgradeButton.interactable = false;
                }
            }
            // open the current childs, all the childs should start closed
            for (int i = 0; i < roomParent.Count; i++)
            {
                FadeIn(roomParent[i].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[i]).gameObject);
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

        int remainingMoney = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0) - objectsByIndexArray[GameDataManager.Instance.dataLists.room.nextUpgradeIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[GameDataManager.Instance.dataLists.room.nextUpgradeIndex] + 1].price;
        PlayerPrefs.SetInt("NumberOfDiamondsKey",remainingMoney);
        moneyText.text = remainingMoney.ToString();
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
        int upgradableParentsCurrentObjectIndex = GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade];
        //disable current parents arrow and outline
        roomParent[parentIndexToUpgrade].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(false);
        //OUTLÝNE CODE BELOW
        //roomParent[parentIndexToUpgrade].transform.GetChild(upgradableParentsCurrentObjectIndex).gameObject.GetComponent<Outline>().enabled = false;
        // open and close indexes 
        OpenAndCloseObject(parentIndexToUpgrade, upgradableParentsCurrentObjectIndex);
       

        //CONTROLL ÝF CURRENT THEME ÝS FÝNÝSHED 
        if (GameDataManager.Instance.dataLists.room.nextUpgradeIndex == roomParent.Count - 1)
        {
            //this theme is finished, increase general theme number
            GameDataManager.Instance.dataLists.room.generalThemeIndex += 1;
            //if all themes finished
            if (GameDataManager.Instance.dataLists.room.generalThemeIndex == 5)
            {
                priceText.gameObject.SetActive(false);
                upgradeButton.gameObject.SetActive(false);
                generalThemeText.text = (GameDataManager.Instance.dataLists.room.generalThemeIndex-1).ToString();
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
                generalThemeText.text = (GameDataManager.Instance.dataLists.room.generalThemeIndex-1).ToString();
            }
        }
        else //prepare next update for that theme
        {
            //outline the next updates object 
            Debug.Log("parent index" + (parentIndexToUpgrade + 1) + "parent child index" + GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade + 1]);
            //roomParent[parentIndexToUpgrade + 1].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade+1]).gameObject.GetComponent<Outline>().enabled = true;
            FadeInFadeOut(roomParent[parentIndexToUpgrade + 1].transform.GetChild(GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade + 1]).gameObject);
            roomParent[parentIndexToUpgrade + 1].transform.GetChild(UPGRADE_CHILD_INDEX).gameObject.SetActive(true);
          
            float price = objectsByIndexArray[parentIndexToUpgrade + 1][GameDataManager.Instance.dataLists.room.currentRoomIndexes[parentIndexToUpgrade] + 1].price;
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
        GameObject objectToClose = roomParent[parentIndex].transform.GetChild(currentChildIndex).gameObject;
        GameObject objectToOpen = roomParent[parentIndex].transform.GetChild(currentChildIndex + 1).gameObject;
        // disappear fade

        StartCoroutine(FadeOut(objectToClose));
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
                matArray[j].DOKill();
                matArray[j].DOFade(0, 1f);
            }
            objectToClose.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials = matArray.SkipLast(1).ToArray();
        }
        yield return new WaitForSeconds(1);
        objectToClose.SetActive(false);
    }
    private void FadeInFadeOut(GameObject relativeObject)
    {
        for (int i = 0; i < relativeObject.transform.childCount; i++)
        {
            Debug.Log("sui");
            Material[] matArray = relativeObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials;
            List<Material> matList=matArray.ToList();
            matList.Add(shiningMaterial);
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