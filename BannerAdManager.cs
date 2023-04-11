using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdManager))]
[RequireComponent(typeof(BannerAd))]
public class BannerAdManager : MonoBehaviour
{
    AdManager _adManager;
    [HideInInspector]
    public BannerAd bannerAd;
    // Start is called before the first frame update
    void Start()
    {
        _adManager = GetComponent<AdManager>();
        bannerAd = GetComponent<BannerAd>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadBannerAd();
        ShowBannerAd();
        BannerAdRefresh();
    }

    public void LoadBannerAd()
    {
        if (_adManager.isInternet && _adManager.initialized && !bannerAd.adLoaded && !bannerAd.adLoading && !bannerAd.adShowing || bannerAd.adFailedToLoad && _adManager.isInternet && _adManager.initialized)
        {
            bannerAd.LoadBanner();
        }
    }

    public void ShowBannerAd()
    {
        if (bannerAd.adLoaded && !bannerAd.adShowing && _adManager.isInternet && !coolingDown)
        {
            bannerAd.ShowBannerAd();
        }
    }

    [Tooltip("Should I Automatically Fetch New Banner Ad?")]
    public bool _autoRefresh = true;
    [Range(60, 300)] //In Seconds!
    [Tooltip("How Long Should The Current Banner Stay Before Fetching A New One?")]
    public float refreshRate = 120f;
    [Range(15, 60f)]
    [Tooltip("How Long Should I Wait Before Showing The Newly Requested Banner Ad?")]
    public float coolDown = 20f;
    bool coolingDown;
    float durationSpent;
    float coolDownDuration;
    public void BannerAdRefresh()
    {
        if (_autoRefresh)
        {
            if (bannerAd.adShowing)
            {
                durationSpent += Time.unscaledDeltaTime;
                if (durationSpent >= refreshRate)
                {
                    coolingDown = true;
                    bannerAd.HideBannerAd();
                }
            }

            if (coolingDown)
            {
                coolDownDuration += Time.unscaledDeltaTime;
                if (coolDownDuration >= coolDown)
                {
                    coolingDown = false;
                    coolDownDuration = 0f;
                    durationSpent = 0f;

                    //Give Us A New Banner!
                }
            }
        }
    }  
}
