using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Viteza de mișcare
    public Rigidbody2D rb;       // Referință la Rigidbody

    Vector2 movement;            // Stochează direcția X și Y

    void Update()
    {
        // 1. Citim Input-ul aici (A/D sau Săgeți pentru X, W/S pentru Y)
        // Folosim "GetAxisRaw" pentru mișcare instantanee (fără alunecare)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalizăm vectorul ca să nu ne mișcăm mai repede pe diagonală
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // 2. Aplicăm fizica aici
        // MovePosition este cea mai sigură metodă pentru coliziuni cu pereți
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}