using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Data/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Health")]
    [SerializeField, Min(1)] private int maxHealth = 100;

    [Header("Movement")]
    [SerializeField, Min(0f)] private float moveSpeed = 2f;

    [Header("Patrol")]
    [SerializeField, Min(0f)] private float patrolSpeed = 1f;
    [SerializeField, Min(0f)] private float patrolDistance = 3f;

    [Header("Detection")]
    [SerializeField, Min(0f)] private float detectionRange = 7f;
    [SerializeField, Min(0f)] private float loseRange = 10f;

    [Header("Shoot")]
    [SerializeField, Min(0f)] private float attackRange = 5f;
    [SerializeField, Min(0.1f)] private float shootInterval = 2f;

    [Header("Contact")]
    [SerializeField, Min(0)] private int contactDamage = 10;
    [SerializeField, Min(0.1f)] private float contactDamageInterval = 1f;

    [Header("Death")]
    [SerializeField] private ParticleSystem deathEffect;
    [SerializeField] private Pickup dropPrefab;
    [SerializeField, Range(0f, 1f)] private float dropChance = 0.3f;

    public int MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;
    public float PatrolSpeed => patrolSpeed;
    public float PatrolDistance => patrolDistance;
    public float DetectionRange => detectionRange;
    public float LoseRange => loseRange;
    public float AttackRange => attackRange;
    public float ShootInterval => shootInterval;
    public int ContactDamage => contactDamage;
    public float ContactDamageInterval => contactDamageInterval;
    public ParticleSystem DeathEffect => deathEffect;
    public Pickup DropPrefab => dropPrefab;
    public float DropChance => dropChance;
}
