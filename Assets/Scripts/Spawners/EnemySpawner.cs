using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Difficulty")]
    [SerializeField, Min(0.1f)] private float minSpawnTime = 4f;
    [SerializeField, Min(0f)] private float spawnTimeStep = 0.5f;
    [SerializeField, Min(0.1f)] private float difficultyInterval = 20f;

    private float currentSpawnTime;
    private float timeUntilNextDifficulty;

    public override void StartSpawning()
    {
        currentSpawnTime = spawnTime;
        timeUntilNextDifficulty = difficultyInterval;

        base.StartSpawning();
        timeUntilNextSpawn = 0f;
    }

    protected override void Update()
    {
        base.Update();

        if (!spawning)
            return;

        timeUntilNextDifficulty -= Time.deltaTime;

        if (timeUntilNextDifficulty > 0f)
            return;

        timeUntilNextDifficulty = difficultyInterval;
        currentSpawnTime = Mathf.Max(minSpawnTime, currentSpawnTime - spawnTimeStep);
    }

    protected override float GetNextSpawnTime()
    {
        return currentSpawnTime;
    }

    protected override Vector3 GetSpawnPosition()
    {
        if (spawnPoints.Length == 0)
            return base.GetSpawnPosition();

        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index].position;
    }

    protected override void OnSpawned(Enemy item)
    {
        item.SetTarget(target);
    }
}
