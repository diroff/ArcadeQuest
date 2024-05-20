using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] protected float baseMovementSpeed = 12f;
    [SerializeField] protected float baseRotationSpeed = 15f;

    protected float currentMovementSpeed;
    protected float currentRotationSpeed;

    protected InputManager inputManager;
    protected Vector3 moveDirection;
    protected Transform cameraObject;
    protected Rigidbody creatureRigidbody;

    public Rigidbody RigidBody => creatureRigidbody;

    protected virtual void Awake()
    {
        inputManager = GetComponent<InputManager>();
        creatureRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    protected virtual void Start()
    {
        currentMovementSpeed = baseMovementSpeed;
        currentRotationSpeed = baseRotationSpeed;
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
        moveDirection = cameraObject.forward * inputManager.VerticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.HorizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= currentMovementSpeed;

        Vector3 movementVelocity = moveDirection;
        creatureRigidbody.velocity = movementVelocity;
    }

    protected void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.VerticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.HorizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion creatureRotation = Quaternion.Slerp(transform.rotation, targetRotation, currentRotationSpeed * Time.deltaTime);

        transform.rotation = creatureRotation;
    }

    public void SetSpeed(float speed)
    {
        if (speed < 0)
            speed = 0;

        currentMovementSpeed = speed;
    }

    public void AddSpeed(float speed)
    {
        currentMovementSpeed += speed;
    }
}