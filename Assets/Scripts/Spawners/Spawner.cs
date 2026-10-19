using UnityEngine;

public abstract class Spawner<T> : SpawnerBase where T : Component
{
    [SerializeField] protected T[] prefabs;
    [SerializeField, Min(1)] protected int instancesPerPrefab = 3;
    [SerializeField, Min(0.1f)] protected float spawnTime = 2f;
    [SerializeField] protected SpawnCoordinator spawnCoordinator;
    [SerializeField] private bool spawnOnStart = true;

    protected ObjectPool<T>[] pools;
    protected float timeUntilNextSpawn;
    protected bool spawning;

    protected virtual void Awake()
    {
        pools = new ObjectPool<T>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new ObjectPool<T>(prefabs[i], instancesPerPrefab, transform);
        }
    }

    protected virtual void Start()
    {
        if (spawnOnStart)
            StartSpawning();
    }

    protected virtual void Update()
    {
        if (!spawning || pools.Length == 0) return;

        timeUntilNextSpawn -= Time.deltaTime;

        if (timeUntilNextSpawn > 0f) return;

        if (spawnCoordinator != null && !spawnCoordinator.CanSpawn())
            return;

        SpawnOne();

        if (spawnCoordinator != null)
            spawnCoordinator.RegisterSpawn();

        timeUntilNextSpawn = GetNextSpawnTime();
    }

    protected virtual float GetNextSpawnTime()
    {
        return spawnTime;
    }

    protected virtual Vector3 GetSpawnPosition()
    {
        return transform.position;
    }

    protected void SpawnOne()
    {
        int typeIndex = Random.Range(0, pools.Length);

        T item = pools[typeIndex].Get(GetSpawnPosition(), Quaternion.identity);

        OnSpawned(item);
    }

    protected virtual void OnSpawned(T item)
    {
    }

    public override void StartSpawning()
    {
        timeUntilNextSpawn = GetNextSpawnTime();
        spawning = true;
    }

    public override void StopSpawning()
    {
        spawning = false;
    }

    public override void ResetSpawner()
    {
        StopSpawning();

        foreach (ObjectPool<T> pool in pools)
        {
            pool.ReleaseAll();
        }
    }
}
