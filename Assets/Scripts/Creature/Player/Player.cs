using UnityEngine;

public class Player : Creature
{
    private void Update()
    {
        inputManager.HandleMovementInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.gameObject.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        interactable.Interact(this);
    }
}
