using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [SerializeField] private int healAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificăm dacă obiectul care a atins căpșuna are Tag-ul "Player"
        if (collision.CompareTag("Player"))
        {
            // Căutăm scriptul de viață pe care îl are Titi (HealthBar)
            HealthBar titiHealth = collision.GetComponent<HealthBar>();

            // Dacă am găsit scriptul, vindecăm și distrugem căpșuna
            if (titiHealth != null)
            {
                titiHealth.Heal(healAmount); // Trimitem valoarea de vindecare

                Debug.Log("Căpșună colectată! Titi s-a vindecat cu: " + healAmount);

                Destroy(gameObject); // Căpșuna dispare de pe ecran
            }
        }
    }
}