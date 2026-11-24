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

        // Error: PickedUp no tiene ningún suscriptor en el proyecto, el evento se dispara y no suena nada al agarrar el pickable.
        // Suscribí AudioGameplay a Pickup.PickedUp (y desuscribí en OnDestroy) con un clip propio.
        PickedUp?.Invoke();
        Destroy(gameObject);
    }

    protected abstract bool TryApply(Player player);
}
