using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float maxAcceleration;
    private Vector2 _heading, _crossHeading;

    public Camera playerViewCamera;
    public PlayerInput _playerInput;
    private DefaultInput _actions;
    private Vector2 _moveInputDirection;
    private float _moveInputAmount;
    private bool _mouseUsage = true;

    private Transform _transform;
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _actions = new DefaultInput();
        _playerInput.onControlsChanged += OnDeviceChanged;
    }

    private void OnEnable() {
        _actions.Player.Enable();
    }

    private void OnDisable() {
        _actions.Player.Disable();
    }

    private void FixedUpdate() {
        // Process Move Input
        _moveInputDirection = _actions.Player.Move.ReadValue<Vector2>();
        _moveInputAmount = _moveInputDirection.magnitude;
        SetHeading((_moveInputAmount > 0.01f) ? _moveInputDirection : _transform.right);

        // Process Movement
        var speed = maxSpeed * _moveInputAmount;
        var speedChange = maxAcceleration * Time.deltaTime;
        var forwardVelocityChange = CalculateVelocityChange(_heading, speed, speedChange);
        var velocityCorrection = CalculateVelocityChange(_crossHeading, 0, speedChange);
        _rigidbody2D.velocity += forwardVelocityChange + velocityCorrection;

        // Process Look Input
        var lookInput = _actions.Player.Look.ReadValue<Vector2>();
        if (_mouseUsage) lookInput = TransformViewToLocal(lookInput);

        // Process Rotation
        var localPoint = (lookInput.sqrMagnitude > 0.01f) ? lookInput : _heading;
        var angle = Mathf.Atan2(localPoint.y, localPoint.x) * Mathf.Rad2Deg;
        _rigidbody2D.rotation = Mathf.MoveTowardsAngle(_rigidbody2D.rotation, angle, 360f * Time.deltaTime);
    }

    private void OnDrawGizmos() {
        DrawRayFromPosition(transform.right, Color.red);
        DrawRayFromPosition(transform.up, Color.green);

        void DrawRayFromPosition(Vector3 direction, Color color) {
            Gizmos.color = color;
            Gizmos.DrawRay(transform.position, direction);
        }
    }

    private void SetHeading(Vector2 direction) {
        _heading.Set(direction.x, direction.y);
        _heading.Normalize();
        _crossHeading.Set(_heading.y, -_heading.x);
    }

    private Vector2 CalculateVelocityChange(Vector2 direction, float speed, float speedChange) {
        var alignedSpeed = Vector2.Dot(_rigidbody2D.velocity, direction);
        var finalSpeed = Mathf.MoveTowards(alignedSpeed, speed, speedChange);
        return direction * (finalSpeed - alignedSpeed);
    }

    private Vector3 TransformViewToLocal(Vector2 point) {
        var viewPosition = playerViewCamera.WorldToScreenPoint(_transform.position);
        point.x -= viewPosition.x;
        point.y -= viewPosition.y;
        return point.normalized;
    }

    private void SetMouseActive(bool value) {
        _mouseUsage = value;
        
        if (_mouseUsage) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnDeviceChanged(PlayerInput input) {
        var isMouseScheme = _playerInput.currentControlScheme.ToUpper().Contains("MOUSE");
        if (_mouseUsage != isMouseScheme) SetMouseActive(isMouseScheme);
    }
}
