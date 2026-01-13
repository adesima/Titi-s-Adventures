using UnityEngine;

public class RatHealth : MonoBehaviour
{
    [Header("Setari Viata Sobolan")]
    public int maxHealth = 60; // Am pus 60 ca sa moara din 3 lovituri de 20
    private int currentHealth;
    private bool isDead = false;

    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Sobolanul a fost lovit! Viata: " + currentHealth);

        if (anim != null) anim.SetTrigger("Hit");

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (anim != null)
        {
            anim.SetBool("IsDead", true);
            anim.SetTrigger("Death");
        }

        // Dezactivam tot ce tine de sobolan
        if (GetComponent<Collider2D>()) GetComponent<Collider2D>().enabled = false;

        // Dezactivam scriptul de AI/Miscare (Oricum s-ar numi el la tine)
        var aiScript = GetComponent<EnemyAI>();
        if (aiScript != null) aiScript.enabled = false;

        if (GetComponent<Rigidbody2D>()) GetComponent<Rigidbody2D>().simulated = false;

        Destroy(gameObject, 3f);
    }
}