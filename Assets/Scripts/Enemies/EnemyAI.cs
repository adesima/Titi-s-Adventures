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
    [SerializeField] private float damageDealt = 1f;
    [SerializeField] private float attackRange = 1.2f; // Distanța de la care lovește
    [SerializeField] private float attackCooldown = 1.5f; // Timp între lovituri

    [Header("Patrol Settings")]
    [SerializeField] private Transform[] patrolPoints; // Trage aici obiectele goale A, B, C

    // Referințe interne
    private int currentPointIndex = 0;
    private float waitCounter;
    private bool isWaiting;
    private bool isAggro; // A devenit agresiv?
    private Transform targetPlayer;
    private float currentHealth;
    private float lastAttackTime;
    private bool isDead;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        waitCounter = waitTimeAtPoint;
    }

    private void Update()
    {
        if (isDead) return; // Dacă e mort, nu face nimic

        // Logica de urmărire și atac (Aggro)
        if (isAggro && targetPlayer != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);

            if (distanceToPlayer <= attackRange)
            {
                // Este în raza de atac, oprește-te și atacă
                StopMovement();
                if (Time.time > lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                }
            }
            else
            {
                // Este prea departe, urmărește player-ul
                MoveTowards(targetPlayer.position);
            }
        }
        // Logica de Patrulare (când nu e Aggro)
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        
        // Dacă suntem departe de punct, mergem spre el
        if (Vector2.Distance(transform.position, targetPoint.position) > 0.1f && !isWaiting)
        {
            MoveTowards(targetPoint.position);
        }
        else
        {
            // Am ajuns la punct
            StopMovement();
            isWaiting = true;
            
            // Numărătoare inversă
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                isWaiting = false;
                waitCounter = waitTimeAtPoint;
                
                // Schimbă la următorul punct (A -> B -> C -> A)
                currentPointIndex++;
                if (currentPointIndex >= patrolPoints.Length)
                {
                    currentPointIndex = 0;
                }
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        // Calculăm direcția
        Vector2 direction = (target - transform.position).normalized;
        
        // Mutăm inamicul
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        // Actualizăm animatorul pentru Blend Tree
        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);
        animator.SetBool("IsMoving", true);
    }

    private void StopMovement()
    {
        animator.SetBool("IsMoving", false);
    }

    private void AttackPlayer()
    {
        lastAttackTime = Time.time;
        animator.SetTrigger("Attack");

        // Aici aplicăm damage player-ului
        // Presupunem că Player-ul are un script cu o metodă TakeDamage()
        // targetPlayer.GetComponent<PlayerHealth>()?.TakeDamage(damageDealt);
        Debug.Log("Inamicul a atacat Player-ul!");
    }

    // Această funcție o poți apela din scriptul armei tale când lovești inamicul
    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        
        // Dezactivăm coliziunile ca să putem trece peste cadavru
        GetComponent<Collider2D>().enabled = false; 
        
        // Opțional: Distruge obiectul după ce se termină animația (ex: 2 secunde)
        Destroy(gameObject, 2f);
    }

    // Detectarea zonei de Aggro
    // Asigură-te că Inamicul are un Collider 2D cu "Is Trigger" bifat pentru raza de detecție
    // SAU un child object cu trigger. 
    // Dacă pui trigger pe inamic direct, ai grijă să nu interfereze cu hit box-ul fizic.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAggro)
        {
            // L-am văzut! Începe urmărirea infinită.
            isAggro = true;
            targetPlayer = other.transform;
            Debug.Log("Player detectat! Atac!");
        }
    }
}