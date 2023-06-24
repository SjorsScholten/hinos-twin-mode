using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetingBehaviour : MonoBehaviour
{
    public float radius;

    private List<Targetable> _targets = new();

    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();
    }

    private void Update() {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if(mouse.rightButton.isPressed) {
            
        }

        _targets.Clear();
        var colliders = Physics2D.OverlapCircleAll(_transform.position, radius);
        for (var i = 0; i < colliders.Length; i++) {
            if (colliders[i].TryGetComponent<Targetable>(out var target)) {
                _targets.Add(target);
            }
        }
    }

    private void OnGUI() {
        var size = new Vector2(60, 30);
        var cam = Camera.main;
        foreach(var t in _targets) {
            var screenPos = cam.WorldToScreenPoint(t.position);
            screenPos.y = Screen.height - screenPos.y;
            var localPos = t.position - (Vector2)_transform.position;
            var angle = Mathf.Atan2(localPos.y, localPos.x) * Mathf.Rad2Deg;
            GUI.Label(new Rect(screenPos, size), $"Angle: {(int)angle}");
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
