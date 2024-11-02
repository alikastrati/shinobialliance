using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public Transform player;
    public GameObject coinPrefab;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ensure your Player has the "Player" tag
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.health--; // Reduce player health
                playerController.UpdateHealthBar(); // Update the health bar display

                if (playerController.health <= 0)
                {
                    // Handle player death
                    Debug.Log("Player has died!");
                    Destroy(collision.gameObject); // Destroy the player GameObject
                }
                // Do not destroy the enemy here; let it continue to exist.
            }
        }
    }


}
