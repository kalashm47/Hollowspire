using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolEnemie : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float waitTime = 1f;


    [Header("Chase")]
    [SerializeField] private float detectionRange = 4f;
    [SerializeField] private float chaseSpeed = 4f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    private int currentWaypoint = 0;
    private bool waiting;
    private bool chasing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        StartCoroutine(PatrolRoutine());
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        chasing = distance <= detectionRange;
    }

    void FixedUpdate()
    {
        if (chasing)
        {
            ChasePlayer();
        }
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!chasing && !waiting && waypoints.Length > 0)
            {
                Vector2 target = waypoints[currentWaypoint].position;

                while (
                    Vector2.Distance(rb.position, target) > 0.1f &&
                    !chasing
                )
                {
                    Vector2 nextPosition = Vector2.MoveTowards(
                        rb.position,
                        target,
                        patrolSpeed * Time.fixedDeltaTime
                    );

                    rb.MovePosition(nextPosition);

                    Flip(target.x - transform.position.x);

                    yield return new WaitForFixedUpdate();
                }

                if (!chasing)
                {
                    waiting = true;

                    yield return new WaitForSeconds(waitTime);

                    currentWaypoint++;
                    currentWaypoint %= waypoints.Length;

                    waiting = false;
                }
            }

            yield return null;
        }
    }

    void ChasePlayer()
    {
        if (player.position.y > transform.position.y + 0.5f)
            return; // attack 

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance < 1f)
            return; // attack

        Vector2 targetPosition = new Vector2(
            player.position.x,
            rb.position.y
        );

        rb.MovePosition(
            Vector2.MoveTowards(
                rb.position,
                targetPosition,
                chaseSpeed * Time.fixedDeltaTime
            )
        );
    }

    void Flip(float direction)
    {
        if (spriteRenderer == null)
            return;

        if (direction > 0.05f)
            spriteRenderer.flipX = false;
        else if (direction < -0.05f)
            spriteRenderer.flipX = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}