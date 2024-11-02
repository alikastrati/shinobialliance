using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI coinsCollectedText; // Coins collected in current session
    public TextMeshProUGUI totalCoinsText; // Total coins collected across all games

    private int coinsCollected = 0; // Coins in current session
    private int totalCoinsCollected = 0; // Total coins saved across sessions

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

        LoadTotalCoins(); // Load total coins at start
    }

    private void StartNewGame()
    {
        coinsCollected = 0; // Reset session coins for new game
        UpdateCoinUI();
    }

    public void AddCoin()
    {
        coinsCollected++; // Increase session coins
        totalCoinsCollected++; // Increase total coins
        UpdateCoinUI();
        SaveTotalCoins(); // Save total after each coin is added
    }

    private void UpdateCoinUI()
    {
        coinsCollectedText.text = "Coins: " + coinsCollected; // Display session coins
        totalCoinsText.text = "Total Coins: " + totalCoinsCollected; // Display total coins on main menu
    }

    private void SaveTotalCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoinsCollected);
        PlayerPrefs.Save();
    }

    private void LoadTotalCoins()
    {
        totalCoinsCollected = PlayerPrefs.GetInt("TotalCoins", 0);
    }
}
