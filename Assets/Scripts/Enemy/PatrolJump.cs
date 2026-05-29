using System.Collections;
using UnityEngine;

public class PatorlJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Grounded")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask whatIsGround;

    [Range(0f, 1f)]
    [SerializeField] private float groundRadius = 0.2f;

    private bool isGrounded = false;

    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float patrolPointWaitTime = 1f;
    [SerializeField] private Transform targetPlayerTransform;

    [Header("Jump")]
    [SerializeField] private float targetJumpDistance = 1.5f;
    [SerializeField] private float playerDetectionRange = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpInitializeTime = 0.4f;
    [SerializeField] private float jumpBackIniTime = 0.6f;

    private bool inRange;
    private bool hasExecuted = false;
    private bool isWaiting = false;

    private int currentPatrolPointIndex = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (targetPlayerTransform == null || patrolPoints.Length == 0)
            return;

        inRange =
            Vector2.Distance(transform.position, targetPlayerTransform.position)
            <= playerDetectionRange
            && !hasExecuted;

        if (!inRange)
        {
            Patrol();
        }
        else
        {
            if (isGrounded && !hasExecuted)
            {
                hasExecuted = true;
                StartCoroutine(JumpToTarget(targetPlayerTransform));
            }
        }
    }

    IEnumerator JumpToPatrolPoint()
    {
        hasExecuted = true;

        yield return new WaitForSeconds(jumpInitializeTime);

        Vector2 targetPos = patrolPoints[currentPatrolPointIndex].position;

        float distanceX =
            targetPos.x - transform.position.x;

        if (distanceX > 0)
            transform.localScale = Vector3.one;
        else
            transform.localScale = new Vector3(-1, 1, 1);

        rb.velocity = Vector2.zero;

        rb.AddForce(
            new Vector2(distanceX * 1.5f, jumpForce),
            ForceMode2D.Impulse
        );

        yield return new WaitForSeconds(jumpBackIniTime);

        hasExecuted = false;
    }
    IEnumerator JumpToTarget(Transform target)
    {
        yield return new WaitForSeconds(jumpInitializeTime);

        if (!isGrounded)
        {
            hasExecuted = false;
            yield break;
        }

        Vector2 direction =
            (target.position - transform.position).normalized;

        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(
            direction.x * targetJumpDistance,
            jumpForce
        );

        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        yield return new WaitForSeconds(jumpBackIniTime);

        hasExecuted = false;
    }

    void Patrol()
    {
        if (hasExecuted || isWaiting)
            return;

        Vector2 targetPosition =
            patrolPoints[currentPatrolPointIndex].position;

        float distance =
            Vector2.Distance(transform.position, targetPosition);

        if (distance < 1f)
        {
            StartCoroutine(WaitAtPoint());
            return;
        }

        if (isGrounded)
        {
            StartCoroutine(JumpToPatrolPoint());
        }
    }

    IEnumerator WaitAtPoint()
    {
        isWaiting = true;

        yield return new WaitForSeconds(patrolPointWaitTime);

        currentPatrolPointIndex =
            (currentPatrolPointIndex + 1) % patrolPoints.Length;

        isWaiting = false;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheckPoint.position,
            groundRadius,
            whatIsGround
        );
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheckPoint.position, groundRadius);
    }
}