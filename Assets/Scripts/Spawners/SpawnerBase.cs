using UnityEngine;

public abstract class SpawnerBase : MonoBehaviour
{
    public abstract void StartSpawning();
    public abstract void StopSpawning();
    public abstract void ResetSpawner();
}
