using UnityEngine;

[DefaultExecutionOrder(1)]
public class SpawnComponent : MonoBehaviour
{
    public bool disableOnAwake = true;

    private Transform _transform;

    public event System.Action OnSpawnEvent;
    public event System.Action<SpawnComponent> OnReturnEvent;

    private void Awake() {
        _transform = GetComponent<Transform>();

        this.gameObject.SetActive(!disableOnAwake);
    }

    public void Spawn(Vector2 position) {
        _transform.position = position;
        OnSpawnEvent?.Invoke();
        this.gameObject.SetActive(true);
    }

    public void Return() {
        this.gameObject.SetActive(false);
        OnReturnEvent?.Invoke(this);
    }
}
