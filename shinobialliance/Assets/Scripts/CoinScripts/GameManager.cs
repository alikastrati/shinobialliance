using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI coinsCollectedText; // This should be of type TextMeshProUGUI
    private int coinsCollected = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    public void AddCoin()
    {
        coinsCollected++; // Increase the coin count
        UpdateCoinUI(); // Update the UI text
    }

    private void UpdateCoinUI()
    {
        coinsCollectedText.text = "Coins: " + coinsCollected; // Update the text to show the current coins collected
    }
}
