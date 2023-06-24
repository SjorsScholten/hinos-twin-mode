using UnityEngine;
using UnityEngine.InputSystem;

public class RotationBehaviour : MonoBehaviour
{
    public float rotationSpeed;

    private Vector2 _mouseInput;

    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _mouseInput = Vector2.zero;

        var mouse = Mouse.current;
        if (mouse == null) return;

        var viewPosition = (Vector2)Camera.main.WorldToScreenPoint(_rigidbody2D.position);
        _mouseInput = mouse.position.ReadValue() - viewPosition;
        _mouseInput.Normalize();
    }

    private void FixedUpdate() {
        _rigidbody2D.rotation = CalculateMoveRotation(_mouseInput, transform.right);
    }

    public float CalculateMoveRotation(Vector2 lookInput, Vector2 heading) {
        var direction = GetLookDirection(lookInput, heading);
        var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var angleChange = rotationSpeed * Time.deltaTime;
        return Mathf.MoveTowardsAngle(_rigidbody2D.rotation, targetAngle, angleChange);
    }

    public Vector2 GetLookDirection(Vector2 lookInput, Vector2 heading) {
        return (lookInput.sqrMagnitude > Mathf.Epsilon) ? lookInput : heading;
    }
}
