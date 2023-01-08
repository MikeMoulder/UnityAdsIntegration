using UnityEngine;
using UnityEngine.Advertisements;
 
public class RewardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    string _androidAdUnitId = "Rewarded_Android";
    string _iOSAdUnitId = "Rewarded_iOS";
    public bool testMode;
    string _adUnitId = null; // This will remain null for unsupported platforms
    
    void Awake()
    {   
        _androidAdUnitId = testMode ? "Rewarded_Android" : "TutorialRewardedVideo_Android";
        _iOSAdUnitId = testMode ? "Rewarded_iOS" : "TutorialRewardedVideo_iOS";

        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
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
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        adLoaded =  true;
        adLoading = false;
        adFailedToLoad = false;
    }
 
    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
        adLoaded = false;
        adLoading = false;
        adShowing = true;
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed, You Can Reward Player");
            // Grant a reward.
            print("You've earned a reward");
            
            adShown = true;
            adFailedToShow = false;
            adLoading = false;
            adFailedToLoad = false;
            adShowing = false;
            adLoaded = false;

            // Load another ad:
            LoadAd();
        }
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        adLoading = false;
        adFailedToLoad = true;
        adShowing = false;
        adLoaded = false;
        print("Ads Failed To Load, Check Internet");
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        adFailedToShow = true;
        adLoading = false;
        adFailedToLoad = false;
        adShowing = false;
        adLoaded = false;
        print("Ads Failed To Show, Check Internet");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
}
