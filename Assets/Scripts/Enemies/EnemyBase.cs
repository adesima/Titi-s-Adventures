using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 50;
    public int currentHealth;

    [Header("Death")]
    public GameObject deathVFX;
    public ItemDrop itemDrop;

    [Header("Combat")]
    public int contactDamage = 10;

    public bool isDead { get; private set; }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        OnHurt();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void OnHurt() {}

    protected virtual void Die()
    {
        isDead = true;
        if (deathVFX != null)
            Instantiate(deathVFX, transform.position, Quaternion.identity);

        if (itemDrop != null)
            itemDrop.RollDrop(transform.position);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        StartCoroutine(DelayDestroy());
    }

    protected virtual IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.collider.CompareTag("Player"))
        {
            var ph = collision.collider.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(contactDamage);
        }
    }
}