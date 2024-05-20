using UnityEngine;

public class Player : Creature
{
    private InputManager _inputManager;
    private Transform _cameraTransform;

    protected override void Awake()
    {
        base.Awake();
        _inputManager = GetComponent<InputManager>();
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        _inputManager.HandleMovementInput();
        Vector3 direction = GetInputDirection();
        SetDirection(direction);
    }

    private Vector3 GetInputDirection()
    {
        Vector3 forward = _cameraTransform.forward * _inputManager.VerticalInput;
        Vector3 right = _cameraTransform.right * _inputManager.HorizontalInput;
        Vector3 direction = forward + right;
        direction.y = 0;
        return direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.gameObject.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        interactable.Interact(this);
    }
}
