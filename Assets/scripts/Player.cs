using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 5f;

    [Header("Start Position")]
    public Vector3 startPosition = Vector3.zero;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

 



    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Prevent stripping of Animator in WebGL
        animator.keepAnimatorStateOnDisable = true;
    }

    private void Start()
    {
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }

    public void ResetToIdle()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("XVelocity", 0f);
        spriteRenderer.flipX = false;
    }

    private void Update()
    {
        HandleInput();
        UpdateAnimation();
    }

    private void HandleInput()
    {
        // Space to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
     
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

     
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void UpdateAnimation()
    {
        // If your blend tree uses XVelocity, we can just feed in speed
        float speed = rb.linearVelocity.magnitude;
        animator.SetFloat("XVelocity", speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obs"))
        {
            FindAnyObjectByType<GameManager>().gameOver();
        }
        else if (other.CompareTag("Score"))
        {
            FindAnyObjectByType<GameManager>().IncreaseScore();
        }
    }
}
