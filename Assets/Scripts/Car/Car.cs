using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    [SerializeField] private float _baseMovementSpeed = 12f;

    [SerializeField] private bool _isIndependent = false;

    private float _currentMovementSpeed;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private Vector3 _movementVelocity;

    private CarSpawner _spawner;

    private bool _isMoving = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentMovementSpeed = _baseMovementSpeed;
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

    public void LetMove(bool canMove)
    {
        _isMoving = canMove;

        if (_isIndependent)
            return;

        if (_isMoving)
            _spawner.SetTrafficState(true);
        else
            _spawner.SetTrafficState(false);
    }

    public void SetSpawner(CarSpawner spawner)
    {
        _spawner = spawner;
    }
}