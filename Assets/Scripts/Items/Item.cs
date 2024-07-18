using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private AutoGUID _guid;
    [SerializeField] private int _prefabID;

    private bool _isCollected = false;
    private BoxCollider _boxCollider;

    public Sprite Icon => _icon;
    public string GUID => _guid.GUID;
    public int PrefabID => _prefabID;

    public bool IsCollected => _isCollected;

    public UnityAction ItemWasCollected;
    public UnityAction<string> ItemWasCollectedWithName;
    public UnityAction<int> ItemWasCollectedWithID;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();

        if (_guid == null)
            _guid = gameObject.AddComponent<AutoGUID>();
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
        ItemWasCollectedWithName?.Invoke(_guid.GUID);
        ItemWasCollectedWithID?.Invoke(_prefabID);
        _isCollected = true;

        Destroy(gameObject);
    }
}