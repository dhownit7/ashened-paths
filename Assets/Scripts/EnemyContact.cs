using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if player is above the enemy (stomping)
            if (collision.contacts[0].normal.y < -0.5f)
            {
                // Player jumped on enemy - destroy enemy
                Destroy(gameObject);

                // Bounce player up
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                playerRb.velocity = new Vector2(playerRb.velocity.x, 10f);
            }
            else
            {
                // Enemy touched player from side - kill player
                RespawnPlayer(collision.gameObject);
            }
        }
    }

    void RespawnPlayer(GameObject player)
    {
        // For now just reset position
        player.transform.position = new Vector2(-8, -3);
    }
}