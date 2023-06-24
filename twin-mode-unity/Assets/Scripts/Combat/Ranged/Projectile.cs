using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData projectileData;
    public float damage;
    public float hitRadius;
    public float lifetime;
    public Vector2 velocity;

    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();
    }

    private void Update() {
        // Process Collision
        var dist = velocity.magnitude * Time.deltaTime;
        if(CheckForCollision(_transform.position, _transform.right, dist, out var hitInfo)) {
            if (hitInfo.collider.TryGetComponent<IDamageHandler>(out var handler)) {
                handler.HandleDamage(damage);
            }

            Destroy(this.gameObject);
        }

        // Process lifetime
        lifetime -= Time.deltaTime;
        if(lifetime < 0) {
            Destroy(this.gameObject);
        }

        // Move projectile
        var finalVelocity = _transform.right * (velocity.x * Time.deltaTime);
        _transform.position += finalVelocity;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }

    private bool CheckForCollision(Vector2 position, Vector2 direction, float distance, out RaycastHit2D hitInfo) {
        hitInfo = Physics2D.CircleCast(position, hitRadius, direction, distance);
        return hitInfo.collider != null && !hitInfo.collider.CompareTag(gameObject.tag);
    }
}
