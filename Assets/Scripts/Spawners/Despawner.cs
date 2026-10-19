using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spawn spawn = collision.GetComponentInParent<Spawn>();

        if (spawn != null)
        {
            spawn.Despawn();
        }
    }
}
