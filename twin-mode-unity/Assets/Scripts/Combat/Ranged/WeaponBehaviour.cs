using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponBehaviour : MonoBehaviour
{
    public float timeBetweenShots;
    public Transform weaponOrigin;
    public ProjectileData projectileData;

    private float _timeSinceLastShot;

    public Rigidbody2D _rigidbody2D;

    private void Update() {
        _timeSinceLastShot += Time.deltaTime;

        var mouse = Mouse.current;
        if (mouse == null) return;

        if(mouse.leftButton.isPressed) {
            if(_timeSinceLastShot > timeBetweenShots) {
                var projectile = projectileData.Create(weaponOrigin.position, weaponOrigin.rotation);
                projectile.tag = this.gameObject.tag;
                projectile.velocity += _rigidbody2D.velocity;
                _timeSinceLastShot = 0;
            }
        }
    }
}