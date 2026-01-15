using UnityEngine;

public class RatHealth : MonoBehaviour
{
    [Header("Setari Viata")]
    [SerializeField] private int maxHealth = 60;
    private int currentHealth;
    private bool isDead = false;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " lovit! Viata ramasa: " + currentHealth);

        if (animator != null) animator.SetTrigger("Hit");

        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        isDead = true;

        
        if (animator != null) animator.SetBool("isDead", true);

        
        if (GetComponent<Collider2D>()) GetComponent<Collider2D>().enabled = false;

        
        EnemyAI aiScript = GetComponent<EnemyAI>();
        if (aiScript != null) aiScript.enabled = false;

        Debug.Log(gameObject.name + " a murit.");
        Destroy(gameObject, 2f);
    }
}