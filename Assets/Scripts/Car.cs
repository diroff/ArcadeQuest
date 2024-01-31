using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    [SerializeField] private float _baseMovementSpeed = 12f;

    private float _currentMovementSpeed;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;

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
            return;

        float currentRotation = transform.rotation.eulerAngles.y;

        _moveDirection = Quaternion.Euler(0f, currentRotation, 0f) * Vector3.forward;
        _moveDirection = _moveDirection * _currentMovementSpeed;

        Vector3 movementVelocity = _moveDirection;
        _rigidbody.velocity = movementVelocity;
    }

    public void LetMove(bool canMove)
    {
        _isMoving = canMove;
    }
}