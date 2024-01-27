using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput = Vector2.zero;
    private AudioSource audioSource;

    private int coins = 0;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        if(moveInput != Vector2.zero) {
            Vector2 currentPosition = rb.position;
            Vector2 newPosition = currentPosition + moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if(moveInput.x > 0) {
                spriteRenderer.flipX = true;
            } else if (moveInput.x < 0) {
                spriteRenderer.flipX = false;
            }
            animator.SetBool("move", true);
        } else {
            animator.SetBool("move", false);
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coins++;
            audioSource.Play();
            CoinGenerate.Instance.Counter = CoinGenerate.Instance.Counter - 1;
            collision.gameObject.SetActive(false);
            ObjectPooler.Instance.ReturnToPool("Coin", collision.gameObject);
        }
    }



    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>().normalized;
    }
}
