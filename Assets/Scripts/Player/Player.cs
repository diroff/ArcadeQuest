using UnityEngine;

public class Player : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerMovement _movement;

    public PlayerMovement Movement => _movement;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _inputManager.HandleMovementInput();
    }

    private void FixedUpdate()
    {
        _movement.HandleAllMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.gameObject.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        interactable.Interact(this);
    }
}
