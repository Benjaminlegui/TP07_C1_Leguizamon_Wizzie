using System;
using UnityEngine;

public class PlayerContext
{
    public Rigidbody2D playerBody;
    public Animator playerAnimator;
    public PlayerSO playerData;
    public Transform throwFirePoint;
    public ObjectPool<Fireball> fireballPool;
    public bool isGrounded;
    public float moveInput;
    public float facingDirection = 1f;

    public event Action Jumped;
    public event Action Landed;
    public event Action Threw;

    public void RaiseJumped() => Jumped?.Invoke();
    public void RaiseLanded() => Landed?.Invoke();
    public void RaiseThrew() => Threw?.Invoke();
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
