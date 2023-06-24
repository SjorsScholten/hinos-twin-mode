using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBehaviour : MonoBehaviour
{
    public float maxSpeed;
    public float maxAcceleration;
    public float maxDeceleration;
    public float maxCorrection;

    private Vector2 _heading, _crossHeading;
    private Vector2 _moveInput;

    private Transform _transform;
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _moveInput = Vector2.zero;

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.wKey.isPressed) _moveInput.y += 1;
        if (keyboard.sKey.isPressed) _moveInput.y -= 1;
        if (keyboard.dKey.isPressed) _moveInput.x += 1;
        if (keyboard.aKey.isPressed) _moveInput.x -= 1;
    }

    private void FixedUpdate() {
        UpdateHeading();
        var moveVeclocity = CalculateMoveVelocity();
        _rigidbody2D.velocity += moveVeclocity;
    }

    private void UpdateHeading() {
        if (_moveInput.sqrMagnitude > Mathf.Epsilon) {
            _heading = _moveInput.normalized;
            _crossHeading = VectorExtensions.Cross(_heading);
        }
    }

    private Vector2 CalculateMoveVelocity() {
        var speed = maxSpeed * _moveInput.magnitude;
        var speedChange = (_moveInput.sqrMagnitude > float.Epsilon) ? maxAcceleration : maxDeceleration;
        var forwardVelocityChange = CalculateVelocityChange(_rigidbody2D.velocity, _heading, speed, speedChange * Time.deltaTime);
        var velocityCorrection = CalculateVelocityChange(_rigidbody2D.velocity, _crossHeading, 0, maxCorrection * Time.deltaTime);
        return forwardVelocityChange + velocityCorrection;
    }

    public static Vector2 CalculateVelocityChange(Vector2 velocity, Vector2 direction, float speed, float speedChange) {
        var alignedSpeed = Vector2.Dot(velocity, direction);
        var finalSpeed = Mathf.MoveTowards(alignedSpeed, speed, speedChange);
        return direction * (finalSpeed - alignedSpeed);
    }
}
