using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 3;
    private int currentHealth;
    private bool isInvincible = false;

    public float moveSpeed = 7f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Animator animator;
    public Vector2 spawnPoint;

    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawnPoint = transform.position;
        currentHealth = maxHealth;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();

        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer))
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    public void Respawn()
    {
        transform.position = spawnPoint;
        rb.velocity = Vector2.zero;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public void TakeDamage()
    {
        if (isInvincible) return;

        currentHealth--;
        GameManager.instance.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
            Respawn();
            currentHealth = maxHealth;
            GameManager.instance.UpdateHealth(currentHealth);
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
            Respawn();
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        // Flash the player
        for (int i = 0; i < 6; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        isInvincible = false;
    }
}