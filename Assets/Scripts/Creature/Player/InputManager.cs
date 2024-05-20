using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    private Vector2 _movementInput;

    private float _verticalInput;
    private float _horizontalInput;

    public float VerticalInput => _verticalInput;
    public float HorizontalInput => _horizontalInput;

    private void OnEnable()
    {
        SetupJoystick();
    }

    public void HandleMovementInput()
    {
        _verticalInput = _movementInput.y;
        _horizontalInput = _movementInput.x;
    }

    private void SetupJoystick()
    {
        _joystick.DirectionChanged += SetDirection;
    }

    private void SetDirection(Vector2 direction)
    {
        _movementInput.x = direction.x;
        _movementInput.y = direction.y;
    }
}