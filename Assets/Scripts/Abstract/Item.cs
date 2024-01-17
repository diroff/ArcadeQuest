using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _icon;

    private bool _isCollected = false;

    public Sprite Icon => _icon;
    public bool IsCollected => _isCollected;

    public UnityAction ItemWasCollected;

    public virtual void Interact(Player player)
    {
        ItemWasCollected?.Invoke();
        _isCollected = true;

        Destroy(gameObject);
    }
}