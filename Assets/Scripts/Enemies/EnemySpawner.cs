using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int count = 1;
    public float spawnRadius = 1f;
    public bool spawnOnStart = true;

    void Start()
    {
        if (spawnOnStart) Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}