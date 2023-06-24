using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProcessor : MonoBehaviour
{
    public Camera playerViewCamera;
    
    private DefaultInput _actions;
    private bool _mouseUsage = true;

    private PlayerInput _playerInput;

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();

        _actions = new DefaultInput();
        _playerInput.onControlsChanged += OnDeviceChanged;
    }

    private void OnEnable() {
        _actions.Player.Enable();
    }

    private void OnDisable() {
        _actions.Player.Disable();
    }

    public bool GetFirePressed() {
        return _actions.Player.Fire.IsPressed();
    }

    public Vector2 GetMoveInput() {
        return _actions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetLookInput(Vector2 position) {
        var lookInput = _actions.Player.Look.ReadValue<Vector2>();

        if (_mouseUsage) {
            var viewPosition = (Vector2)playerViewCamera.WorldToScreenPoint(position);
            lookInput -= viewPosition;
            lookInput.Normalize();
        }

        return lookInput;
    }

    public void OnDeviceChanged(PlayerInput input) {
        var isMouseScheme = _playerInput.currentControlScheme.ToUpper().Contains("MOUSE");
        if (_mouseUsage != isMouseScheme) {
            _mouseUsage = isMouseScheme;
            Cursor.visible = _mouseUsage;
            Cursor.lockState = (_mouseUsage) ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}