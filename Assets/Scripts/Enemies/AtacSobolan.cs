using UnityEngine;

public class SobolanAtac : MonoBehaviour
{
    [Header("Setari Atac")]
    public Transform tintaJucator;   // Trage-l pe Titi aici
    public float razaAtac = 1f;      // Distanta de la care musca
    public int damage = 1;           // Cat HP scade
    public float timpIntreAtacuri = 1.5f; // Sa nu atace de 100 de ori pe secunda

    private float timpUrmatorAtac = 0f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Siguranta: Daca nu l-ai pus pe Titi, nu face nimic
        if (tintaJucator == null) return;

        // 2. Calculam distanta
        float distanta = Vector2.Distance(transform.position, tintaJucator.position);

        // 3. Verificam daca e timpul sa atace
        if (distanta <= razaAtac && Time.time >= timpUrmatorAtac)
        {
            Musca();
        }
    }

    void Musca()
    {
        // Setam timpul pentru urmatorul atac
        timpUrmatorAtac = Time.time + timpIntreAtacuri;

        // A. Pornim Animatia (Trebuie sa ai Trigger "Attack" in Animator)
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        // B. Dam Damage
        // Cautam scriptul "SistemViata" pe Titi
        SistemViata viataTiti = tintaJucator.GetComponent<SistemViata>();

        if (viataTiti != null)
        {
            viataTiti.PrimesteDamage(damage);
            Debug.Log("Sobolanul a atacat!");
        }
    }

    // Desenam cercul rosu in Editor ca sa vezi raza
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, razaAtac);
    }
}