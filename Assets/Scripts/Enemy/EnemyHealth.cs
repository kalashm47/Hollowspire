using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maximumHealth = 3;

    [SerializeField]
    private float health;

    [Header("Effects")]
    public GameObject explosionPrefab;

    void Start()
    {
        health = maximumHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        Debug.Log("Enemy took damage!");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }

}