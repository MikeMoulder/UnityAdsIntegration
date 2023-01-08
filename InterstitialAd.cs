using UnityEngine;
using UnityEngine.Advertisements;
 
public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    string _androidAdUnitId = "Interstitial_Android";
    string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    public bool testMode = false;

    void Awake()
    {
        _androidAdUnitId = testMode ? "Interstitial_Android" : "UnityTutorialInterstitialAds_Android";
        _iOsAdUnitId = testMode ? "Interstitial_iOS" : "UnityTutorialInterstitial_iOS";

        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }
    

    [HideInInspector]
    public bool adLoaded = false;
    [HideInInspector]
    public bool adShowing = false;
    [HideInInspector]
    public bool adFailedToLoad = false;
    [HideInInspector]
    public bool adFailedToShow = false;
    [HideInInspector]
    public bool adShown = false;
    [HideInInspector]
    public bool adLoading = false;

    
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);

        adLoading = true;
        adLoaded = false;
        adShowing = false;
        adFailedToLoad = false;
        adFailedToShow = false;
    }
 
    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);

        adLoaded = false;
        adLoading = false;
        adShowing = true;
    }
 
    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.

        adLoaded = true;
        adLoading = false;
        adFailedToLoad = false;
    }
 
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        adFailedToLoad = true;
        adLoaded = false;
        adLoading = false;
        adShowing = false;
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        adFailedToLoad = false;
        adLoaded = false;
        adLoading = false;
        adShowing = false;
        adFailedToShow = true;
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}
