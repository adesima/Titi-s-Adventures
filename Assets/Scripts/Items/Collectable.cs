using UnityEngine;

public enum CollectableType
{
    Wood,
    Acorn,
    MagicItem
}

public class Collectable : MonoBehaviour
{
    public CollectableType type;
    public int amount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (type)
            {
                case CollectableType.Wood:
                    Debug.Log("Collected Wood +" + amount);
                    // TODO: Add wood to inventory
                    break;

                case CollectableType.Acorn:
                    PlayerHealth hp = collision.GetComponent<PlayerHealth>();
                    if (hp != null)
                    {
                        hp.currentHealth = Mathf.Min(hp.maxHealth, hp.currentHealth + amount);
                        Debug.Log("Healed +" + amount);
                    }
                    break;

                case CollectableType.MagicItem:
                    Debug.Log("Picked up Magic Item!");
                    // TODO: Apply temporary power-up
                    break;
            }

            Destroy(gameObject);
        }
    }
}
