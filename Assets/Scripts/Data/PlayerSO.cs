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
    
    [Header("Movement")]
    [SerializeField] private KeyCode moveUp = KeyCode.W;
    [SerializeField] private KeyCode moveDown = KeyCode.S;
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveRight = KeyCode.D;
    [SerializeField] private KeyCode jump = KeyCode.Space;
    
    public LayerMask GroundLayer => groundLayer;
    public float GroundCheckDistance => groundCheckDistance;
    
    public float JumpSpeed => jumpSpeed;
    public float MoveSpeed => moveSpeed;
    public float AirControl => airControl;
    
    public KeyCode PlayerMoveUp => moveUp;
    public KeyCode PlayerMoveDown => moveDown;
    public KeyCode PlayerMoveLeft => moveLeft;
    public KeyCode PlayerMoveRight => moveRight;
    public KeyCode PlayerJump => jump;
}
