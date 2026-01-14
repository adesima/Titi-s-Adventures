using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTimeAtPoint = 2f;

    [Header("Combat Stats")]
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private int damageDealt = 10;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 3.0f;

    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolPoints;

    private int currentPointIndex = 0;
    private float waitCounter;
    private bool isWaiting;
    private bool isAggro;
    private Transform targetPlayer;
    private float currentHealth;
    private float lastAttackTime;
    private bool isDead;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        waitCounter = waitTimeAtPoint;
        lastAttackTime = -attackCooldown;
    }

    private void Update()
    {
        if (isDead) return;

        if (isAggro && targetPlayer != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);

            // Actualizăm direcția chiar și când stă pe loc să atace, ca să privească spre Titi
            FaceTarget(targetPlayer.position);

            if (distanceToPlayer <= attackRange)
            {
                StopMovement();

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
            else
            {
                MoveTowards(targetPlayer.position);
            }
        }
        else
        {
            Patrol();
        }
    }

    private void AttackPlayer()
    {
        lastAttackTime = Time.time;
        animator.SetTrigger("Attack");

        if (targetPlayer != null)
        {
            // Verifică dacă scriptul de sănătate al lui Titi se numește HealthBar sau PlayerHealth
            var titiHealth = targetPlayer.GetComponent<HealthBar>();
            if (titiHealth != null)
            {
                titiHealth.TakeDamage(damageDealt);
            }
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        Transform targetPoint = patrolPoints[currentPointIndex];

        if (Vector2.Distance(transform.position, targetPoint.position) > 0.1f && !isWaiting)
        {
            MoveTowards(targetPoint.position);
        }
        else
        {
            StopMovement();
            isWaiting = true;
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                isWaiting = false;
                waitCounter = waitTimeAtPoint;
                currentPointIndex++;
                if (currentPointIndex >= patrolPoints.Length) currentPointIndex = 0;
            }
        }
    }

    // Această funcție doar rotește inamicul vizual (prin Animator) spre țintă
    private void FaceTarget(Vector3 target)
    {
        Vector2 direction = (target - transform.position).normalized;

        // Trimitem valorile către Blend Tree
        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);
    }

    private void MoveTowards(Vector3 target)
    {
        FaceTarget(target); // Ne asigurăm că privește unde merge
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        animator.SetBool("IsMooving", true);
    }

    private void StopMovement()
    {
        animator.SetBool("IsMooving", false);
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;
        currentHealth -= damageAmount;
        animator.SetTrigger("Hit");
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true); // Atenție la litere mari/mici: isDead vs IsDead
        GetComponent<Collider2D>().enabled = false;

        // Oprim orice mișcare la moarte
        StopMovement();

        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAggro)
        {
            isAggro = true;
            targetPlayer = other.transform;
        }
    }
}