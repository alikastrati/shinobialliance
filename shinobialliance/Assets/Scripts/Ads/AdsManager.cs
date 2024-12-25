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

    // ---------------------
    // BANNER CODE START
    // ---------------------
    private string bannerPlacementId;  
    // ---------------------
    // BANNER CODE END
    // ---------------------

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
        interstitialPlacementId = "Interstitial_Android"; // Interstitial
        rewardedPlacementId = "Rewarded_Android";         // Rewarded
        // ---------------------
        // BANNER CODE START
        // ---------------------
        bannerPlacementId = "Banner_Android";             // Banner for Android
        // ---------------------
        // BANNER CODE END
#elif UNITY_IOS
        gameId = iOSGameId;
        interstitialPlacementId = "Interstitial_iOS";
        rewardedPlacementId = "RewardedIOS";
        // ---------------------
        // BANNER CODE START
        // ---------------------
        bannerPlacementId = "Banner_iOS";                 // Banner for iOS
        // ---------------------
        // BANNER CODE END
#else
        gameId = androidGameId; // fallback
        interstitialPlacementId = "Interstitial_Android";
        rewardedPlacementId = "Rewarded_Android";
        // ---------------------
        // BANNER CODE START
        // ---------------------
        bannerPlacementId = "Banner_Android";             // Banner fallback
        // ---------------------
        // BANNER CODE END
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

        // ---------------------
        // BANNER CODE START
        // If you want to load/show a banner immediately
        // ---------------------
        LoadBannerAd();
        // ---------------------
        // BANNER CODE END
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
    // BANNER CODE START
    // =========================================
    public void LoadBannerAd()
    {
        Debug.Log("Loading Banner Ad...");
        // You can set the banner position before loading
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        // Set up callbacks
        var bannerLoadOptions = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(bannerPlacementId, bannerLoadOptions);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner Ad Loaded Successfully!");

        // Optionally, show banner immediately after loading
        ShowBannerAd();
    }

    private void OnBannerError(string message)
    {
        Debug.LogError($"Banner failed to load: {message}");
        // Could retry here if needed...
    }

    public void ShowBannerAd()
    {
        Debug.Log("Showing Banner Ad...");
        var bannerOptions = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            showCallback = OnBannerShown,
            hideCallback = OnBannerHidden
        };

        Advertisement.Banner.Show(bannerPlacementId, bannerOptions);
    }

    public void HideBannerAd()
    {
        Debug.Log("Hiding Banner Ad...");
        Advertisement.Banner.Hide();
    }

    private void OnBannerClicked()
    {
        Debug.Log("Banner Clicked!");
    }

    private void OnBannerShown()
    {
        Debug.Log("Banner is now visible!");
    }

    private void OnBannerHidden()
    {
        Debug.Log("Banner is hidden!");
    }
    // =========================================
    // BANNER CODE END
    // =========================================

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
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddCoins(20);
            Debug.Log("Rewarded Ad Completed! +20 coins added to the player!");

            // Immediately refresh the display
            MainMenuDisplay display = FindObjectOfType<MainMenuDisplay>();
            if (display != null)
            {
                display.UpdateCoinsDisplay();
            }
        }
        else
        {
            Debug.LogWarning("ScoreManager instance is null. Cannot add coins.");
        }
    }
}
