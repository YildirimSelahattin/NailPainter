using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class RewardedAdManager : MonoBehaviour
{
    public RewardedAd rewardedAd;
    public RewardedAd rewardedSkipAd;
    public static RewardedAdManager Instance;
    int nextLevelNumber;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        RequestRewarded();
        RequestSkipRewarded();

        nextLevelNumber = PlayerPrefs.GetInt("NextLevelNumberKey", 0);
    }

    private void RequestRewarded()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        /*
                // Called when an ad request has successfully loaded.
                this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
                // Called when an ad request failed to load.
                this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
                // Called when an ad is shown.
                this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
                // Called when an ad request failed to show.
                this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
                // Called when the user should be rewarded for interacting with the ad.
                this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
                // Called when the ad is closed.
                this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        */
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    /*
        public void HandleRewardedAdLoaded(object sender, EventArgs args)
        {
            Debug.Log("Reklam Yuklendi");
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.Log("Reklam Fail");

        }

        public void HandleRewardedAdOpening(object sender, EventArgs args)
        {
            Debug.Log("Reklam Acildi");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            Debug.Log("Reklam Gosterilemedi");
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            Debug.Log("Reklam Kapatildi");
        }
    */

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        UIManager.Instance.rewardPanel.gameObject.SetActive(false);
        UIManager.Instance.earnedRewardPanel.gameObject.SetActive(true);
        Debug.Log("Hey");
        GameDataManager.Instance.AddUpgradeToStack();
        RequestRewarded();

    }

    public void FurnitureRewardAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    private void RequestSkipRewarded()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedSkipAd = new RewardedAd(adUnitId);

        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedSkipAd.OnUserEarnedReward += HandleUserEarnedSkipReward;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedSkipAd.LoadAd(request);
    }

    public void HandleUserEarnedSkipReward(object sender, Reward args)
    {
        UIManager.Instance.losePanel.SetActive(false);
        PlayerPrefs.SetInt("NextLevelNumberKey", nextLevelNumber + 1);
        PlayerPrefs.DeleteAll(); ///
        UIManager.Instance.LoadScene(0);
    }

    public void SkipRewardAd()
    {
        if (this.rewardedSkipAd.IsLoaded())
        {
            this.rewardedSkipAd.Show();
        }
    }
}
