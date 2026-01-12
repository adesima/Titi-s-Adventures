using System.Collections;
using UnityEngine;

// Liniile astea OBLIGA Unity sa puna componentele lipsa. Adio erori!
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class SobolanFinal : MonoBehaviour
{
    [Header("1. Trage aici punctele si jucatorul")]
    public Transform[] punctePatrulare; // Aici tragi PunctA, PunctB, C
    public Transform tintaJucator;      // Aici il tragi pe Titi

    [Header("2. Setari")]
    public float viteza = 2f;
    public float timpAsteptare = 2f;
    public float razaDetectie = 5f;
    public float razaAtac = 1f;

    // Variabile interne
    private int index = 0;
    private bool asteapta = false;
    private bool eMort = false;

    private Animator anim;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    void Start()
    {
        // Aici facem legaturile automat
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Setam fizica sa nu cada prin pamant
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (eMort) return;
        if (tintaJucator == null) return; // Daca ai uitat sa pui jucatorul, nu facem nimic

        float distanta = Vector2.Distance(transform.position, tintaJucator.position);

        // LOGICA: 1. Atac -> 2. Urmarire -> 3. Patrulare
        if (distanta < razaAtac)
        {
            Ataca();
        }
        else if (distanta < razaDetectie)
        {
            Urmareste();
        }
        else
        {
            Patruleaza();
        }
    }

    void Patruleaza()
    {
        if (asteapta || punctePatrulare.Length == 0) return;

        Transform tinta = punctePatrulare[index];
        MergiCatre(tinta.position);

        if (Vector2.Distance(transform.position, tinta.position) < 0.2f)
        {
            StartCoroutine(Pauza());
        }
    }

    void Urmareste()
    {
        MergiCatre(tintaJucator.position);
    }

    void MergiCatre(Vector2 pozitie)
    {
        transform.position = Vector2.MoveTowards(transform.position, pozitie, viteza * Time.deltaTime);

        // ATENTIE: Aici trebuie sa fie numele EXACT din Animatorul tau
        // Am pus "IsMooving" (cu doi de O) pentru ca asa era in poza ta. 
        // Daca l-ai corectat intre timp in "IsMoving", sterge un O de aici!
        anim.SetBool("IsMooving", true);

        // Flip sprite
        if (pozitie.x < transform.position.x) sprite.flipX = false;
        else sprite.flipX = true;
    }

    void Ataca()
    {
        anim.SetBool("IsMooving", false); // Se opreste
        // Numele Trigger-ului de atac din poza ta
        anim.SetTrigger("Attack");
    }

    IEnumerator Pauza()
    {
        asteapta = true;
        anim.SetBool("IsMooving", false);
        yield return new WaitForSeconds(timpAsteptare);
        index = (index + 1) % punctePatrulare.Length;
        asteapta = false;
    }

    // Functie apelata cand moare
    public void PrimesteDamage()
    {
        eMort = true;
        anim.SetBool("IsDead", true); // Confirmare din poza ta
        anim.SetTrigger("Die");

        GetComponent<Collider2D>().enabled = false; // Cade lat
        this.enabled = false; // Opreste scriptul
    }

    // Deseneaza cercurile colorate in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, razaDetectie);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, razaAtac);
    }
}