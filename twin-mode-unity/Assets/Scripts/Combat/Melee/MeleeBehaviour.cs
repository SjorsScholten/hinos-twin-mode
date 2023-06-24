using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeBehaviour : MonoBehaviour
{
    public Transform attackOrigin;
    public float attackRadius;
    public float damage;

    private void Update() {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if(mouse.rightButton.wasPressedThisFrame) {
            var colliders = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius);
            for(var i = 0; i < colliders.Length; i++) {
                if (colliders[i].TryGetComponent<IDamageHandler>(out var handler)) {
                    handler.HandleDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }
}
