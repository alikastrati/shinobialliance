using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathHandler : MonoBehaviour
{
    public GameObject deathPopup;  // The popup panel
    public Button continueButton;  // Button to continue playing (ads later)
    public Button mainMenuButton;  // Button to go back to the main menu

    private void Start()
    {
        // Make sure the death popup is hidden at the start
        deathPopup.SetActive(false);

        // Add listeners for the buttons
        continueButton.onClick.AddListener(ContinuePlaying);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // Call this method when the player dies
    public void ShowDeathPopup()
    {
        deathPopup.SetActive(true);  // Show the death popup
    }

    // Continue playing logic (we'll implement ads here later)
    private void ContinuePlaying()
    {
        // Reset the game or show an ad before continuing
        deathPopup.SetActive(false);  // Hide the death popup
        // Additional logic for resuming the game (ads, resetting, etc.)
    }

    // Go to the Main Menu
    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Load the MainMenu scene
    }
}
