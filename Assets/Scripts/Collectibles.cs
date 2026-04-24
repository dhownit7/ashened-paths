using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore(value);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Gentle floating animation
        transform.position += new Vector3(0, Mathf.Sin(Time.time * 2f) * 0.001f, 0);
    }
}