using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _icon;

    private bool _isCollected = false;

    public Sprite Icon => _icon;
    public bool IsCollected => _isCollected;

    public UnityAction ItemWasCollected;
    public UnityAction<string> ItemWasCollectedWithName;

    public virtual void Interact(Player player)
    {
        Collect();
    }

    public void Collect()
    {
        if (_isCollected)
            return;

        ItemWasCollected?.Invoke();
        ItemWasCollectedWithName?.Invoke(gameObject.name);
        _isCollected = true;

        Destroy(gameObject);
    }
}