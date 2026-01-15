using UnityEngine;

public class Strawberry : MonoBehaviour
{
    [SerializeField] private int healAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            
            HealthBar titiHealth = collision.GetComponent<HealthBar>();

            
            if (titiHealth != null)
            {
                titiHealth.Heal(healAmount); 

                Debug.Log("Căpșună colectată! Titi s-a vindecat cu: " + healAmount);

                Destroy(gameObject); 
            }
        }
    }
}