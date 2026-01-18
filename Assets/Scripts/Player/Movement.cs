using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    private Transform _transform;
    private InputSystem_ActionsCopy _inputActions;
    private Animator _animator;
    PlayerInventory player;
    NPCDialog npcDialog;

    private Vector2 lastoMoveDirection;
    private bool facingLeft = true;

    private SpriteRenderer spriteRenderer;
    private float xPosLastFrame;

    // --- AUDIO: Variabile noi ---
    [Header("Audio Setari")]
    public AudioSource movementAudioSource; // Trage componenta AudioSource aici
    public AudioClip pasiSound;             // Trage fisierul de sunet (pasi) aici
    // ----------------------------

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = GetComponent<Transform>();
        _inputActions = new InputSystem_ActionsCopy();
        _inputActions.Enable();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // --- AUDIO: Gasim automat sursa daca ai uitat sa o pui ---
        if (movementAudioSource == null)
            movementAudioSource = GetComponent<AudioSource>();
        // ---------------------------------------------------------
        player =    GetComponent<PlayerInventory>();
        npcDialog = GameObject.Find("Dialog").GetComponent<NPCDialog>();
    }

    // Update is called once per frame



    void FixedUpdate()
    {
        bool isInteracting = _inputActions.Player.Interact.ReadValue<float>() > 0.5f;
        bool isSprinting = _inputActions.Player.Sprint.ReadValue<float>() > 0.5f;
        bool isAttacking = _inputActions.Player.Attack.ReadValue<float>() > 0.5f;
        Vector2 movementInput = _inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0)*Time.deltaTime*5f;

        
        if((movementInput.x!=0 || movementInput.y!=0))
        {
           // Debug.Log("Updating last move direction to: " + movementInput);
            lastoMoveDirection = movementInput;
        }

        //if (movementInput.x < 0 && !facingLeft || movementInput.x > 0 && facingLeft)
        //{
        //    //Vector3 scale =transform.localScale;
        //    //scale.x*=-1; //face x negativ >>da flip
        //    //transform.localScale=scale;
        //    //facingLeft = !facingLeft;
        //}
        if (isInteracting)
        {
            Collider2D[] ItemColliders = Physics2D.OverlapCircleAll(transform.position, 0.25f,LayerMask.GetMask("Item"));
            foreach (var hitCollider in ItemColliders)
            {
                Debug.Log("Interacted with " + hitCollider.name);
                UIManager.AddItem(hitCollider);

                if (hitCollider.name.Contains("Ghinda"))
                {
                    
                }

            }

            Collider2D[] NPCColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f,LayerMask.GetMask("NPC"));
            foreach (var hitCollider in NPCColliders)
            {
                Debug.Log("Interacted with " + hitCollider.name);
                if(hitCollider.name.Contains("CastorApa"))
                {
                    npcDialog.StartDialog(NPCDialog.DialogListNames.CastorApa, player.woodCount);

                }
                else if (hitCollider.name.Contains("PersonajRau"))
                {
                    npcDialog.StartDialog(NPCDialog.DialogListNames.PersonajRau, player.woodCount);
                }
                else if (hitCollider.name.Contains("Veverita"))
                {
                    npcDialog.StartDialog(NPCDialog.DialogListNames.Veverita, player.woodCount);
                }
            }
        }
       
        
        if (movement != Vector3.zero)
        {

            _animator.SetBool("isMoving", true);

            _animator.SetFloat("lastMoveX", lastoMoveDirection.x);
            _animator.SetFloat("lastMoveY", lastoMoveDirection.y);
            _animator.SetFloat("moveMagnitude", movement.magnitude);
            _animator.SetFloat("moveX", movement.x);
            _animator.SetFloat("moveY", movement.y);

            
            if (isSprinting)
            {
                _animator.SetBool("isSprinting", true);
                _transform.position += movement;
                _animator.SetBool("isMoving", false);

                // --- AUDIO: Redam sunetul de pasi daca nu este deja redat ---
                movementAudioSource.pitch = 1.5f; // Pasii sunt mai rapizi la sprint
            }
            else
            {
                _animator.SetBool("isSprinting", false);
                _transform.position += movement * 0.5f;

                // --- AUDIO: Redam sunetul de pasi daca nu este deja redat ---
                movementAudioSource.pitch = 1.0f; // Pasii normali
            }
            if (isAttacking)
            {
                _animator.SetBool("isAttacking", true);
                _animator.SetBool("isMoving", false);

            }
            else
            {
                _animator.SetBool("isAttacking", false);
            }

            // --- AUDIO: Redam sunetul de pasi daca nu este deja redat ---
            GestioneazaSunetPasi(true);

        }

        else
        {
            _animator.SetBool("isMoving", false);
            _animator.SetBool("isSprinting", false);

            // --- AUDIO: Oprim sunetul de pasi daca ne-am oprit din miscare ---
            GestioneazaSunetPasi(false);
        }

        if (isAttacking)
        {
            _animator.SetBool("isAttacking", true);
            _animator.SetBool("isMoving", false);

        }
        else
        {
            _animator.SetBool("isAttacking", false);
        }

        FlipCharacter();
    }

    private void FlipCharacter()
    {
        if(transform.position.x > xPosLastFrame)
        {       // moving right
            spriteRenderer.flipX = false;
        }       // moving left  
        else if (transform.position.x < xPosLastFrame)
        {
            spriteRenderer.flipX = true;
        }
        xPosLastFrame = transform.position.x;
    }

    // --- AUDIO: Functie noua pentru gestionarea sunetului ---
    void GestioneazaSunetPasi(bool seMisca)
    {
        // Masura de siguranta sa nu dea eroare daca lipsesc componentele
        if(movementAudioSource == null || pasiSound == null) return;

        if (seMisca)
        {
            // Daca trebuie sa se auda, dar nu canta deja...
            if (!movementAudioSource.isPlaying)
            {
                movementAudioSource.clip = pasiSound;
                movementAudioSource.loop = true; // Foarte important: sunetul se repeta singur
                movementAudioSource.Play();
            }
        }
        else
        {
            // Daca s-a oprit din mers, dar sunetul inca se aude...
            if (movementAudioSource.isPlaying && movementAudioSource.clip == pasiSound)
            {
                movementAudioSource.Stop();
            }
        }
    }
    // --------------------------------------------------------
    

}

