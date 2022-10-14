using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator targetPicAnimator;
    [SerializeField] GameObject soundOn;
    [SerializeField] GameObject soundOff;
    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;
    //[SerializeField] GameObject loaderCanvas;
    //[SerializeField] GameObject validateCanvas;
    //[SerializeField] Image progressBar;

    int isSoundOn;
    int isMusicOn;
    int LevelNumber;
    public static UIManager Instance;

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
        Time.timeScale = 1f;
        UpdateSound();
        UpdateMusic();
    }

    void Update()
    {
        if (PlayerController.Instance.firstTouch == true)
        {
            targetPicAnimator.SetBool("isStart", true);
        }
    }

    /*
        public async void LoadScene(int sceneID)
        {
            _target = 0;
            progressBar.fillAmount = 0;

            var scene = SceneManager.LoadSceneAsync(sceneID);
            Time.timeScale = 1;
            scene.allowSceneActivation = false;

            loaderCanvas.SetActive(true);

            do
            {
                await Task.Delay(100);
                _target = scene.progress;
            }
            while (scene.progress < 0.9f);

            await Task.Delay(1500);

            scene.allowSceneActivation = true;
            //loaderCanvas.SetActive(false);
        }

        void Update()
        {
            progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _target, 3 * Time.deltaTime);

            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //validateCanvas.gameObject.SetActive(true);
                }
            }
        }
    */
    public void UpdateSound()
    {
        isSoundOn = PlayerPrefs.GetInt("IsSoundOnKey", 1);
        if (isSoundOn == 0)
        {
            soundOff.gameObject.SetActive(true);
            SoundsOn();
        }
        if (isSoundOn == 1)
        {
            soundOn.gameObject.SetActive(true);
            SoundsOff();
        }
    }

    public void UpdateMusic()
    {
        isMusicOn = PlayerPrefs.GetInt("IsMusicOnKey", 1);
        if (isMusicOn == 0)
        {
            musicOff.gameObject.SetActive(true);
            MusicOn();
        }
        if (isMusicOn == 1)
        {
            musicOn.gameObject.SetActive(true);
            MusicOff();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MusicOn()
    {
        PlayerPrefs.SetInt("IsMusicOnKey", 0);
        //GameMusic.seagull_volume = 0;
        //GameMusic.BGmusic_volume = 0;
        //GameMusic.Instance.OnOffVolume();
        musicOn.gameObject.SetActive(false);
        musicOff.gameObject.SetActive(true);
    }

    public void MusicOff()
    {
        PlayerPrefs.SetInt("IsMusicOnKey", 1);
        //GameMusic.seagull_volume = 1;
        //GameMusic.BGmusic_volume = 0.50f;
        //GameMusic.Instance.OnOffVolume();
        musicOff.gameObject.SetActive(false);
        musicOn.gameObject.SetActive(true);
    }

    public void SoundsOn()
    {
        PlayerPrefs.SetInt("IsSoundOnKey", 0);
        //EggsGameManager.play_sound = false;
        //TimeFlewAwayAfterCounter.play_sound = false;
        //TimeCracker.play_sound = false;
        //TimeThrowScript.play_sound = false;
        //EnemyBirdMove.play_sound = false;
        //DialogHandler.play_sound = false;
        soundOn.gameObject.SetActive(false);
        soundOff.gameObject.SetActive(true);
    }
    public void SoundsOff()
    {
        PlayerPrefs.SetInt("IsSoundOnKey", 1);
        //EggsGameManager.play_sound = true;
        //TimeFlewAwayAfterCounter.play_sound = true;
        //TimeCracker.play_sound = true;
        //TimeThrowScript.play_sound = true;
        //DialogHandler.play_sound = true;
        //EnemyBirdMove.play_sound = true;
        soundOff.gameObject.SetActive(false);
        soundOn.gameObject.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}