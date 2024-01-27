using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Hız değerini birim/saniye olarak ayarla

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;
    public bool gainCoin=false;
    int coins=0;
    AudioSource audioSource;
    [SerializeField] Transform groom;

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
    private void OnTriggerEnter2D(Collider2D collision) // Coin'ler toplandıkça yok edicek ve sayıcak
    {
        if (!collision.gameObject.CompareTag("Groom"))
        {
            collision.gameObject.SetActive(false);
            gainCoin = true;
            coins++;
            audioSource.Play();
        }
      
      
    }



    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>().normalized; // Normalizasyon, sabit hızı korumak için
    }
}
