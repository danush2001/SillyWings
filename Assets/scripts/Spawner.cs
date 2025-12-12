using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Prefab;
    public float baseSpawnRate = 1.5f; // Default spawn rate
    private float currentSpawnRate;
    public float minHeight = -1f;
    public float maxHeight = 1f;
    private float lastSpawnRate = -1f; // Store the previous spawn rate

    private void OnEnable()
    {
        currentSpawnRate = baseSpawnRate;
        InvokeRepeating(nameof(Spawn), currentSpawnRate, currentSpawnRate);
    }

    private void Update()
    {
        AdjustSpawnRate();
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(Prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }

    private void AdjustSpawnRate()
    {
        float difficultyFactor = Mathf.Clamp(1 - (GameManager.Instance.Score * 0.01f), 0.5f, 1.0f);
        float newSpawnRate = Mathf.Clamp(baseSpawnRate * difficultyFactor, 0.5f, baseSpawnRate);

        // ✅ Only restart spawning if the new spawn rate is significantly different
        if (Mathf.Abs(newSpawnRate - lastSpawnRate) > 0.1f)
        {
            lastSpawnRate = newSpawnRate;
            CancelInvoke(nameof(Spawn));
            InvokeRepeating(nameof(Spawn), newSpawnRate, newSpawnRate);
        }
    }
}
