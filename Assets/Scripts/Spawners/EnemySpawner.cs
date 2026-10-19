using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] spawnPoints;

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
