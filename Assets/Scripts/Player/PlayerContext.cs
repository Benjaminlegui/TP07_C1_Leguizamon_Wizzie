using UnityEngine;

public class PlayerContext
{
    public Rigidbody2D playerBody;
    public Animator playerAnimator;
    public PlayerSO playerData;
    public bool isGrounded;
    public float moveInput;
    public float RiseTime =>
        playerData.JumpSpeed / Mathf.Abs(Physics2D.gravity.y * playerBody.gravityScale);
    
    public void MoveHorizontal(float multiplier)
    {
        float targetX = moveInput * playerData.MoveSpeed * multiplier;

        playerBody.linearVelocity = new Vector2(
            targetX,
            playerBody.linearVelocity.y
        );
    }
}
