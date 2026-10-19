using System;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public static event Action PickedUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player == null || !TryApply(player))
            return;

        PickedUp?.Invoke();
        Destroy(gameObject);
    }

    protected abstract bool TryApply(Player player);
}
