using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
    [SerializeField] private int _prefabID;

    private bool _isCollected = false;
    private BoxCollider _boxCollider;

    public Sprite Icon => _icon;
    public int ID => _id;
    public int PrefabID => _prefabID;

    public bool IsCollected => _isCollected;

    public UnityAction ItemWasCollected;
    public UnityAction<int> ItemWasCollectedWithID;

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
        ItemWasCollectedWithID?.Invoke(_id);
        _isCollected = true;

        Destroy(gameObject);
    }
}