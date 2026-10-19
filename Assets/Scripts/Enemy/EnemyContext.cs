using UnityEngine;

public class EnemyContext
{
    public Transform enemyTransform;
    public Rigidbody2D enemyBody;
    public EnemySO enemyData;
    public Transform firePoint;
    public ObjectPool<Projectile> projectilePool;
    public Transform target;

    public bool HasTarget => target != null;

    public float DistanceToTarget =>
        Mathf.Abs(target.position.x - enemyTransform.position.x);

    public float DirectionToTarget =>
        target.position.x >= enemyTransform.position.x ? 1f : -1f;

    public bool IsTargetInRange =>
        HasTarget && DistanceToTarget <= enemyData.AttackRange;

    public void FaceTarget()
    {
        if (!HasTarget)
            return;

        float angleY = DirectionToTarget > 0f ? 0f : 180f;
        enemyTransform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }

    public void MoveHorizontal(float direction)
    {
        enemyBody.linearVelocity = new Vector2(
            direction * enemyData.MoveSpeed,
            enemyBody.linearVelocity.y
        );
    }
}
