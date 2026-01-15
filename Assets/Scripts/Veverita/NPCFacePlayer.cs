using UnityEngine;

public class NPCFacePlayer : MonoBehaviour
{
    public Transform player; 
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        
        if (player == null)
        {
            GameObject titi = GameObject.Find("Titi");
            if (titi != null) player = titi.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            
            Vector2 direction = (player.position - transform.position).normalized;

            
            animator.SetFloat("InputX", direction.x);
            animator.SetFloat("InputY", direction.y);
        }
    }
}