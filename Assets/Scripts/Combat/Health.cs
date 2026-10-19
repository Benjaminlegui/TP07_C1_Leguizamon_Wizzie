using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;
    public event Action Changed;
    public event Action Died;

    public void Initialize(int max)
    {
        maxHealth = max;
        currentHealth = maxHealth;
        Changed?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        if (IsDead || amount <= 0) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);
        Changed?.Invoke();

        if (IsDead) Died?.Invoke();
    }

    [ContextMenu("Take 10 Damage")]
    private void TakeTestDamage() => TakeDamage(10);
}
