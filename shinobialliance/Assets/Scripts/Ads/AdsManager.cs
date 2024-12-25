using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour,
    IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidGameId = "5759620";
    [SerializeField] private string iOSGameId = "5759621";
    [SerializeField] private bool testMode = true;

    private string gameId;

    // Interstitial
    private string interstitialPlacementId;

    // Rewarded
    private string rewardedPlacementId;

    // Singleton-ish pattern (optional)
    private static AdsManager instance;

    void Awake()
    {
        // Ensure only one instance of AdsManager exists and persists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_ANDROID
        gameId = androidGameId;
        interstitialPlacementId = "Interstitial_Android"; // Set your Interstitial placement ID
        rewardedPlacementId = "Rewarded_Android";         // Set your Rewarded placement ID
#elif UNITY_IOS
        gameId = iOSGameId;
        interstitialPlacementId = "Interstitial_iOS";
        rewardedPlacementId = "RewardedIOS";
#else
        gameId = androidGameId; // fallback
        interstitialPlacementId = "Interstitial_Android";
        rewardedPlacementId = "Rewarded_Android";
#endif

        Advertisement.Initialize(gameId, testMode, this);
    }

    // =========================================
    // Initialization Callbacks
    // =========================================
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete");

        // Load both ad types after initialization
        LoadInterstitial();
        LoadRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads Initialization Failed: {error} - {message}");
    }

    // =========================================
    // Load Interstitial
    // =========================================
    public void LoadInterstitial()
    {
        Debug.Log("Loading Interstitial Ad...");
        Advertisement.Load(interstitialPlacementId, this);
    }

    // =========================================
    // Load Rewarded Ad
    // =========================================
    public void LoadRewardedAd()
    {
        Debug.Log("Loading Rewarded Ad...");
        Advertisement.Load(rewardedPlacementId, this);
    }

    // =========================================
    // Show Interstitial
    // =========================================
    public void ShowInterstitial()
    {
        Debug.Log("Showing Interstitial Ad...");
        Advertisement.Show(interstitialPlacementId, this);
    }

    // =========================================
    // Show Rewarded
    // =========================================
    public void ShowRewardedAd()
    {
        Debug.Log("Showing Rewarded Ad...");
        Advertisement.Show(rewardedPlacementId, this);
    }

    // =========================================
    // IUnityAdsLoadListener Callbacks
    // =========================================
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad Loaded Successfully: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to Load Ad: {placementId} - {error} - {message}");
    }

    // =========================================
    // IUnityAdsShowListener Callbacks
    // =========================================
    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Ad Start: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Ad Clicked: {placementId}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Failed to Show Ad: {placementId} - {error} - {message}");

        // Attempt to reload after failure
        if (placementId == interstitialPlacementId)
        {
            LoadInterstitial();
        }
        else if (placementId == rewardedPlacementId)
        {
            LoadRewardedAd();
        }
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad Complete: {placementId} - {showCompletionState}");

        // If the user finished a Rewarded ad, grant a reward
        if (placementId == rewardedPlacementId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            GrantReward();
        }

        // Reload whichever ad was just shown
        if (placementId == interstitialPlacementId)
        {
            LoadInterstitial();
        }
        else if (placementId == rewardedPlacementId)
        {
            LoadRewardedAd();
        }
    }

    // =========================================
    // Reward Logic
    // =========================================
    private void GrantReward()
    {
        // Here is where you credit the user’s account, give items, or whatever your “reward” is.
        Debug.Log("Rewarded Ad Completed! Grant the reward to the player here.");
    }
}
