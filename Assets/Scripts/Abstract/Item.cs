using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _icon;

    private bool _isCollected = false;

    public Sprite Icon => _icon;
    public bool IsCollected => _isCollected;

    public UnityAction ItemWasCollected;
    public UnityAction<string> ItemWasCollectedWithName;

    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public virtual void Interact(Player player)
    {
        Collect();
    }

    [ContextMenu("Collect")]
    public void Collect()
    {
        _boxCollider.enabled = false;

        ItemWasCollected?.Invoke();
        ItemWasCollectedWithName?.Invoke(gameObject.name);
        _isCollected = true;

        Destroy(gameObject);
    }
}