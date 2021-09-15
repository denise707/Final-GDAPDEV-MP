using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager Instance;

    public EventHandler<AdFinishEventArgs> OnAdDone;

    public string GameID
    {
        get
        {
#if UNITY_ANDROID
            return "4293503";
#elif UNITY_IOS
            return "4293502";
#endif
        }
    }

    public const string SampleRewarded = "RewardedAd";
    public const string SampleInterstitial= "InterstitialAd";
    public const string SampleBanner = "BannerAd";

    private void Start()
    {
        Advertisement.AddListener(this);
    }

    private void Awake()
    {
        //Import Advertisement
        Advertisement.Initialize(GameID, true);
        //True for MP, False for publishing
    }

    //Interstitial Function
    public void ShowInterstitialAd()
    {
        if (Advertisement.IsReady(SampleInterstitial))
        {
            Advertisement.Show(SampleInterstitial);
        }
        else
        {
            Debug.Log("No Ads");
        }
    }

    //Banner Function
    IEnumerator ShowBannerCoroutine()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(SampleBanner);
    }

    public void ShowBanner()
    {
        StartCoroutine(ShowBannerCoroutine());
    }

    public void HideBanner()
    {
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log($"Loading done {placementId}");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log($"Ad error: {message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"Ad shown: {placementId}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(OnAdDone!= null)
        {
            AdFinishEventArgs args = new AdFinishEventArgs(placementId, showResult);
            OnAdDone(this, args);
        }
    }

    public void ShowRewawrdedAd()
    {
        if (Advertisement.IsReady(SampleRewarded))
        {
            Advertisement.Show(SampleRewarded);
        }
        else
        {
            Debug.Log("No Ads");
        }
    }
}
