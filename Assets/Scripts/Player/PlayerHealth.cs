// using UnityEngine;

// public class PlayerHealth : MonoBehaviour
// {
//     public int maxHealth = 100;
//     public int currentHealth;

//     [Header("Damage Feedback")]
//     public GameObject hitVFX;
//     public float invincibilityTime = 0.5f;
//     private bool isInvincible = false;

//     void Start()
//     {
//         currentHealth = maxHealth;
//     }

//     public void TakeDamage(int amount)
//     {
//         if (isInvincible) return; // prevent rapid damage ticks

//         currentHealth -= amount;

//         if (hitVFX != null)
//             Instantiate(hitVFX, transform.position, Quaternion.identity);

//         if (currentHealth <= 0)
//             Die();

//         StartCoroutine(Invincibility());
//     }

//     System.Collections.IEnumerator Invincibility()
//     {
//         isInvincible = true;
//         yield return new WaitForSeconds(invincibilityTime);
//         isInvincible = false;
//     }

//     void Die()
//     {
//         Debug.Log("Player has died.");
//         // Add respawn or game over logic here
//     }
// }


using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;

    [Header("Visuals")]
    public GameObject hitVFX;
    public float invincibilityTime = 0.5f;

    private bool isInvincible = false;
    private bool isDead = false;
    
    // Referință la Animator
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        
        // Dacă HealthBar folosește metoda statică, ne asigurăm că pornește corect
        // (Opțional, depinde cum e inițializat HealthBar-ul tău)
    }

    // Funcția pe care o va apela Căpșuna
    public void Heal(int amount)
    {
        currentHealth += amount;

        // Aici facem "truncherea". Dacă depășește maxHealth, devine maxHealth.
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        Debug.Log("Viata actuala: " + currentHealth);
        
        // Aici ai putea adăuga o funcție UpdateHealthUI(); dacă ai bara de viata
    }

    public void TakeDamage(int amount)
    {
        // 1. Verificări: dacă e invincibil sau mort, nu face nimic
        if (isInvincible || isDead) return; 

        // 2. Scade viața
        currentHealth -= amount;
        
        // 3. Actualizează Bara de viață (folosind scriptul tău HealthBar.cs)
        HealthBar healthBarScript = FindAnyObjectByType<HealthBar>();
        if (healthBarScript != null)
        {
            healthBarScript.TakeDamage(amount);
        }

        // 4. VFX și Animație de lovitură
        if (hitVFX != null)
            Instantiate(hitVFX, transform.position, Quaternion.identity);

        if (animator != null)
            animator.SetTrigger("Hit"); // Asigură-te că ai parametrul "Hit" în Animator

        // 5. Verificare Moarte
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invincibility());
        }
    }

    System.Collections.IEnumerator Invincibility()
    {
        isInvincible = true;
        // Aici poți face player-ul să pâlpâie (opțional)
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player has died.");
        
        if (animator != null)
            animator.SetBool("IsDead", true);

        // Dezactivăm scripturile de control (Mișcare/Atac) ca să nu mai poți umbla mort
        // GetComponent<PlayerMovement>()?.enabled = false;
        PlayerCombat combat = GetComponent<PlayerCombat>();
        if (combat != null)
            combat.enabled = false;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}