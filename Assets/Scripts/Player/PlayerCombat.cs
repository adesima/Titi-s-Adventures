using UnityEngine;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour
{
    [Header("Setari Atac")]
    public float attackRadius = 1.2f; 
    public LayerMask enemyLayers;    
    public int damage = 20;
    public float attackCooldown = 0.5f;

    [Header("Referinte")]
    public Animator animator;

    private float nextAttackTime = 0f;

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

        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayers);

        
        foreach (Collider2D enemy in hitEnemies)
        {
            
            RatHealth health = enemy.GetComponent<RatHealth>();

            if (health != null)
            {
                health.TakeDamage(damage);
                Debug.Log("Lovit: " + enemy.name);
            }
        }

    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}