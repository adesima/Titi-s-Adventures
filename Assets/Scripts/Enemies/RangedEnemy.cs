using UnityEngine;

public class RangedEnemy : EnemyAI
{
    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 6f;
    public int projectileDamage = 10;
    public float preFireDelay = 0.15f;

    protected override void OnAttack()
    {
        if (player == null || projectilePrefab == null || firePoint == null) return;
        StartCoroutine(FireProjectile());
    }

    System.Collections.IEnumerator FireProjectile()
    {
        yield return new WaitForSeconds(preFireDelay);

        Vector2 dir = (player.position - firePoint.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Projectile p = proj.GetComponent<Projectile>();
        if (p != null)
            p.Initialize(dir * projectileSpeed, projectileDamage, gameObject.tag);
        else
        {
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = dir * projectileSpeed;
        }
    }
}