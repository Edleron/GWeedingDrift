using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float maxSpeed = 8f;
    public Animator circleAnim;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Transform Bride;
    private bool groomTriggered ;

    private int pathState;
    private List<Vector2> movePath = new List<Vector2>();

    
    private void OnEnable()
    {
        EventManager.onDetectRestart += onRestartPlayer;
    }

    private void OnDisable()
    {
        EventManager.onDetectRestart -= onRestartPlayer;
    }

    void Start(){
        pathState = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();        

        PathGenerate();
    }

    private void PathGenerate()
    {
        int index = 0;
        int gear = 0;
        int borderX = 4;
        int borderY = 2;

       while(gear < 3) 
       {
            for (var i = 0; i < 15; i++)
            {
                index++;
                Vector2 position = new Vector2(Random.Range(borderX, -borderX), Random.Range(borderY, -borderY));
                movePath.Add(position);                
            }
            gear++;
            borderX = borderX + 2;
            borderY = borderY + 1;
       }

       // Son path -> karakter çıkar ve level resetlenir !
       movePath.Add(new Vector2(0, -8.65f));
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

                // Restart Game
                if (pathState == movePath.Count)
                {
                    
                    LevelManager.Instance.puanValue--;
                    circleAnim.SetBool("state", false);
                    EventManager.Fire_onDetectRestart();
                    EventManager.Fire_onDetectPuan(LevelManager.Instance.puanValue);
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
            moveSpeed = moveSpeed + 0.25f;
        }

        if (pathState > 30)
        {
            circleAnim.SetBool("state", true);
            int teaser = movePath.Count - pathState;
            EventManager.Fire_onDetectTimer(teaser);
        }
    }

    private void onRestartPlayer()
    {
        pathState = 0;
        moveSpeed = 2;
        this.transform.position = new Vector3(0, 0, 0);
    }
}
