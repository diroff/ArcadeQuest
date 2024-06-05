using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour, IMoveableController
{
    [SerializeField] private float _baseMovementSpeed = 12f;

    private float _currentMovementSpeed;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private Vector3 _movementVelocity;

    private CarSpawner _spawner;

    private bool _isMoving = true;
    private bool _isIndependent = false;

    public UnityAction<Car> CarWasDestroyed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentMovementSpeed = _baseMovementSpeed;
    }

    private void OnEnable()
    {
        if (_spawner == null)
            _isIndependent = true;
        else
            _isIndependent = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!_isMoving)
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, 0.1f);
            return;
        }

        float currentRotation = transform.rotation.eulerAngles.y;

        _moveDirection = Quaternion.Euler(0f, currentRotation, 0f) * Vector3.forward;
        _moveDirection = _moveDirection * _currentMovementSpeed;

         _movementVelocity = _moveDirection;
        _rigidbody.velocity = _movementVelocity;
    }

    public void DestroyCar()
    {
        CarWasDestroyed?.Invoke(this);

        if (_isIndependent)
            Destroy(gameObject);
    }

    public void SetSpawner(CarSpawner spawner)
    {
        _spawner = spawner;
    }

    public void StartMove()
    {
        _isMoving = true;

        _spawner?.SetTrafficState(true);
    }

    public void StopMove()
    {
        _isMoving = false;

        _spawner?.SetTrafficState(false);
    }
}