using UnityEngine;

public class Targetable : MonoBehaviour
{
    public Vector2 position { get; private set; } = Vector2.zero;

    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();
    }

    private void Update() {
        position = _transform.position;
    }
}
