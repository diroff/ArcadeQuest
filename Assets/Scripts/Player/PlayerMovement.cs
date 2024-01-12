using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 15f;

    private InputManager _inputManager;

    private Vector3 _moveDirection;
    private Transform _cameraObject;
    private Rigidbody _playerRigidbody;

    public float MovementSpeed => _movementSpeed;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        _moveDirection = _cameraObject.forward * _inputManager.VerticalInput;
        _moveDirection = _moveDirection + _cameraObject.right * _inputManager.HorizontalInput;
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        _moveDirection = _moveDirection * _movementSpeed;

        Vector3 movementVelocity = _moveDirection;
        _playerRigidbody.velocity = movementVelocity;
    }

    public void SetSpeed(float speed)
    {
        if (speed < 0)
            speed = 0;

        _movementSpeed = speed;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = _cameraObject.forward * _inputManager.VerticalInput;
        targetDirection = targetDirection + _cameraObject.right * _inputManager.HorizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
