using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject soundOn;
    [SerializeField] GameObject soundOff;
    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;
    [SerializeField] GameObject loaderCanvas;
    [SerializeField] GameObject tapToStartCanvas;
    public GameObject bg;
    [SerializeField] GameObject follower;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject studioButton;
    [SerializeField] GameObject scoreArea;
    [SerializeField] GameObject settingsButton;
    [SerializeField] public GameObject losePanel;
    public GameObject rewardPanel;
    public GameObject earnedRewardPanel;
    [SerializeField] GameObject endPanel;
    public GameObject rewardItem;
    [SerializeField] GameObject handModel;
    [SerializeField] Animator targetPicAnimator;
    [SerializeField] GameObject gameMusicObject;
    [SerializeField] TextMeshProUGUI diamondNumberText;
    [SerializeField] TextMeshProUGUI matchRateText;
    [SerializeField] TextMeshProUGUI rewardPanelObjectPrice;
    [SerializeField] TextMeshProUGUI levelCounterText;
    [SerializeField] Image notificationParent;
    [SerializeField] GameObject popArtParent;
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject compareHandsPanel;
    [SerializeField] GameObject minimapBG;
    public GameObject minimap;
    [SerializeField] Sprite compareHandsWinSprite;
    [SerializeField] Sprite compareHandsLoseSprite;
    [SerializeField] FontStyle loseFont;
    [SerializeField] FontStyle winFont;
    public GameObject fireworks;
    public GameObject diamondMuliplier;
    public int multiplyAmount;
    int CurrentLevelNumber;
    public int currentLevelDiamond;
    public bool isTapped = false;
    public  GameObject mudParent;
    public GameObject acidParent;

    //[SerializeField] GameObject validateCanvas;
    //[SerializeField] Image progressBar;

    int isSoundOn;
    int isMusicOn;
    int LevelNumber;
    public static UIManager Instance;
    public int NumberOfDiamonds
    {
        get { return PlayerPrefs.GetInt("NumberOfDiamondsKey", 0); }   // get method
        set
        {
            PlayerPrefs.SetInt("NumberOfDiamondsKey", value);
            diamondNumberText.text = value.ToString();
        }
    }  // set method

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { }
    }

    private void Start()
    {
        follower.GetComponent<PlayerController>().enabled = false;
        PrepareUI();
        UpdateSound();
        UpdateMusic();
        CurrentLevelNumber = PlayerPrefs.GetInt("NextLevelNumberKey", 0);
        levelCounterText.text = ("LEVEL " + (CurrentLevelNumber + 1).ToString());
    }

    public void TapToStart()
    {
        PlayerStartMovement();
        tapToStartCanvas.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
        studioButton.gameObject.SetActive(false);
        scoreArea.gameObject.SetActive(true);
        targetPicAnimator.SetBool("isStart", true);
    }

    public async void LoadScene(int sceneID)
    {
        //_target = 0;
        //progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneID);
        Time.timeScale = 1;
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            //_target = scene.progress;
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1500);

        scene.allowSceneActivation = true;
        //loaderCanvas.SetActive(false);
    }
    /*
        void Update()
        {
            //progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _target, 3 * Time.deltaTime);
        }
    */

    //Run bittikten sonra calisan fonk.
    public void ShowEndScreen()
    {
        //Geçiş reklamı
        if (InterstitialAdManager.Instance.interstitialEndGame.IsLoaded())
        {
            InterstitialAdManager.Instance.interstitialEndGame.Show();
        }
        compareHandsPanel.SetActive(true);
        targetPicAnimator.SetBool("isEnd", true);
        settingsButton.SetActive(false);
        infoPanel.SetActive(false);
        pauseScreen.SetActive(false);
        levelCounterText.gameObject.SetActive(false);
        int matchRate = (int)GameManager.Instance.CompareTwoHands();
        matchRateText.text = "% " + matchRate.ToString();
        endPanel.SetActive(true);
        minimapBG.SetActive(true);

        //Basariya göre win-lose
        if (matchRate >= 0)
        {
            matchRateText.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(0, 192, 42, 255);
            compareHandsPanel.GetComponent<Image>().sprite = compareHandsWinSprite;
            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.winSound);
                Destroy(sound, GameDataManager.Instance.winSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
            winPanel.SetActive(true);
            StartCoroutine(DelayAndStartMovingLastAnim(0.01f));
        }
        else
        {
            matchRateText.GetComponent<TextMeshProUGUI>().outlineColor = new Color32(255, 0, 0, 255);
            compareHandsPanel.GetComponent<Image>().sprite = compareHandsLoseSprite;
            if (GameDataManager.Instance.playSound == 1)
            {
                GameObject sound = new GameObject("sound");
                sound.AddComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.loseSound);
                Destroy(sound, GameDataManager.Instance.loseSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
            }
            losePanel.SetActive(true);
        }
    }

    public void AfterEndGame()
    {
        winPanel.SetActive(false);
        matchRateText.gameObject.SetActive(false);
        levelCounterText.gameObject.SetActive(false);
        endPanel.SetActive(false);
    }

    //Muzik ve Sound Ayarları
    public void UpdateSound()
    {
        isSoundOn = GameDataManager.Instance.playSound;
        if (isSoundOn == 0)
        {
            soundOff.gameObject.SetActive(true);
            SoundsOff();
        }
        if (isSoundOn == 1)
        {
            soundOn.gameObject.SetActive(true);
            SoundsOn();
        }
    }

    public void UpdateMusic()
    {
        isMusicOn = GameDataManager.Instance.playMusic;
        if (isMusicOn == 0)
        {
            musicOff.gameObject.SetActive(true);
            MusicOff();
        }
        if (isMusicOn == 1)
        {
            musicOn.gameObject.SetActive(true);
            MusicOn();
        }
    }

    //-del-
    public void QuitGame()
    {
        Application.Quit();
    }
    //

    public void MusicOff()
    {
        GameDataManager.Instance.playMusic = 0;
        gameMusicObject.SetActive(false);
        musicOn.gameObject.SetActive(false);
        musicOff.gameObject.SetActive(true);
    }

    public void MusicOn()
    {
        GameDataManager.Instance.playMusic = 1;
        gameMusicObject.SetActive(true);
        musicOff.gameObject.SetActive(false);
        musicOn.gameObject.SetActive(true);

    }

    public void SoundsOff()
    {
        GameDataManager.Instance.playSound = 0;
        soundOn.gameObject.SetActive(false);
        soundOff.gameObject.SetActive(true);
    }
    public void SoundsOn()
    {
        GameDataManager.Instance.playSound = 1;
        soundOff.gameObject.SetActive(false);
        soundOn.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Oyun basinda ekrana dokunma ile cagirilan fonk.
    public void PlayerStartMovement()
    {
        follower.GetComponent<PlayerController>().enabled = true;
    }

    //Eger kazanılmış bir gelistirme varsa studio buttonunda bildirim veren fonk.
    public void PrepareUI()
    {
        //reload number of diamonds;
        NumberOfDiamonds = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0);
        //reload number of stacked changes, if 0 , close notification tab
        if (GameDataManager.Instance.dataLists.freeUpgradesLeft == 0)
        {
            notificationParent.gameObject.SetActive(false);
        }
        else
        {
            notificationParent.gameObject.SetActive(true);
            //notificationParent.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = GameDataManager.Instance.dataLists.stackedChangeParentIndexes.Count.ToString();
        }
    }

    public void TapToContinue()
    {
        isTapped = true;
        minimap.SetActive(false);
    }

    IEnumerator DelayAndStartMovingLastAnim(float second)
    {
        yield return new WaitForSeconds(second);
        handModel.gameObject.SetActive(false);
        //winPanel.SetActive(true);
        //endPanel.SetActive(false);
        GameManager.Instance.EnableMovingPolish();
    }

    //Run sonrası win ile bitmişse cagirilan fonk.
    public void OpenRewardPanel()
    {
        //instantiate the relevant upgradable item
        rewardPanelObjectPrice.text = GameDataManager.Instance.objectsByIndexArray[GameDataManager.Instance.dataLists.room.nextUpgradeIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[GameDataManager.Instance.dataLists.room.nextUpgradeIndex] + 1].price.ToString();
        Transform spawnPoint = rewardItem.transform.GetChild(1);
        Instantiate(GameDataManager.Instance.GetUpgradableObject(), spawnPoint.position, spawnPoint.rotation, spawnPoint);
        rewardItem.SetActive(true);
        rewardPanel.SetActive(true);
    }

    //Studio'da elmas ile geliştirme yapmak icin upgrade button'dan cagirilan fonk.
    public void GetUpgradeWithMoneyBtn()
    {
        NumberOfDiamonds -= GameDataManager.Instance.objectsByIndexArray[GameDataManager.Instance.dataLists.room.nextUpgradeIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[GameDataManager.Instance.dataLists.room.nextUpgradeIndex] + 1].price; ;
        GameDataManager.Instance.AddUpgradeToStack();
    }

    //Dogru tırnak boyadıktan sonra ekranda gösterilen yazi icin calisan fonk.
    public void CreateCelebrationPopUp()
    {
        int index = Random.Range(0, popArtParent.transform.childCount);
        if (popArtParent.transform.GetChild(index).gameObject.active == false)
        {
            popArtParent.transform.GetChild(index).gameObject.SetActive(true);
        }
    }

    public void NoTnxDiamondMuliplierPanel()
    {
        diamondMuliplier.SetActive(false);
        Debug.Log("DASDASDSDSDSDSDSDDSDSDSDS");
        OpenRewardPanel();
    }

    public void RewardDiamondMuliplierPanel()
    {
        diamondMuliplier.SetActive(false);
        OpenRewardPanel();
    }

    public void BuyUpgradeTotack()
    {
        GameDataManager.Instance.dataLists.freeUpgradesLeft++;
        NumberOfDiamonds -= (GameDataManager.Instance.objectsByIndexArray[GameDataManager.Instance.dataLists.room.nextUpgradeIndex][GameDataManager.Instance.dataLists.room.currentRoomIndexes[GameDataManager.Instance.dataLists.room.nextUpgradeIndex] + 1].price);
        UIManager.Instance.rewardPanel.gameObject.SetActive(false);
        UIManager.Instance.earnedRewardPanel.gameObject.SetActive(true);
    }

    public IEnumerator GetMultiplierReward()
    {
        yield return new WaitForEndOfFrame();
        UIManager.Instance.NumberOfDiamonds += (UIManager.Instance.currentLevelDiamond) * (multiplyAmount - 1);
        UIManager.Instance.diamondMuliplier.SetActive(false);
        UIManager.Instance.OpenRewardPanel();
    }
}