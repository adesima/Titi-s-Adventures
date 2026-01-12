using UnityEngine;

public class SistemViata : MonoBehaviour
{
    [Header("Setari")]
    public int viataMaxima = 3;

    [Header("Status (Doar de vizualizat)")]
    public int viataCurenta;
    public bool esteMort = false; // Asta va fi citita de alte scripturi

    private Animator anim;
    private Collider2D col;
    private Rigidbody2D rb;

    void Start()
    {
        // Initializam viata
        viataCurenta = viataMaxima;

        // Luam componentele automat
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Aceasta este functia pe care o apeleaza Titi cand da cu sabia
    public void PrimesteDamage(int damage)
    {
        // Daca e deja mort, nu-l mai batem
        if (esteMort) return;

        // Scadem viata
        viataCurenta -= damage;

        // Pornim animatia de lovitura (Trebuie sa ai Trigger "Hit" in Animator!)
        if (anim != null) anim.SetTrigger("Hit");

        // Verificam daca a murit
        if (viataCurenta <= 0)
        {
            Moare();
        }
    }

    void Moare()
    {
        esteMort = true;
        Debug.Log(gameObject.name + " a murit!");

        // Animatia de moarte (Trigger "Die" si Bool "IsDead" ca in pozele tale)
        if (anim != null)
        {
            anim.SetBool("IsDead", true);
            anim.SetTrigger("Die");
        }

        // Oprim fizica ca sa nu il mai putem impinge sau lovi
        if (col != null) col.enabled = false;
        if (rb != null) rb.bodyType = RigidbodyType2D.Static;

        // Oprim orice script de miscare de pe acest obiect (ca sa nu mai patruleze mort)
        // Cauta automat scripturi comune de miscare si le opreste
        var miscare1 = GetComponent<SobolanMovement>();
        if (miscare1 != null) miscare1.enabled = false;

        // Distruge obiectul de pe scena dupa 4 secunde
        Destroy(gameObject, 4f);
    }
}