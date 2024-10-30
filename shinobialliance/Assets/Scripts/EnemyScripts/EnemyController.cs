using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public Transform player; 

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
            // Call a method to handle player death
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        // Logic to end the game or trigger a game over state
        Debug.Log("Player has died!");
        // Optionally: Destroy the player, show a game over screen, etc.
        // For example: Destroy(collision.gameObject);
    }

}
