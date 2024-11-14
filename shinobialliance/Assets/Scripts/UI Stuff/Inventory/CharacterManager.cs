using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance; // Singleton instance
    public GameObject[] playerPrefabs; // Assign player prefabs in Inspector
    public int[] characterPrices; // Set the coin price for each character in Inspector
    public bool[] isPurchased; // Track if each character is purchased
    public Image characterDisplay; // UI Image to display character
    public TextMeshProUGUI coinRequirementText; // TextMeshProUGUI to display the coin requirement
    private int currentIndex = 0; // Start with the first character
    public int selectedCharacterIndex = -1; // -1 indicates no selection yet

    private void Awake()
    {
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

    private void OnEnable()
    {
        // Reload purchase data when the object is enabled
        LoadPurchaseData();

        isPurchased[0] = true;

        // Load the last selected character or default to the first one
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);

        if (!isPurchased[selectedCharacterIndex])
        {
            selectedCharacterIndex = 0; // Default to the first character if not purchased
        }

        currentIndex = selectedCharacterIndex; // Update current index to reflect the last selection
        UpdateCharacterDisplay();
    }

    private void Start()
    {
        // Only call this once when the scene is first loaded, but ensure display is updated after OnEnable
        UpdateCharacterDisplay();
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % playerPrefabs.Length;
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + playerPrefabs.Length) % playerPrefabs.Length;
        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        if (playerPrefabs.Length > 0)
        {
            characterDisplay.sprite = playerPrefabs[currentIndex].GetComponent<SpriteRenderer>().sprite;

            if (isPurchased[currentIndex])
            {
                coinRequirementText.text = "Owned";
            }
            else
            {
                coinRequirementText.text = $"Price: {characterPrices[currentIndex]} Coins";
            }
        }
    }

    public void PurchaseCharacter()
    {
        int price = characterPrices[currentIndex];

        if (!isPurchased[currentIndex] && ScoreManager.instance.totalCoinsCollected >= price)
        {
            ScoreManager.instance.DeductCoins(price); // Deduct coins from total
            isPurchased[currentIndex] = true; // Mark character as purchased
            SavePurchaseData();
            UpdateCharacterDisplay();
        }
    }

    private void LoadPurchaseData()
    {
        for (int i = 1; i < playerPrefabs.Length; i++)
        {
            isPurchased[i] = PlayerPrefs.GetInt($"Character_{i}_Purchased", 0) == 1;
        }
    }

    private void SavePurchaseData()
    {
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            PlayerPrefs.SetInt($"Character_{i}_Purchased", isPurchased[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public GameObject GetSelectedCharacter()
    {
        // Return the selected character if it has been set, otherwise return the default character
        return (selectedCharacterIndex >= 0 && isPurchased[selectedCharacterIndex])
            ? playerPrefabs[selectedCharacterIndex]
            : playerPrefabs[0];
    }

    public void SelectCharacter()
    {
        if (isPurchased[currentIndex])
        {
            selectedCharacterIndex = currentIndex; // Mark the character as selected
            PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacterIndex); // Save selection
            PlayerPrefs.Save();
            Debug.Log($"Character {currentIndex} selected.");
        }
        else
        {
            Debug.Log("Character not purchased.");
        }
    }
}
