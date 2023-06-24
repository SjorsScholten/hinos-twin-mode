using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Transform playerSpawn;
    public Camera mainCamera;

    private void Awake() {
        SpawnPlayer();
    }

    private void SpawnPlayer() {
        var go = new GameObject("Player");

        // Setup Input Component
        var input = go.AddComponent<PlayerInputProcessor>();
        input.playerViewCamera = mainCamera;

        var controller = go.AddComponent<PlayerController>();
    }
}
