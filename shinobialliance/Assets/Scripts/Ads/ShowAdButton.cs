using UnityEngine;

public class ShowAdButton : MonoBehaviour
{
    // Shows an Interstitial
    public void OnShowInterstitialButtonClicked()
    {
        AdsManager adsManager = FindObjectOfType<AdsManager>();

        if (adsManager != null)
        {
            adsManager.ShowInterstitial();
        }
        else
        {
            Debug.LogWarning("AdsManager not found in the scene!");
        }
    }

    // Shows a Rewarded Ad
    public void OnShowRewardedButtonClicked()
    {
        AdsManager adsManager = FindObjectOfType<AdsManager>();

        if (adsManager != null)
        {
            adsManager.ShowRewardedAd();
        }
        else
        {
            Debug.LogWarning("AdsManager not found in the scene!");
        }
    }
}
