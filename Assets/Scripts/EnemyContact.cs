using UnityEngine;

public class EnemyContact : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y < -0.5f)
            {
                Destroy(gameObject);
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                playerRb.velocity = new Vector2(playerRb.velocity.x, 10f);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage();
            }
        }
    }

    void RespawnPlayer(GameObject player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.Respawn();
    }
}