using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool _isJoystickInput;
    [SerializeField] private Joystick _joystick;

    private PlayerControls _playerControls;
    private PlayerAnimator _animator;

    private Vector2 _movementInput;

    private float _moveAmount;
    private float _verticalInput;
    private float _horizontalInput;

    public float VerticalInput => _verticalInput;
    public float HorizontalInput => _horizontalInput;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        if(_isJoystickInput)
            SetupJoystick();
        else
            SetupInput();
    }

    private void OnDisable()
    {
        if (!_isJoystickInput)
            _playerControls.Disable();
        else
            _joystick.DirectionChanged -= SetDirection;
    }

    public void HandleMovementInput()
    {
        _verticalInput = _movementInput.y;
        _horizontalInput = _movementInput.x;
        _moveAmount = Mathf.Clamp01(Mathf.Abs(_horizontalInput) + Mathf.Abs(_verticalInput));
        _animator.UpdateAnimatorValues(0, _moveAmount);
    }

    private void SetupInput()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            _playerControls.PlayerMovement.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();
        }

        _playerControls.Enable();
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