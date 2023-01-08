using UnityEngine;
using UnityEngine.Advertisements;
using PetrushevskiApps.Utilities;
public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    string _androidGameId = "5108979";
    string _iOSGameId = "5108978";
    [SerializeField] bool _testMode = true;
    private string _gameId;

    private bool initialized;
    [HideInInspector]
    public RewardedAd rewardedAd;
    [HideInInspector]
    public InterstitialAd interstitialAd;
    [SerializeField]
    string connectionStatus = "Disconnected";
    public ConnectivityManager _connectivityManager;

    void Awake()
    {
        QuickStart();
    }

    void Update()
    {
        isInternet = _connectivityManager.IsConnected;
        if (isInternet)
        {
            connectionStatus = "Connected";
        }else
        {
            connectionStatus = "Disconnected";
        }

        initialized = Advertisement.isInitialized;

        if (!initialized && isInternet)
        {
            InitializeAds();
        }

        LoadInterStitialAds();
        LoadRewardedAds();
        ShowAdsPerodically();
        InitializationError();
    }
    void QuickStart()
    {
        _connectivityManager = GameObject.FindObjectOfType<ConnectivityManager>();
        rewardedAd = GetComponent<RewardedAd>();
        interstitialAd = GetComponent<InterstitialAd>();
    }
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    void InitializationError()
    {
        if (initializationFailed && !initialized)
        {
            InitializeAds();
        }
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        initialized = true;
        initializationFailed = false;
    }
    
    bool initializationFailed;
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        initialized = false;
        initializationFailed = true;
        InitializeAds();
    }

    [HideInInspector]
    public bool isInternet;
    public void LoadInterStitialAds()
    {
        if (isInternet && initialized && !interstitialAd.adLoaded && !interstitialAd.adLoading || interstitialAd.adFailedToLoad && isInternet && initialized)
        {
            interstitialAd.LoadAd();
        }
    }

    public void LoadRewardedAds()
    {
        if (isInternet && initialized && !rewardedAd.adLoaded && !rewardedAd.adLoading || rewardedAd.adFailedToLoad && isInternet && initialized)
        {
            rewardedAd.LoadAd();
        }
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd.adLoaded && !interstitialAd.adShowing && isInternet)
        {
            interstitialAd.ShowAd();
        }else
        {
            if (!interstitialAd.adLoaded)
            {
                print("Couldn't Show An Ad Becuase It Was Not Loaded!");
            }else if(interstitialAd.adShowing)
            {
                print("Couldn't Show An Ad Becuase One Is Currently Showing");
            }else if(!isInternet)
            {
                print("Couldn't Show An Ad Becuase There's No Internet Connection!");
            }
        }
    }

    public void ShowVideoAd()
    {
        if (rewardedAd.adLoaded && !rewardedAd.adShowing && isInternet)
        {
            rewardedAd.ShowAd();
        }else
        {
            if (!rewardedAd.adLoaded)
            {
                print("Couldn't Show An Ad Becuase It Was Not Loaded!");
            }else if(rewardedAd.adShowing)
            {
                print("Couldn't Show An Ad Becuase One Is Currently Showing");
            }else if(!isInternet)
            {
                print("Couldn't Show An Ad Becuase There's No Internet Connection!");
            }
        }
    }

    public bool showAdsPerodically = false;
    public float showAdEvery = 0;
    float showAdTime = 0;

    void ShowAdsPerodically()
    {
        if (showAdsPerodically)
        {
            showAdTime += Time.deltaTime;

            if (showAdTime >= showAdEvery)
            {
                if (interstitialAd.adLoaded && !interstitialAd.adShowing && isInternet)
                {
                    interstitialAd.ShowAd();
                    showAdTime = 0;
                    print("Showing Ads");
                }else
                {
                    if (isInternet)
                    {
                        print("No Network");
                    }

                    showAdTime = 0;
                }
            }
        }
    }
}
