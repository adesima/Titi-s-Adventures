using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack }
    public State state = State.Patrol;

    public Transform player;
    Rigidbody2D rb;
    protected EnemyBase enemyBase;

    [Header("Movement")]
    public float patrolSpeed = 1.2f;
    public float chaseSpeed = 2.0f;
    public List<Transform> patrolPoints;
    int currentPatrolIndex = 0;
    public float waypointTolerance = 0.2f;

    [Header("Detection")]
    public float detectionRadius = 4.0f;
    public float chaseBreakRadius = 6.0f;
    public float attackRange = 1.0f;
    public float attackCooldown = 1.0f;
    protected float lastAttackTime = -999f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyBase = GetComponent<EnemyBase>();
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (enemyBase != null && enemyBase.isDead) return;

        float distToPlayer = player ? Vector2.Distance(transform.position, player.position) : Mathf.Infinity;

        switch (state)
        {
            case State.Patrol:
                if (player != null && distToPlayer <= detectionRadius) state = State.Chase;
                break;

            case State.Chase:
                if (player == null) { state = State.Patrol; break; }
                if (distToPlayer <= attackRange) state = State.Attack;
                else if (distToPlayer > chaseBreakRadius) state = State.Patrol;
                break;

            case State.Attack:
                if (player == null) { state = State.Patrol; break; }
                if (distToPlayer > attackRange + 0.2f) state = State.Chase;
                break;
        }

        Act(state);
    }

    protected virtual void Act(State s)
    {
        switch (s)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase:  Chase(); break;
            case State.Attack: Attack(); break;
        }
    }

    protected virtual void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Count == 0)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Transform target = patrolPoints[currentPatrolIndex];
        Vector2 dir = (target.position - transform.position);

        if (dir.magnitude <= waypointTolerance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
            return;
        }

        MoveTowards(dir.normalized * patrolSpeed);
    }

    protected virtual void Chase()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dir = (player.position - transform.position).normalized;
        MoveTowards(dir * chaseSpeed);
    }

    protected virtual void Attack()
    {
        rb.linearVelocity = Vector2.zero;

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            OnAttack();
        }
    }

    protected virtual void OnAttack() {}

    protected void MoveTowards(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, chaseBreakRadius);
    }
}