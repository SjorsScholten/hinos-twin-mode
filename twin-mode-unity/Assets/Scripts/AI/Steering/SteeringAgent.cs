using System;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    public float maxSpeed;

    public Vector2 Velocity { get => _rigidbody2D.velocity; }

    private Rigidbody2D _rigidbody2D;
    private readonly List<SteeringBehaviour> _steeringBehaviours = new();

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _steeringBehaviours.AddRange(GetComponents<SteeringBehaviour>());
    }

    private void FixedUpdate() {
        var steering = SteeringData.Empty;

        foreach(var sb in _steeringBehaviours) {
            var data = sb.GetSteeringData(this);
            steering.linear += data.linear;
            steering.angular += data.angular;
        }

        steering.linear = Vector2.ClampMagnitude(steering.linear, maxSpeed);

        if(steering.angular > float.Epsilon) {

        }
    }
}

public abstract class SteeringBehaviour : MonoBehaviour
{
    public abstract SteeringData GetSteeringData(SteeringAgent agent);
}

public interface ISteeringBehaviour
{
    SteeringData GetSteeringData(SteeringAgent agent);
}

public class Arrive_SteeringBehaviour : MonoBehaviour, ISteeringBehaviour
{
    public Transform target;
    public float targetRadius;
    public float slowRadius;

    private Transform _transform;
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public SteeringData GetSteeringData(SteeringAgent agent) {
        var offset = target.position - _transform.position;
        var distance = offset.magnitude;

        if(distance < targetRadius) {
            return SteeringData.Empty;
        }

        var speed = agent.maxSpeed;
        if (distance < slowRadius) {
            speed *= (distance / slowRadius);
        }

        var linear = offset.normalized * speed;
        //linear -= _rigidbody2D.velocity;

        return new SteeringData(linear, 0);
    }
}

public class Pursuit_SteeringBehaviour : MonoBehaviour, ISteeringBehaviour
{
    public Transform target;
    private Vector2 lastTargetPosition;

    private Transform _transform;
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _transform = GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        lastTargetPosition = target.position;
    }

    public SteeringData GetSteeringData(SteeringAgent agent) {

        var offset = target.position - _transform.position;
        var distance = offset.magnitude;
        var speed = _rigidbody2D.velocity.magnitude;

        return SteeringData.Empty;
    }
}