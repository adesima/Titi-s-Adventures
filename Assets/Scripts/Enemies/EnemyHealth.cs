
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 50;
    public int currentHealth;

    [Header("VFX & Death")]
    public GameObject deathVFX;
    public bool destroyOnDeath = true;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (deathVFX != null)
            Instantiate(deathVFX, transform.position, Quaternion.identity);

        if (destroyOnDeath)
            Destroy(gameObject);
    }
}
