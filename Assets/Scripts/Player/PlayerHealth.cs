using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage Feedback")]
    public GameObject hitVFX;
    public float invincibilityTime = 0.5f;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return; // prevent rapid damage ticks

        currentHealth -= amount;

        if (hitVFX != null)
            Instantiate(hitVFX, transform.position, Quaternion.identity);

        if (currentHealth <= 0)
            Die();

        StartCoroutine(Invincibility());
    }

    System.Collections.IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("Player has died.");
        // Add respawn or game over logic here
    }
}
