using UnityEngine;

public class EnemyContext
{
    public Transform enemyTransform;
    public Rigidbody2D enemyBody;
    public Animator enemyAnimator;
    public EnemySO enemyData;
    public Transform firePoint;
    public Transform groundCheck;
    public ObjectPool<Projectile> projectilePool;
    public Transform target;
    public float patrolOriginX;

    private AnimationClip currentAnimation;

    public bool HasTarget => target != null;

    public float DistanceToTarget =>
        Mathf.Abs(target.position.x - enemyTransform.position.x);

    public float DirectionToTarget =>
        target.position.x >= enemyTransform.position.x ? 1f : -1f;

    public bool IsTargetInRange =>
        HasTarget && DistanceToTarget <= enemyData.AttackRange;

    public bool IsTargetDetected =>
        HasTarget && Vector2.Distance(target.position, enemyTransform.position) <= enemyData.DetectionRange;

    public bool IsTargetLost =>
        !HasTarget || Vector2.Distance(target.position, enemyTransform.position) > enemyData.LoseRange;

    public float FacingDirection =>
        enemyTransform.right.x >= 0f ? 1f : -1f;

    public bool IsGroundAhead =>
        groundCheck == null ||
        Physics2D.Raycast(groundCheck.position, Vector2.down, enemyData.GroundCheckDistance, enemyData.GroundLayer);

    public void PlayAnimation(AnimationClip clip, bool restart = false)
    {
        if (enemyAnimator == null || clip == null)
            return;

        if (clip == currentAnimation && !restart)
            return;

        currentAnimation = clip;
        enemyAnimator.Play(clip.name, 0, 0f);
    }

    public void ResetAnimation() => currentAnimation = null;

    public void FaceTarget()
    {
        if (!HasTarget)
            return;

        Face(DirectionToTarget);
    }

    public void Face(float direction)
    {
        float angleY = direction > 0f ? 0f : 180f;
        enemyTransform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }

    public void MoveHorizontal(float direction) => MoveHorizontal(direction, enemyData.MoveSpeed);

    public void MoveHorizontal(float direction, float speed)
    {
        enemyBody.linearVelocity = new Vector2(
            direction * speed,
            enemyBody.linearVelocity.y
        );
    }
}
