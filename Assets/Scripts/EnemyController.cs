using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float maxSpeed = 8f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private int pathState;
    public List<Vector2> movePath = new List<Vector2>();

    void Start(){
        pathState = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (movePath.Count > 0) {
            MoveToNextPoint();
        }
    }

    void FixedUpdate() {
        if (pathState < movePath.Count) {
            Vector2 targetPosition = movePath[pathState];
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if (direction.x > 0) {
                spriteRenderer.flipX = true;
            } else if (direction.x < 0) {
                spriteRenderer.flipX = false;
            }

            animator.SetBool("move", true);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f) {
                pathState++;
                if (pathState < movePath.Count) {
                    MoveToNextPoint();
                } else {
                    animator.SetBool("move", false);
                }
            }
        } else {
            animator.SetBool("move", false);
        }
    }

    void MoveToNextPoint() {
        if (pathState % 5 == 0)
        {
            moveSpeed++;
        }
    }
}
