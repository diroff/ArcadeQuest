using UnityEngine;

public class Creature : MonoBehaviour, IMoveableController
{
    [SerializeField] protected float BaseMovementSpeed = 12f;
    [SerializeField] protected float BaseRotationSpeed = 15f;
    [SerializeField] private float Acceleration = 5f;
    [SerializeField] private float Deceleration = 5f;

    protected CreatureAnimator Animator;

    protected float CurrentMovementSpeed;
    protected float CurrentRotationSpeed;

    protected Vector3 MoveDirection;
    protected Vector3 SmoothMoveDirection;
    protected Rigidbody CreatureRigidbody;

    private float _moveAmount;

    public Rigidbody RigidBody => CreatureRigidbody;

    protected virtual void Awake()
    {
        CreatureRigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<CreatureAnimator>();
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
        Animator.UpdateAnimatorValues(0, _moveAmount);
    }

    protected void HandleMovement()
    {
        SmoothMoveDirection = Vector3.MoveTowards(SmoothMoveDirection, MoveDirection, Time.deltaTime * (MoveDirection != Vector3.zero ? Acceleration : Deceleration));

        Vector3 movementVelocity = SmoothMoveDirection * CurrentMovementSpeed;
        CreatureRigidbody.velocity = movementVelocity;

        _moveAmount = SmoothMoveDirection.magnitude;
    }

    protected void HandleRotation()
    {
        if (SmoothMoveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(SmoothMoveDirection);
            Quaternion creatureRotation = Quaternion.Slerp(transform.rotation, targetRotation, CurrentRotationSpeed * Time.deltaTime);
            transform.rotation = creatureRotation;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        MoveDirection = direction.normalized;
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
        CurrentRotationSpeed += speed;
    }

    public virtual void StartMove()
    {
        SetDirection(MoveDirection);
    }

    public virtual void StopMove()
    {
        ResetVelocity();
    }

    protected void ResetVelocity()
    {
        _moveAmount = 0f;
        MoveDirection = Vector3.zero;
        SmoothMoveDirection = Vector3.zero;
        CreatureRigidbody.velocity = Vector3.zero;
    }
}