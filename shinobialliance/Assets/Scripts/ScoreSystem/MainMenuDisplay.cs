using UnityEngine;
using TMPro;

public class MainMenuDisplay : MonoBehaviour
{
    public TextMeshProUGUI totalCoinsText;

    void Start()
    {
        UpdateCoinsDisplay();
    }

    public void UpdateCoinsDisplay()
    {
        if (ScoreManager.instance != null)
        {
            // Grab the up-to-date coin count from ScoreManager
            int currentCoins = ScoreManager.instance.totalCoinsCollected;
            totalCoinsText.text = ": " + currentCoins;
        }
        else
        {
            // Fallback if ScoreManager doesn't exist yet
            totalCoinsText.text = ": 0";
        }
    }
}
