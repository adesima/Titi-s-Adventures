using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [SerializeField] private int healAmount = 50; // Valoarea cu care vindecă

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificăm dacă obiectul care a atins căpșuna este Player-ul
        // (Asigură-te că Player-ul are Tag-ul "Player")
        if (collision.CompareTag("Player"))
        {
            // Căutăm scriptul de viață pe obiectul lovit
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            // Dacă scriptul există, aplicăm vindecarea
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                
                // Distrugem căpșuna după ce a fost consumată
                Destroy(gameObject);
            }
        }
    }
}