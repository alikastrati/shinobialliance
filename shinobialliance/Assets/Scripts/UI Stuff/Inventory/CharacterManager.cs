using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance; // Singleton instance
    public GameObject[] playerPrefabs; // Assign your player prefabs in the Inspector
    public Image characterDisplay; // Reference to the UI Image to display the character
    private int currentIndex = 0; // Start with the first character

    private void Awake()
    {
        // Ensure only one instance of CharacterManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    private void Start()
    {
        UpdateCharacterDisplay();
    }

    public void NextCharacter()
    {
        currentIndex++;
        if (currentIndex >= playerPrefabs.Length)
        {
            currentIndex = 0; // Loop back to the first character
        }
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = playerPrefabs.Length - 1; // Loop to the last character
        }
        UpdateCharacterDisplay();
    }

    private void UpdateCharacterDisplay()
    {
        characterDisplay.sprite = playerPrefabs[currentIndex].GetComponent<SpriteRenderer>().sprite;
    }

    public GameObject GetSelectedCharacter()
    {
        return playerPrefabs[currentIndex];
    }
}
