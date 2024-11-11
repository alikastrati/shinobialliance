using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject spawnPoint; // Assign the spawn point GameObject

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        // Get the selected character from CharacterManager
        GameObject selectedCharacter = CharacterManager.instance.GetSelectedCharacter();

        if (selectedCharacter != null)
        {
            // Spawn the selected character at the spawn point
            Instantiate(selectedCharacter, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            // Display a warning if no character has been selected
            Debug.LogWarning("No character selected or character not purchased.");

            // Optional: Instantiate a default character here if desired
         // Example: Instantiate(defaultCharacterPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}
