using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddCoin(); // Update coin count in ScoreManager
            Destroy(gameObject); // Remove coin after collection
        }
    }
}
