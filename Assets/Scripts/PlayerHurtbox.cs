using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerHealth.TakeDamage(1);
        }
            /*
                if (other.CompareTag("Hazard"))
                {
                    playerHealth.Die();
                }*/
    }
}