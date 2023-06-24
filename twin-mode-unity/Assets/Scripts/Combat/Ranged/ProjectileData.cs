using UnityEngine;

[CreateAssetMenu(fileName = "new projectile", menuName = "Projectile Data")]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private float initialLifetime;
    [SerializeField] private Vector2 initialVelocity;
    [SerializeField] private float initialAngularVelocity;
    [SerializeField] private Projectile prefab;

    public Projectile Create(Vector2 position, Quaternion rotation) {
        var projectile = Instantiate<Projectile>(prefab, position, rotation);
        projectile.projectileData = this;
        projectile.velocity = initialVelocity;
        projectile.lifetime = initialLifetime;
        return projectile;
    }
}