using UnityEngine;
using TMPro;

public class MainMenuDisplay : MonoBehaviour
{
    public TextMeshProUGUI totalCoinsText;

    private void Start()
    {
        // Load total coins from PlayerPrefs
        int totalCoins = PlayerPrefs.GetInt("TotalCoinsCollected", 0);
        totalCoinsText.text = "Total Coins: " + totalCoins;
    }
}
