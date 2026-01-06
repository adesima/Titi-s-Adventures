using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    private Transform _transform;
    private InputSystem_ActionsCopy _inputActions;
    public Animator _animator;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private float xPosLastFrame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = GetComponent<Transform>();
        _inputActions = new InputSystem_ActionsCopy();
        _inputActions.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isInteracting = _inputActions.Player.Interact.ReadValue<float>() > 0.5f;
        bool isSprinting = _inputActions.Player.Sprint.ReadValue<float>() > 0.5f;
        bool isAttacking = _inputActions.Player.Attack.ReadValue<float>() > 0.5f;
        Vector2 movementInput = _inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0)*Time.deltaTime*5f;
        
        if(isInteracting)
        {
            Collider2D[] ItemColliders = Physics2D.OverlapCircleAll(transform.position, 0.25f,LayerMask.GetMask("Item"));
            foreach (var hitCollider in ItemColliders)
            {
                //Debug.Log("Interacted with " + hitCollider.name);
                UIManager.AddItem(hitCollider);

            }
             Collider2D[] NPCColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f,LayerMask.GetMask("NPC"));
            foreach (var hitCollider in NPCColliders)
            {
                //Debug.Log("Interacted with " + hitCollider.name);
                //TODO: Add NPC interaction logic here
            }
        }
       
        
        if (movement != Vector3.zero)
        {

            _animator.SetBool("isMoving", true);
            if (movement.x > 0)
            {
                
                _animator.SetFloat("moveX", 1);
            }
            else if (movement.x < 0)
            {
               
                _animator.SetFloat("moveX", -1);
            }
            else
            {
                _animator.SetFloat("moveX", 0);
            }
            if (movement.y > 0)
            {

                _animator.SetFloat("moveY", 1);
            }
            else if (movement.y < 0)
            {
                _animator.SetFloat("moveY", -1);
            }
            else
            {
                _animator.SetFloat("moveY", 0);
            }
            if (isSprinting)
            {
                _animator.SetBool("isSprinting", true);
                _transform.position += movement;
                _animator.SetBool("isMoving", false);
            }
            else
            {
                _animator.SetBool("isSprinting", false);
                _transform.position += movement * 0.5f;
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
        }

        else
        {
            _animator.SetBool("isMoving", false);
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

}
