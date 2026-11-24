using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Spawn
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField, Min(1)] private int damage = 25;

    private Rigidbody2D body;
    private float timeAlive;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
    }

    public override void OnGet()
    {
        timeAlive = 0f;
    }

    public override void OnRelease()
    {
        body.linearVelocity = Vector2.zero;
    }

    public void Launch(float direction)
    {
        body.linearVelocity = new Vector2(direction * speed, 0f);

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= lifetime)
            Despawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponentInParent<Health>();

        // Bug: Si lo que toca no tiene Health el proyectil sigue de largo, atraviesa paredes y suelo y puede pegarle a un enemigo detrás de una pared.
        // Hacé Despawn también al tocar la capa Ground (chequeando other.gameObject.layer contra un LayerMask serializado).
        if (health == null)
            return;

        health.TakeDamage(damage);
        Despawn();
    }
}
