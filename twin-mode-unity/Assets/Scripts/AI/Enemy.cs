using UnityEngine;

public class Enemy : MonoBehaviour, IDamageHandler
{
    public float initialHealth;
    public float speed;

    private float _health;
    private bool _isDead;

    private Rigidbody2D _rigidbody2D;
    private SpawnComponent _spawnComponent;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spawnComponent = GetComponent<SpawnComponent>();

        _spawnComponent.OnSpawnEvent += OnSpawn;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        _rigidbody2D.velocity = Vector2.Reflect(_rigidbody2D.velocity, collision.contacts[0].normal);
    }

    private void OnSpawn() {
        _health = initialHealth;
        _isDead = false;

        _rigidbody2D.rotation = Random.Range(0, 360);
    }

    public void HandleDamage(float damage) {
        if (_isDead) return;

        _health -= damage;

        if(_health <= 0) {
            _isDead = true;
            _spawnComponent.Return();
            this.gameObject.SetActive(false);
        }
    }
}
