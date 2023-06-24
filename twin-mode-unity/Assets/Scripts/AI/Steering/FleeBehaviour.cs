using UnityEngine;

public class FleeBehaviour : MonoBehaviour, ISteeringBehaviour
{
    public Transform target;
    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();
    }

    public SteeringData GetSteeringData(SteeringAgent agent) {
        var offset = _transform.position - target.position;
        var linear = Vector2.ClampMagnitude(offset, agent.maxSpeed);
        return new SteeringData(linear, 0);
    }
}