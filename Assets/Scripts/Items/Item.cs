using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private AutoGUID _guid;

    private bool _isCollected = false;
    private BoxCollider _boxCollider;

    public Sprite Icon => _icon;
    public bool IsCollected => _isCollected;

    public UnityAction ItemWasCollected;
    public UnityAction<string> ItemWasCollectedWithName;

    public string GUID => _guid.GUID;

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
        Debug.Log("Was collected:" + _guid.GUID);
        _isCollected = true;

        Destroy(gameObject);
    }
}