using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class BannerAd : MonoBehaviour
{
    public BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
 
    protected string _androidAdUnitId = "Banner_Android";
    protected string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.
    
    public bool testMode = true;

    [HideInInspector]
    public bool adLoaded = false;
    [HideInInspector]
    public bool adShowing = false;
    [HideInInspector]
    public bool adLoading = false;
    [HideInInspector]
    public bool adFailedToLoad;
    void Start()
    {
        _androidAdUnitId = testMode ? "Banner_Android" : "MyBannerAd_Android";
        _iOSAdUnitId = testMode ? "Banner_iOS" : "MyBannerAd_iOS";

        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);
    }
 
 
    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
 
        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
        adLoading = true;
        adShowing = false;
        adLoaded = false;
        adFailedToLoad = false;
    }
 
    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded"); 
        adLoading = false;
        adShowing = false;
        adLoaded = true;
        adFailedToLoad = false;
    }
 
    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
        adLoading = false;
        adShowing = false;
        adLoaded = false;
        adFailedToLoad = true;
    }
 
    // Implement a method to call when the Show Banner button is clicked:
    public void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
 
        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);

        adLoading = false;
        adShowing = true;
        adLoaded = false;
        adFailedToLoad = false;
    }
 
    // Implement a method to call when the Hide Banner button is clicked:
    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
        adLoading = false;
        adShowing = false;
        adLoaded = false;
        adFailedToLoad = false;
    }
 
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}
