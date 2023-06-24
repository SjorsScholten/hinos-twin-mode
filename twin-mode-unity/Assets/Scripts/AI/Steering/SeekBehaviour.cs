using UnityEngine;

public class SeekBehaviour : MonoBehaviour, ISteeringBehaviour
{
    public Transform target;
    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();
    }

    public SteeringData GetSteeringData(SteeringAgent agent) {
        var offset = target.position - _transform.position;
        var linear = Vector2.ClampMagnitude(offset, agent.maxSpeed);
        return new SteeringData(linear, 0);
    }
}
