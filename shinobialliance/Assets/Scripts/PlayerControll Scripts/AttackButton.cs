using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnAttackButtonClicked);
    }

    private void OnAttackButtonClicked()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Attack(); // Call the attack method
            }
            else
            {
                Debug.LogWarning("No PlayerController found on the Player object.");
            }
        }
        else
        {
            Debug.LogWarning("No player found with the tag 'Player'.");
        }
    }
}
