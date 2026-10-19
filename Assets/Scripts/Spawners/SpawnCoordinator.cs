using UnityEngine;

public class SpawnCoordinator : MonoBehaviour
{
    [SerializeField] private float minimumTimeBetweenSpawns = 0.5f;

    private float lastSpawnTime = -999f;

    public bool CanSpawn()
    {
        return Time.time - lastSpawnTime >= minimumTimeBetweenSpawns;
    }

    public void RegisterSpawn()
    {
        lastSpawnTime = Time.time;
    }
}
