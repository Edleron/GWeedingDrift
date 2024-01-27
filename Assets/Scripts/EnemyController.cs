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
    [SerializeField] private Transform Bride;
    private bool groomTriggered ;

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
    private void Update()
    {
        if (groomTriggered && Input.GetKey(KeyCode.E))
            grab();
        else                            
            groomTriggered = false;
            
    }

    void FixedUpdate() {
            if (pathState < movePath.Count && !groomTriggered)
            {
                Vector2 targetPosition = movePath[pathState];
                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

                rb.MovePosition(newPosition);

                if (direction.x > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (direction.x < 0)
                {
                    spriteRenderer.flipX = false;
                }

                animator.SetBool("move", true);

                if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
                {
                    pathState++;
                    if (pathState < movePath.Count)
                    {
                        MoveToNextPoint();
                    }
                    else
                    {
                        animator.SetBool("move", false);
                    }
                }
            }
            else
            {
                animator.SetBool("move", false);
            }
        
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Bride"))
        {
            groomTriggered = true;  
        }


    }
   
    private void grab()
    {
        transform.position = new Vector3(Bride.position.x - 1, Bride.position.y, transform.position.z);
    }

    void MoveToNextPoint() {
       
        if (pathState % 5 == 0)
        {
            moveSpeed++;
        }
    }
}
