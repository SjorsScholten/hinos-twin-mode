using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitchBehaviour : MonoBehaviour
{
    public CharacterBehaviour a, b;

    private CharacterBehaviour _current;

    public CharacterBehaviour Current { 
        get => _current; 
        set => SetCurrentCharacter(value); 
    }

    public event Action<CharacterBehaviour> OnCharacterChangedEvent;

    private void Start() {
        SetCurrentCharacter(a);
    }

    private void Update() {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if(keyboard.qKey.wasPressedThisFrame) {
            if (_current == a) SetCurrentCharacter(b);
            else SetCurrentCharacter(a);
        }

        if (keyboard.digit0Key.wasPressedThisFrame) {
            SetCurrentCharacter(null);
        }

        if (keyboard.digit1Key.wasPressedThisFrame) {
            SetCurrentCharacter(a);
        }

        if(keyboard.digit2Key.wasPressedThisFrame) {
            SetCurrentCharacter(b);
        }
    }

    public void SetCurrentCharacter(CharacterBehaviour character) {
        if (_current == character) return;
        if (_current != null) {
            _current.isEnabled = false;
        }
        _current = character;
        _current.isEnabled = true;
        OnCharacterChangedEvent?.Invoke(character);
    }
}
