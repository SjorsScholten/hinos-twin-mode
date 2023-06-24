using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public SpawnComponent[] entities;
    public float spawnDelay;

    private WaitForSeconds _waitForSpawnDelay;
    private bool _isSpawning;
    private readonly Queue<SpawnComponent> _spawnQueue = new();

    private Transform _transform;

    private void Awake() {
        _transform = GetComponent<Transform>();

        foreach(var e in entities)
            AddToPool(e);

        _waitForSpawnDelay = new(spawnDelay);
    }

    private void Start() {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine() {
        _isSpawning = true;
        while(_spawnQueue.TryDequeue(out var enemy)) {
            yield return _waitForSpawnDelay;
            enemy.Spawn(_transform.position);
        }
        _isSpawning = false;
    }

    private void ReturnToQueue(SpawnComponent enemy) {
        _spawnQueue.Enqueue(enemy);
        
        if (!_isSpawning)
            StartCoroutine(SpawnRoutine());
    }

    public void AddToPool(SpawnComponent entitiy) {
        entitiy.OnReturnEvent += ReturnToQueue;
        _spawnQueue.Enqueue(entitiy);
    }
}
