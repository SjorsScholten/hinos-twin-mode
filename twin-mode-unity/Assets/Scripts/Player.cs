using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterSwitchBehaviour characterSwitchBehaviour;

    // Components
    private MovementBehaviour _movementBehaviour;

    private void Awake() {
        _movementBehaviour = GetComponent<MovementBehaviour>();
    }

    private void OnEnable() {
        characterSwitchBehaviour.OnCharacterChangedEvent += OnCharacterSwitch;
    }

    private void OnDisable() {
        characterSwitchBehaviour.OnCharacterChangedEvent -= OnCharacterSwitch;
    }

    private void OnCharacterSwitch(CharacterBehaviour character) {
        _movementBehaviour.maxSpeed = character.data.maxSpeed;
        _movementBehaviour.maxAcceleration = character.data.maxAcceleration;
        _movementBehaviour.maxDeceleration = character.data.maxDeceleration;
        _movementBehaviour.maxCorrection = character.data.maxCorrection;
    }
}
