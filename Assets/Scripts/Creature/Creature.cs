using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] protected float BaseMovementSpeed = 12f;
    [SerializeField] protected float BaseRotationSpeed = 15f;

    protected float CurrentMovementSpeed;
    protected float CurrentRotationSpeed;

    protected Vector3 moveDirection;
    protected Rigidbody creatureRigidbody;

    public Rigidbody RigidBody => creatureRigidbody;

    protected virtual void Awake()
    {
        creatureRigidbody = GetComponent<Rigidbody>();
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
    }

    protected void HandleMovement()
    {
        Vector3 movementVelocity = moveDirection * CurrentMovementSpeed;
        creatureRigidbody.velocity = movementVelocity;
    }

    protected void HandleRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            Quaternion creatureRotation = Quaternion.Slerp(transform.rotation, targetRotation, CurrentRotationSpeed * Time.deltaTime);
            transform.rotation = creatureRotation;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;
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