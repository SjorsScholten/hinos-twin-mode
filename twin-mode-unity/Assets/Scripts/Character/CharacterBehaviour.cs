using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public CharacterData data;
    public bool isEnabled;
    public Transform body;
    public float range;

    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();
    }

    private void LateUpdate() {
        if (isEnabled) {
            _transform.position = body.position;
        } else {
            var err = body.position - _transform.position;
            if(err.sqrMagnitude > range * range) {
                _transform.position = Vector2.MoveTowards(_transform.position, body.position + err.normalized * range, data.maxAcceleration * Time.deltaTime);
            }
        }
    }
}
