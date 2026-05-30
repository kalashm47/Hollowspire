using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;

    private int currentHealth;

    private void Start()
    {
        currentHealth = stats.maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log($"Player took {damage} damage. Life: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > stats.maxHealth)
            currentHealth = stats.maxHealth;
    }

    public void Die()
    {
        Debug.Log("Player died");

        // Respawn ou Game Over
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}