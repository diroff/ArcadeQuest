using UnityEngine;
using UnityEngine.EventSystems;

public class Creature : MonoBehaviour
{
    [SerializeField] protected float BaseMovementSpeed = 12f;
    [SerializeField] protected float BaseRotationSpeed = 15f;

    private CreatureAnimator _animator;
    private float _moveAmount;

    protected float CurrentMovementSpeed;
    protected float CurrentRotationSpeed;

    protected Vector3 MoveDirection;
    protected Rigidbody CreatureRigidbody;
    
    public Rigidbody RigidBody => CreatureRigidbody;

    protected virtual void Awake()
    {
        CreatureRigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<CreatureAnimator>();
    }

    protected virtual void Start()
    {
        CurrentMovementSpeed = BaseMovementSpeed;
        CurrentRotationSpeed = BaseRotationSpeed;
    }

    protected virtual void FixedUpdate()
    {
        HandleAllMovement();
    }

    protected void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        _animator.UpdateAnimatorValues(0, _moveAmount);
    }

    protected void HandleMovement()
    {
        Vector3 movementVelocity = MoveDirection * CurrentMovementSpeed;
        CreatureRigidbody.velocity = movementVelocity;

        if(movementVelocity ==  Vector3.zero)
            CreatureRigidbody.velocity = Vector3.zero;
    }

    protected void HandleRotation()
    {
        if (MoveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(MoveDirection);
            Quaternion creatureRotation = Quaternion.Slerp(transform.rotation, targetRotation, CurrentRotationSpeed * Time.deltaTime);
            transform.rotation = creatureRotation;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        MoveDirection = direction.normalized;
        _moveAmount = Mathf.Clamp01(Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
    }

    public void StopMoving()
    {
        MoveDirection = Vector3.zero;
        _moveAmount = 0f;
        CreatureRigidbody.velocity = Vector3.zero;
    }

    public void SetSpeed(float speed)
    {
        if (speed < 0)
            speed = 0;

        CurrentMovementSpeed = speed;
    }

    public void AddSpeed(float speed)
    {
        CurrentMovementSpeed += speed;
    }
}