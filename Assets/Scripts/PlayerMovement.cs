using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats stats;
    [SerializeField] private Vector2 moveInput;

    [SerializeField] public float speed = 5f;

    [SerializeField] [Range(0, 1)] float lerpConstant = 0.1f;

    [SerializeField] public float jumpForce = 5f;

    private Collider2D playerCollider;

    private RaycastHit2D groundCast;

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
    }

    private void FixedUpdate()
    {
        playerBehaviour();
        collisionBehaviour();
        HandleJump();

    }

    private void GatherInput()
    { 
        frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            JumpHeld = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
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
        PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, jumpForce);

        float jumpVelocity = Mathf.Sqrt(2f * stats.jumpHeight * Mathf.Abs(Physics2D.gravity.y * stats.baseGravity));

        PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, jumpVelocity);

    }
    void collisionBehaviour()
    {
        float groundDistance = 0.5f;
        if (playerCollider == null) return;

        groundCast = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.down, groundDistance);

        isGrounded = groundCast.collider != null;
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
