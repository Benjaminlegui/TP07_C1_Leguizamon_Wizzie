using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [Header("Trasnforms")]
    [SerializeField] private Transform groundCheck;

    [Header("Throw")]
    [SerializeField] private Transform throwFirePoint;
    [SerializeField] private Fireball fireballPrefab;
    [SerializeField, Min(1)] private int fireballPoolSize = 5;
    
    [Header("Scriptable Objects")]
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private PlayerAnimationsSO animations;
    
    private Rigidbody2D rb;
    private Animator animator;
    
    private PlayerContext context;
    private StateMachine<PlayerContext>  stateMachine;
    
    private PlayerStateSetup states;

    public event Action Jumped
    {
        add => context.Jumped += value;
        remove => context.Jumped -= value;
    }

    public event Action Landed
    {
        add => context.Landed += value;
        remove => context.Landed -= value;
    }

    public event Action Threw
    {
        add => context.Threw += value;
        remove => context.Threw -= value;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        GetComponent<Health>().Initialize(playerSO.MaxHealth);
        
        context = new PlayerContext
        {
            playerBody = rb,
            playerAnimator =  animator,
            playerData = playerSO,
            throwFirePoint = throwFirePoint,
            fireballPool = new ObjectPool<Fireball>(fireballPrefab, fireballPoolSize),
        };

        stateMachine = new StateMachine<PlayerContext>();
        
        states = new PlayerStateSetup(
            context,
            stateMachine,
            animations
        );

        stateMachine.Initialize(states.Grounded);
    }
    
    private void Update()
    {
        CheckGround();
        ReadMoveInput();
        stateMachine.Update();
    }
    
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    
    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            playerSO.GroundCheckDistance,
            playerSO.GroundLayer
        );

        context.isGrounded = hit.collider != null;

        // Debug.DrawRay(
        //     groundCheck.position,
        //     Vector2.down * playerSO.GroundCheckDistance,
        //     context.isGrounded ? Color.green : Color.red
        // );
    }

    private void ReadMoveInput()
    {
        float direction = 0;
        
        if (Input.GetKey(playerSO.PlayerMoveRight))
        {
            direction += 1;
        }

        if (Input.GetKey(playerSO.PlayerMoveLeft))
        {
            direction -= 1;
        }
        
        context.moveInput = direction;

        if (direction != 0f && direction != context.facingDirection)
        {
            context.facingDirection = direction;
            Face(direction);
        }
    }

    private void Face(float direction)
    {
        float angleY = direction > 0f ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }
}
