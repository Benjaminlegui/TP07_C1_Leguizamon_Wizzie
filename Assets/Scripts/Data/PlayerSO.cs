using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Data/Player")]
public class PlayerSO : ScriptableObject
{
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;
    
    [Header("Configuration")]
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float airControl = 0.8f;

    [Header("Run Animation")]
    [SerializeField, Min(0.1f)] private float runClipReferenceSpeed = 5f;

    [Header("Health")]
    [SerializeField, Min(1)] private int maxHealth = 100;

    [Header("Throw")]
    [SerializeField, Min(0.01f)] private float throwFireTime = 0.5f;
    [SerializeField, Min(0.01f)] private float throwClipReleaseTime = 0.5f;
    
    [Header("Movement")]
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private KeyCode jump = KeyCode.Space;
    
    public LayerMask GroundLayer => groundLayer;
    public float GroundCheckDistance => groundCheckDistance;
    
    public float JumpSpeed => jumpSpeed;
    public float MoveSpeed => moveSpeed;
    public float AirControl => airControl;

    public float RunClipReferenceSpeed => runClipReferenceSpeed;

    public int MaxHealth => maxHealth;

    public float ThrowFireTime => throwFireTime;
    public float ThrowClipReleaseTime => throwClipReleaseTime;
    
    public KeyCode PlayerMoveLeft => moveLeft;
    public KeyCode PlayerMoveRight => moveRight;
    public KeyCode PlayerJump => jump;
}
