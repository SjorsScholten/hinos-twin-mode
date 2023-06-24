using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringComponent : MonoBehaviour
{
    public float maxSpeed;
    public float maxAcceleration;
    public Rigidbody2D target;
    public float range;

    private Vector2 _velocity;

    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        _velocity = _rigidbody2D.velocity;

        var diff = target.position - _rigidbody2D.position;
        var dist = diff.magnitude;
        var dir = diff.normalized;

        var scalar = (dist > range) ? 1 : -1;

        var speed = maxSpeed * (dist / range);
        speed = Mathf.Min(maxSpeed, speed);

        var targetDirection = dir * scalar;
        var alignedSpeed = Vector2.Dot(_velocity, targetDirection);
        var finalSpeed = Mathf.MoveTowards(alignedSpeed, speed, maxAcceleration * Time.deltaTime);
        var drivingVelocity = targetDirection * (finalSpeed - alignedSpeed);

        var crossHeading = new Vector2(targetDirection.y, -targetDirection.x);
        var crossAlignedSpeed = Vector2.Dot(_velocity, crossHeading);
        var crossFinalSpeed = Mathf.MoveTowards(crossAlignedSpeed, 0, maxAcceleration * Time.deltaTime);
        var correctionVelocity = crossHeading * (crossFinalSpeed - crossAlignedSpeed);


        _rigidbody2D.velocity += drivingVelocity + correctionVelocity;
    }
}
