using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    
    [Header("Scriptable Objects")]
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private PlayerAnimationsSO animations;

    private Rigidbody2D rb;
    private Animator animator;
    
    private PlayerContext context;
    private StateMachine<PlayerContext>  stateMachine;
    
    private PlayerStateSetup states;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        
        context = new PlayerContext
        {
            playerBody = rb,
            playerAnimator =  animator,
            playerData = playerSO,
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
    }
}
