using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public int damage = 10;
    public float lifeTime = 4f;
    public GameObject hitVFX;
    string ownerTag;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    public void Initialize(Vector2 velocity, int damageAmount, string owner = "Enemy")
    {
        rb.linearVelocity = velocity;
        damage = damageAmount;
        ownerTag = owner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ownerTag)) return;

        if (collision.CompareTag("Player"))
        {
            var ph = collision.GetComponent<PlayerHealth>();
            if (ph != null) ph.TakeDamage(damage);
        }

        if (hitVFX != null)
            Instantiate(hitVFX, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}