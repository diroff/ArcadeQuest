using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;
    public UnityAction ItemWasCollected;

    public virtual void Interact(Player player)
    {
        ItemWasCollected?.Invoke();

        Destroy(gameObject);
    }
}