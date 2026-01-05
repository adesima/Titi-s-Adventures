using UnityEngine;

public class WoodCollectible : MonoBehaviour
{
    // Această funcție se declanșează automat când un obiect intră în trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Încercăm să luăm componenta PlayerInventory de pe obiectul care ne-a atins
        PlayerInventory player = collision.GetComponent<PlayerInventory>();

        // Dacă obiectul are scriptul (deci e player-ul)
        if (player != null)
        {
            player.AddWood();    // Adăugăm lemnul în inventar
            Destroy(gameObject); // Distrugem obiectul de lemn (dispare din scenă)
        }
    }
}