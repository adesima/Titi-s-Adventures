using UnityEngine;
using System.Collections.Generic; // Avem nevoie de asta pentru HashSet

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public Transform[] attackPoints; // MODIFICAT: Acum avem o listă de puncte
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int damage = 20;
    public float attackCooldown = 0.5f;

    private float nextAttackTime = 0f;
    private Animator animator;

    void Awake() { animator = GetComponent<Animator>(); }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        if (animator != null) animator.SetTrigger("Attack");

        // Folosim un HashSet pentru a nu lovi același inamic de mai multe ori
        HashSet<RatHealth> enemiesHit = new HashSet<RatHealth>();

        // Verificăm fiecare punct de atac din listă
        foreach (Transform point in attackPoints)
        {
            if (point == null) continue;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(point.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                RatHealth health = enemy.GetComponent<RatHealth>();
                if (health != null)
                {
                    enemiesHit.Add(health);
                }
            }
        }

        // Aplicăm damage inamicilor unici detectați
        foreach (RatHealth enemyHealth in enemiesHit)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    // Desenăm cercurile pentru toate punctele în Editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoints == null) return;
        Gizmos.color = Color.red;
        foreach (Transform point in attackPoints)
        {
            if (point != null)
                Gizmos.DrawWireSphere(point.position, attackRange);
        }
    }
}