using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class MeleeEnemy : EnemyAI
{
    public int meleeDamage = 15;
    public float attackWindup = 0.2f;
    public float attackDuration = 0.1f;
    public Collider2D attackHitbox;

    protected override void OnAttack()
    {
        StartCoroutine(DoMeleeAttack());
    }

    System.Collections.IEnumerator DoMeleeAttack()
    {
        yield return new WaitForSeconds(attackWindup);

        if (attackHitbox != null)
        {
            attackHitbox.enabled = true;
            Collider2D[] hits = Physics2D.OverlapBoxAll(attackHitbox.bounds.center, attackHitbox.bounds.size, 0f);

            foreach (var c in hits)
            {
                if (c.CompareTag("Player"))
                {
                    var ph = c.GetComponent<PlayerHealth>();
                    if (ph != null) ph.TakeDamage(meleeDamage);
                }
            }

            yield return new WaitForSeconds(attackDuration);
            attackHitbox.enabled = false;
        }
    }
}