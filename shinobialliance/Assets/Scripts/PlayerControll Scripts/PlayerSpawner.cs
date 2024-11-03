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
        // Instantiate the selected character at the spawn point
        GameObject selectedCharacter = CharacterManager.instance.GetSelectedCharacter();
        Instantiate(selectedCharacter, spawnPoint.transform.position, Quaternion.identity);
    }
}
