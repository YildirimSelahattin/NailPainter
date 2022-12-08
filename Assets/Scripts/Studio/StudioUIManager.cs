using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System;

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
    */

    [SerializeField] Material shiningMaterial;
    public static StudioUIManager Instance;
    [SerializeField] GameObject roomParentOfParents;
    [SerializeField] List<GameObject> roomParent;
    public float fillAmount;
    [SerializeField] Button upgradeWithMoneyButton;
    [SerializeField] Button upgradeFreelyButton;
    [SerializeField] Button upgradeWithAdButton;
    [SerializeField] GameObject priceTextParent;
    [SerializeField] GameObject ringCheck;
    [SerializeField] GameObject braceletCheck;
    float upgradableObjectsNumberPerTheme = 10;
    [SerializeField] GameObject footerContentParent;
    // bu ilk chil veya son child olabilir, upgrade buttonlar kapal� ba�lamal�
    int UPGRADE_CHILD_INDEX = 5;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI generalThemeText;
    [SerializeField] TextMeshProUGUI CongratsThemeText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Slider percentBar;
    [SerializeField] GameObject sliderHandle;
    [SerializeField] GameObject themeFinishedPanel;
    [SerializeField] ParticleSystem cloudParticle;
    [SerializeField] float[] scrollRectYPoss;
    [SerializeField] Sprite[] sliderColorArr;
    float lastRingScrollRectValue;
    float lastBraceletScrollRectValue;
    public ScrollRect ringScrollRect;
    public ScrollRect braceletScrollRect;
    bool motionStartedRing = true;
    bool motionStartedBracelet = true;
    public int ringIndex;
    public int braceletIndex;
    GameObject contentForRing;
    GameObject contentForBracelet;
    [SerializeField] GameObject normalTimesUIElementParent;
    [SerializeField] GameObject GiftTimesUIElementParent;
    string[] themeNames = { "BASIC", "YELLOW TOPAZ", "EMERALD", "RUBY", "PINK DIAMOND" };
    string[] themeNamesEnd = { "CONGRATS! BASIC COMPLETED", "CONGRATS! PINK DIAMOND COMPLETED", "CONGRATS! EMERALD COMPLETED", "CONGRATS! RUBY COMPLETED", "CONGRATS! PINK DIAMOND COMPLETED" };

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            braceletIndex = GameDataManager.Instance.currentBraceletIndex;
            ringIndex = GameDataManager.Instance.currentRingIndex;

            //PlayerPrefs.SetInt("NumberOfDiamondsKey", 250);
            // update money text
            moneyText.text = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0).ToString();
            //FOOTER RINGS AND BRACELET JOBS
            contentForRing = ringScrollRect.transform.GetChild(0).GetChild(0).gameObject;
            contentForBracelet = braceletScrollRect.transform.GetChild(0).GetChild(0).gameObject;
            for (int i = 1; i < GameDataManager.Instance.dataLists.room.generalThemeIndex; i++)
            {
                //for ring side 
                OpenRingOrBracelet(contentForRing.transform.GetChild(i).gameObject);
                //for braceletSide
                OpenRingOrBracelet(contentForBracelet.transform.GetChild(i).gameObject);
            }

            //RingScroll calcs
            lastRingScrollRectValue = scrollRectYPoss[ringIndex];
            ringScrollRect.DOVerticalNormalizedPos(scrollRectYPoss[ringIndex], 0.1f).OnComplete(() => StartCoroutine(AddRingListener()));

            if (ringIndex != 0)
            {
                StartCoroutine(ChangeLayerToUI(contentForRing, ringIndex));
            }
            //Bracelet calcs
            lastBraceletScrollRectValue = scrollRectYPoss[braceletIndex];
            braceletScrollRect.DOVerticalNormalizedPos(scrollRectYPoss[braceletIndex], 0.1f).OnComplete(() => StartCoroutine(AddBraceletListener()));
            if (braceletIndex != 0)
            {
                StartCoroutine(ChangeLayerToUI(contentForBracelet, braceletIndex));
            }

            ///ROOM JOBS
            //update slider
            sliderHandle.SetActive(true);
            percentBar.DOValue((float)GameDataManager.Instance.dataLists.room.nextUpgradeIndex / (float)upgradableObjectsNumberPerTheme, 1f).OnComplete(() => sliderHandle.SetActive(false));
            //write general theme index
            generalThemeText.text = themeNames[(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1)];
            CongratsThemeText.text = themeNamesEnd[(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1)];
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
                if (GameDataManager.Instance.dataLists.freeUpgradesLeft == 0)// if there is no free update;
                {
                    if (GameDataManager.Instance.upgradeAmountInSession < 2)
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

    public IEnumerator AddRingListener()
    {
        yield return new WaitForEndOfFrame();
        ringScrollRect.onValueChanged.AddListener(ringScrollRectCallBack);
        motionStartedRing = false;
    }

    public IEnumerator AddBraceletListener()
    {
        yield return new WaitForEndOfFrame();
        braceletScrollRect.onValueChanged.AddListener(braceletScrollRectCallBack);
        motionStartedBracelet = false;
    }

    public void OnCloseThemeFinishedPanelBtnClicked()
    {
        ringIndex = GameDataManager.Instance.dataLists.room.generalThemeIndex - 1;
        braceletIndex = GameDataManager.Instance.dataLists.room.generalThemeIndex - 1;
        lastRingScrollRectValue = scrollRectYPoss[ringIndex];
        ringScrollRect.DOVerticalNormalizedPos(scrollRectYPoss[ringIndex], 0.1f).OnComplete(() => StartCoroutine(AddRingListener()));
        OpenRingOrBracelet(contentForRing.transform.GetChild(ringIndex).gameObject);
        StartCoroutine(ChangeLayerToUI(contentForRing, ringIndex));

        //Bracelet calcs
        lastBraceletScrollRectValue = scrollRectYPoss[braceletIndex];
        braceletScrollRect.DOVerticalNormalizedPos(scrollRectYPoss[braceletIndex], 0.1f).OnComplete(() => StartCoroutine(AddBraceletListener()));
        OpenRingOrBracelet(contentForBracelet.transform.GetChild(braceletIndex).gameObject);
        StartCoroutine(ChangeLayerToUI(contentForBracelet, braceletIndex));
        roomParentOfParents.SetActive(true);
        normalTimesUIElementParent.SetActive(true);
        GiftTimesUIElementParent.SetActive(false);
        generalThemeText.text = themeNames[GameDataManager.Instance.dataLists.room.generalThemeIndex - 1];
        //FOOTER CALCULATİONS 
        //FOOTER RINGS AND BRACELET JOBS
        //ring
        OpenRingOrBracelet(contentForRing.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject);
        //bracelet
        OpenRingOrBracelet(contentForBracelet.transform.GetChild(GameDataManager.Instance.dataLists.room.generalThemeIndex - 1).gameObject);
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
            if (GameDataManager.Instance.upgradeAmountInSession < 2)
            {


                if (GameDataManager.Instance.dataLists.freeUpgradesLeft < 1)//there is no free upgrade
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
            else
            {
                upgradeWithAdButton.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    public void Upgrade()
    {
        if (GameDataManager.Instance.dataLists.freeUpgradesLeft > 0)
        {
            GameDataManager.Instance.dataLists.freeUpgradesLeft--;
        }

        if (GameDataManager.Instance.playSound == 1)
        {
            GameObject sound = new GameObject("sound");
            sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.diamondCollected);
            Destroy(sound, GameDataManager.Instance.diamondCollected.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
        }

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
            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.themeFinishedSound);
                Destroy(sound, GameDataManager.Instance.themeFinishedSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
            GameDataManager.Instance.dataLists.room.nextUpgradeIndex = 0;
            percentBar.DOValue(1, 1f);
            // theme finished parts done here;
            upgradeWithMoneyButton.gameObject.SetActive(false);
            upgradeFreelyButton.gameObject.SetActive(false);
            priceTextParent.gameObject.SetActive(false);
            roomParentOfParents.SetActive(false);
            normalTimesUIElementParent.SetActive(false);
            RemoveListeners();
            GameDataManager.Instance.dataLists.room.generalThemeIndex += 1;
            GiftTimesUIElementParent.SetActive(true);
        }
        else //prepare next update for that theme
        {
            sliderHandle.SetActive(true);
            percentBar.DOValue((float)GameDataManager.Instance.dataLists.room.nextUpgradeIndex / (float)upgradableObjectsNumberPerTheme, 1f).OnComplete(() => sliderHandle.SetActive(false));
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

    //Will be called when ScrollRect changes
    void ringScrollRectCallBack(Vector2 value)
    {
        Debug.Log("asdasd");
        if (motionStartedRing == false)
        {
            motionStartedRing = true;
            if (lastRingScrollRectValue > value.y && ringIndex != 4) // downwards
            {
                StartCoroutine(ChangeLayerToDefault(contentForRing, ringIndex));
                if (ringIndex < 4)
                {
                    ringIndex += 1;

                }
                StartCoroutine(ChangeLayerToUI(contentForRing, ringIndex));
                ringScrollRect.DOVerticalNormalizedPos(scrollRectYPoss[ringIndex], 1).OnComplete(() => StartCoroutine(OpenMotionRing(ringScrollRect.verticalNormalizedPosition)));

            }

            else if (lastRingScrollRectValue < value.y && ringIndex != 0)//upwards
            {
                StartCoroutine(ChangeLayerToDefault(contentForRing, ringIndex));
                if (ringIndex > 0)
                {
                    ringIndex -= 1;
                }
                StartCoroutine(ChangeLayerToUI(contentForRing, ringIndex));
                ringScrollRect.DOVerticalNormalizedPos(scrollRectYPoss[ringIndex], 1).OnComplete(() => StartCoroutine(OpenMotionRing(ringScrollRect.verticalNormalizedPosition)));
            }
            else
            {
                motionStartedRing = false;
            }
            if (value.y < 0.205)
            {
                ringScrollRect.verticalNormalizedPosition = 0.205f;
            }
        }
    }

    public IEnumerator ChangeLayerToUI(GameObject parent, int index)
    {
        yield return new WaitForSeconds(0.5f);
        parent.transform.GetChild(index).gameObject.layer = LayerMask.NameToLayer("UI");
        foreach (Transform child in parent.transform.GetChild(index))
        {
            child.gameObject.layer = LayerMask.NameToLayer("UI");
            child.GetChild(0).gameObject.layer = LayerMask.NameToLayer("UI");
        }
    }

    public IEnumerator ChangeLayerToDefault(GameObject parent, int index)
    {
        yield return new WaitForSeconds(0.2f);
        parent.transform.GetChild(index).gameObject.layer = LayerMask.NameToLayer("Default");
        foreach (Transform child in parent.transform.GetChild(index))
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
            child.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void braceletScrollRectCallBack(Vector2 value)
    {
        if (motionStartedBracelet == false)
        {
            motionStartedBracelet = true;
            if (lastBraceletScrollRectValue > value.y && braceletIndex != 4) // downwards
            {
                StartCoroutine(ChangeLayerToDefault(contentForBracelet, braceletIndex));
                if (braceletIndex < 4)
                {

                    braceletIndex += 1;
                }
                StartCoroutine(ChangeLayerToUI(contentForBracelet, braceletIndex));
                braceletScrollRect.DOVerticalNormalizedPos(scrollRectYPoss[braceletIndex], 1).OnComplete(() => StartCoroutine(OpenMotionBracelet(braceletScrollRect.verticalNormalizedPosition)));

            }
            else if (lastBraceletScrollRectValue < value.y && braceletIndex != 0)
            {
                StartCoroutine(ChangeLayerToDefault(contentForBracelet, braceletIndex));
                if (braceletIndex > 0)
                {
                    braceletIndex -= 1;
                }
                StartCoroutine(ChangeLayerToUI(contentForBracelet, braceletIndex));
                braceletScrollRect.DOVerticalNormalizedPos(scrollRectYPoss[braceletIndex], 1).OnComplete(() => StartCoroutine(OpenMotionBracelet(braceletScrollRect.verticalNormalizedPosition)));

            }
            else
            {
                motionStartedBracelet = false;
            }
            if (value.y < 0.205)
            {
                braceletScrollRect.verticalNormalizedPosition = 0.205f;
            }
        }
    }

    void OnDisable()
    {
        RemoveListeners();
    }

    public void RemoveListeners()
    {
        //Un-Subscribe To ScrollRect Event
        ringScrollRect.onValueChanged.RemoveListener(ringScrollRectCallBack);
        braceletScrollRect.onValueChanged.RemoveListener(braceletScrollRectCallBack);
    }

    public IEnumerator OpenMotionRing(float lastPos)
    {
        yield return new WaitForEndOfFrame();
        motionStartedRing = false;
        lastRingScrollRectValue = lastPos;
    }

    public IEnumerator OpenMotionBracelet(float lastPos)
    {
        yield return new WaitForEndOfFrame();
        motionStartedBracelet = false;
        lastBraceletScrollRectValue = lastPos;
    }

    public void OnSelectRingByIndexButtonClicked(int ringIndex)
    {
        GameDataManager.Instance.currentRingIndex = ringIndex;
        contentForRing.transform.GetChild(ringIndex).gameObject.SetActive(true);
    }

    public void OnSelectBraceletByIndexButtonClicked(int braceletIndex)
    {
        GameDataManager.Instance.currentBraceletIndex = braceletIndex;
        contentForBracelet.transform.GetChild(braceletIndex).gameObject.SetActive(true);
    }

    private void OpenRingOrBracelet(GameObject parent)
    {
        parent.GetComponent<Button>().interactable = true; // open button
        parent.transform.GetChild(1).gameObject.SetActive(false);//close transparency
    }
}