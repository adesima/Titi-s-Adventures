using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Cere obligatoriu Rigidbody
public class SobolanMovement : MonoBehaviour
{
    public Animator _animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Setari AI")]
    public Transform jucator;
    public Transform[] punctePatrulare;
    public float viteza = 2f;
    public float razaDetectie = 5f;
    public float razaAtac = 1f;
    public float timpAsteptare = 2f;

    // Variabile interne
    private int indexPunct = 0;
    private bool asteapta = false;
    private float xPosLastFrame;
    private bool isAttacking = false;

    // NOU: Referinta la fizica
    private Rigidbody2D rb;

    void Start()
    {
        // Luam componenta de fizica
        rb = GetComponent<Rigidbody2D>();
        xPosLastFrame = transform.position.x;
    }

    void FixedUpdate()
    {
        Vector3 directieDorita = CalculeazaDirectieAI();

        // MODIFICARE: Calculam miscarea dar NU o aplicam cu transform
        Vector2 movement = directieDorita * viteza * Time.deltaTime;

        if (!isAttacking && !asteapta)
        {
            // NOU: Mutam sobolanul folosind Fizica (Respecta zidurile!)
            rb.MovePosition(rb.position + movement);
        }

        GestioneazaAnimatii(movement);
        FlipCharacter();
    }

    // --- Restul codului ramane la fel ca inainte ---
    Vector3 CalculeazaDirectieAI()
    {
        if (jucator == null) return Vector3.zero;

        float distanta = Vector2.Distance(transform.position, jucator.position);
        Vector3 directie = Vector3.zero;

        if (distanta < razaAtac)
        {
            isAttacking = true;
            return Vector3.zero;
        }
        else { isAttacking = false; }

        if (distanta < razaDetectie)
        {
            asteapta = false;
            directie = (jucator.position - transform.position).normalized;
        }
        else
        {
            if (punctePatrulare.Length > 0 && !asteapta)
            {
                Transform tinta = punctePatrulare[indexPunct];
                directie = (tinta.position - transform.position).normalized;
                if (Vector2.Distance(transform.position, tinta.position) < 0.2f)
                {
                    StartCoroutine(AsteaptaLaPunct());
                }
            }
        }
        return directie;
    }

    void GestioneazaAnimatii(Vector3 movement)
    {
        if (movement != Vector3.zero && !asteapta && !isAttacking)
        {
            _animator.SetBool("isMoving", true);
            _animator.SetBool("isAttacking", false);

            if (movement.x > 0.01f) _animator.SetFloat("moveX", 1);
            else if (movement.x < -0.01f) _animator.SetFloat("moveX", -1);
            else _animator.SetFloat("moveX", 0);

            if (movement.y > 0.01f) _animator.SetFloat("moveY", 1);
            else if (movement.y < -0.01f) _animator.SetFloat("moveY", -1);
            else _animator.SetFloat("moveY", 0);
        }
        else
        {
            _animator.SetBool("isMoving", false);
            if (isAttacking) _animator.SetBool("isAttacking", true);
            else _animator.SetBool("isAttacking", false);
        }
    }

    IEnumerator AsteaptaLaPunct()
    {
        asteapta = true;
        yield return new WaitForSeconds(timpAsteptare);
        indexPunct++;
        if (indexPunct >= punctePatrulare.Length) indexPunct = 0;
        asteapta = false;
    }

    private void FlipCharacter()
    {
        if (transform.position.x > xPosLastFrame) spriteRenderer.flipX = false;
        else if (transform.position.x < xPosLastFrame) spriteRenderer.flipX = true;
        xPosLastFrame = transform.position.x;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, razaDetectie);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, razaAtac);
    }
}