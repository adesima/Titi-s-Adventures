using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public Transform attackPoint;   // Un obiect gol pus în fața jucătorului
    public float attackRange = 0.8f;
    public LayerMask enemyLayers;   // Layer-ul pe care sunt inamicii
    public int damage = 20;
    public float attackCooldown = 0.5f;

    private float nextAttackTime = 0f;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Putem ataca doar dacă a trecut timpul de cooldown
        if (Time.time >= nextAttackTime)
        {
            // Click Stânga (Fire1) sau Space
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) 
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        // 1. Pornim animația
        if (animator != null)
            animator.SetTrigger("Attack");

        // 2. Detectăm inamicii în raza de acțiune
        // Notă: Folosim OverlapCircle pentru a vedea ce atingem
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // 3. Le dăm damage
        foreach (Collider2D enemy in hitEnemies)
        {
            // Încercăm să găsim scriptul EnemyBase (pe care l-ai urcat tu)
            EnemyBase enemyScript = enemy.GetComponent<EnemyBase>();
            
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
                Debug.Log("Am lovit inamicul: " + enemy.name);
            }
            else
            {
                // Verificăm și EnemyHealth în caz că folosești scriptul vechi pe unii inamici
                SistemViata oldEnemyScript = enemy.GetComponent<SistemViata>();
                if(oldEnemyScript != null)
                {
                    oldEnemyScript.PrimesteDamage(damage);
                }
            }
        }
    }

    // Vizualizăm raza atacului în Editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}