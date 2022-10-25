using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject soundOn;
    [SerializeField] GameObject soundOff;
    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;
    [SerializeField] GameObject loaderCanvas;
    [SerializeField] GameObject gameStartCanvas;
    [SerializeField] Animator startButtonAnimator;
    [SerializeField] GameObject tapToStartCanvas;
    [SerializeField] GameObject follower;
    [SerializeField] Animator targetPicAnimator;
    [SerializeField] GameObject gameMusicObject;
    [SerializeField] TextMeshProUGUI diamondNumberText;
    [SerializeField] TextMeshProUGUI matchRateText;
    [SerializeField] TextMeshProUGUI definationText;
    //[SerializeField] GameObject validateCanvas;
    //[SerializeField] Image progressBar;

    int isSoundOn;
    int isMusicOn;
    int LevelNumber;
    public static UIManager Instance;
    public int numberOfDiamonds;
    public int NumberOfDiamonds
    {
        get { return numberOfDiamonds; }   // get method
        set
        {
            numberOfDiamonds = value;
            PlayerPrefs.SetInt("NumberOfDiamondsKey", value);
            diamondNumberText.text = numberOfDiamonds.ToString();
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
        //tapToStartCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //validateCanvas.gameObject.SetActive(true);
            }
        }
    }

    /*
        public void StartGameButton()
        {
            startButtonAnimator.SetBool("ClickPlayButton", true);
            tapToStartCanvas.gameObject.SetActive(true);
            //appaerAnim ekle
            StartCoroutine(Delay(3f));
        }
    */

    public void TapToStart()
    {
        PlayerStartMovement();
        tapToStartCanvas.gameObject.SetActive(false);
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

    public void ShowEndScreen()
    {
        GameManager.Instance.targetMinimap.SetActive(true);
        GameManager.Instance.currentMinimap.SetActive(true);
        GameManager.Instance.currentRightMinimap.SetActive(false);
        matchRateText.text =GameManager.Instance.CompareTwoHands().ToString();
    }

    public void UpdateSound()
    {
        isSoundOn = PlayerPrefs.GetInt("IsSoundOnKey", 1);
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
        isMusicOn = PlayerPrefs.GetInt("IsMusicOnKey", 1);
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MusicOff()
    {
        PlayerPrefs.SetInt("IsMusicOnKey", 0);
        gameMusicObject.SetActive(false);
        musicOff.gameObject.SetActive(false);
        musicOn.gameObject.SetActive(true);
    }

    public void MusicOn()
    {
        PlayerPrefs.SetInt("IsMusicOnKey", 1);
        gameMusicObject.SetActive(true);

        musicOn.gameObject.SetActive(false);
        musicOff.gameObject.SetActive(true);
    }

    public void SoundsOff()
    {
        PlayerPrefs.SetInt("IsSoundOnKey", 0);
        soundOn.gameObject.SetActive(false);
        soundOff.gameObject.SetActive(true);
    }
    public void SoundsOn()
    {
        PlayerPrefs.SetInt("IsSoundOnKey", 1);
        soundOff.gameObject.SetActive(false);
        soundOn.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator Delay(float adDelay)
    {
        yield return new WaitForSeconds(adDelay);
        gameStartCanvas.gameObject.SetActive(false);
    }

    public void PlayerStartMovement()
    {
        follower.GetComponent<PlayerController>().enabled = true;
    }

    public void IncreaseGold()
    {
        NumberOfDiamonds++;
    }
    public void PrepareUI()
    {
        NumberOfDiamonds = PlayerPrefs.GetInt("NumberOfDiamondsKey", 0);
    }
}