using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackInterval = 0.1f;

    [Header("References")]
    public GameObject hitbox;
    public Animator animator;

    [Header("Debug")]
    public bool isAttacking;

    // Dictionary containing all attack data
    public Dictionary<Direction, AttackData> attacks =
        new Dictionary<Direction, AttackData>();

    // Attack directions
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(AttackRoutine(Direction.Right));
        }
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [System.Serializable]
    public class AttackData
    {
        public string animation;
        public Vector2 hitboxOffset;
    }

    private void Awake()
    {
        // Register attack data
        attacks = new Dictionary<Direction, AttackData>()
        {
            {
                Direction.Up,
                new AttackData()
                {
                    animation = "attack",
                    hitboxOffset = new Vector2(0, 1)
                }
            },

            {
                Direction.Down,
                new AttackData()
                {
                    animation = "attack",
                    hitboxOffset = new Vector2(0, -1)
                }
            },

            {
                Direction.Left,
                new AttackData()
                {
                    animation = "attack",
                    hitboxOffset = new Vector2(-1, 0)
                }
            },

            {
                Direction.Right,
                new AttackData()
                {
                    animation = "attack",
                    hitboxOffset = new Vector2(1, 0)
                }
            }
        };

        // Safety check
        hitbox.SetActive(false);
    }

    // Public coroutine so other scripts can call it
    public IEnumerator AttackRoutine(Direction dir)
    {
        // Prevent attack spam
        if (isAttacking)
            yield break;

        isAttacking = true;

        // Get attack data
        AttackData data = attacks[dir];

        // Play animation
        animator.Play(data.animation);

        // Small startup delay
        yield return new WaitForSeconds(0.1f);

        // Move hitbox BEFORE enabling
        hitbox.transform.localPosition = data.hitboxOffset;

        // Enable hitbox
        hitbox.SetActive(true);

        // Active frames
        yield return new WaitForSeconds(0.15f);

        // Disable hitbox
        hitbox.SetActive(false);

        // Recovery frames
        yield return new WaitForSeconds(attackInterval);

        // Return to idle
        animator.Play("idle");

        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        if (hitbox == null)
            return;

        BoxCollider2D box = hitbox.GetComponent<BoxCollider2D>();

        if (box == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(
            box.bounds.center,
            box.bounds.size
        );
    }
}