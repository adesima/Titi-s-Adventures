using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [Header("Setari Viata")]
    public int maxHealth = 100;
    static private int currentHealth;
    private bool isDead = false;

    private Animator anim;
    private Movement movementScript;

    private Rigidbody2D rb;
    private Collider2D col;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        movementScript = GetComponent<Movement>();

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Titi a fost lovit! Viata ramasa: " + currentHealth);

        if (anim != null)
        {
            anim.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Titi a mâncat o căpșună! Viata actuală: " + currentHealth);
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        Debug.Log("Titi a murit!");

        if (anim != null)
        {
            anim.SetBool("IsDead", true);
            anim.SetTrigger("Death");
        }

        if (movementScript != null) movementScript.enabled = false;

        if (rb != null)
        {
            
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        StartCoroutine(DistrugeDupaPauza(3f));
    }

    private IEnumerator DistrugeDupaPauza(float secunde)
    {
        yield return new WaitForSeconds(secunde);
        Destroy(gameObject);
        Debug.Log("Titi a disparut de pe ecran.");
    }

    
    static public int GetCurrentHealth()
    {
        return currentHealth;
    }
}