using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _baseMovementSpeed = 12f;
    [SerializeField] private float _baseRotationSpeed = 15f;

    private float _currentMovementSpeed;
    private float _currentRotationSpeed;

    private InputManager _inputManager;

    private Vector3 _moveDirection;
    private Transform _cameraObject;
    private Rigidbody _playerRigidbody;

    public float CurrentMovementSpeed => _currentMovementSpeed;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _cameraObject = Camera.main.transform;
    }

    private void Start()
    {
        _currentMovementSpeed = _baseMovementSpeed;
        _currentRotationSpeed = _baseRotationSpeed;
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
        _moveDirection = _moveDirection * _currentMovementSpeed;

        Vector3 movementVelocity = _moveDirection;
        _playerRigidbody.velocity = movementVelocity;
    }

    public void SetSpeed(float speed)
    {
        if (speed < 0)
            speed = 0;

        _currentMovementSpeed = speed;
    }

    public void AddSpeed(float speed)
    {
        _currentMovementSpeed += speed;
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
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, _currentRotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}