using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats stats;
    [SerializeField] private Vector2 moveInput;

    [SerializeField] public float speed = 5f;

    [SerializeField] [Range(0, 1)] float lerpConstant = 0.1f;

    [SerializeField] public float jumpForce = 5f;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float groundCheckDistance = 0.1f;

    private Collider2D playerCollider;


    private bool isGrounded;    

    private FrameInput frameInput;
    public Rigidbody2D PlayerRb { get; private set; } 

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }
    void Update()
    {
        GatherInput();
        collisionBehaviour();
    }

    private void FixedUpdate()
    {
        playerBehaviour();
        HandleJump();

    }

    private void GatherInput()
    { 
        frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"),0)
        };
    }

    private void HandleJump()
    {
        if (frameInput.JumpDown && isGrounded)
        {
            ExecuteJump();

        }
    }
    private void ExecuteJump()
    {
        float jumpVelocity = Mathf.Sqrt(2f * stats.jumpHeight * Mathf.Abs(Physics2D.gravity.y * stats.baseGravity));

        PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, jumpVelocity);

    }
    void collisionBehaviour()
    {
        if (playerCollider == null) return;

        RaycastHit2D  hit = Physics2D.BoxCast(playerCollider.bounds.center, 
                                              playerCollider.bounds.size, 
                                              0, 
                                              Vector2.down, 
                                              groundCheckDistance,
                                              groundLayer);
        isGrounded = hit.collider != null;

        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
    void playerBehaviour()
    {
        float horizontal = frameInput.Move.x;
        
        Vector2 targetVelocity = new Vector2(
            horizontal * speed,
            PlayerRb.velocity.y
        );

        PlayerRb.velocity = Vector2.Lerp(
            PlayerRb.velocity,
            targetVelocity,
            lerpConstant
            );

    }

}
