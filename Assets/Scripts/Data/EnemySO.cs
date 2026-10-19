using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Data/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Health")]
    [SerializeField, Min(1)] private int maxHealth = 100;

    [Header("Movement")]
    [SerializeField, Min(0f)] private float moveSpeed = 2f;

    [Header("Shoot")]
    [SerializeField, Min(0f)] private float attackRange = 5f;
    [SerializeField, Min(0.1f)] private float shootInterval = 2f;

    public int MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;
    public float AttackRange => attackRange;
    public float ShootInterval => shootInterval;
}
