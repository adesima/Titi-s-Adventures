using UnityEngine;

[System.Serializable]
public class DropEntry
{
    public GameObject itemPrefab;
    [Range(0,1)] public float chance = 1.0f;
    public int minAmount = 1;
    public int maxAmount = 1;
}

public class ItemDrop : MonoBehaviour
{
    public DropEntry[] drops;

    public void RollDrop(Vector2 position)
    {
        foreach (var d in drops)
        {
            if (d.itemPrefab == null) continue;

            if (Random.value <= d.chance)
            {
                int amount = Random.Range(d.minAmount, d.maxAmount + 1);

                for (int i = 0; i < amount; i++)
                {
                    Vector2 offset = Random.insideUnitCircle * 0.4f;
                    Instantiate(d.itemPrefab, position + offset, Quaternion.identity);
                }
            }
        }
    }
}