using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggered by: {collision.gameObject.name}"); // Add this line to see what the player collides with
        if (collision.CompareTag("Coin"))
        {
            ScoreManager.instance.AddCoin();
            Destroy(collision.gameObject);
            Debug.Log("Coin collected! Total coins: " + ScoreManager.instance.coinsCollected);
        }
    }

}
