using UnityEngine;
using UnityEngine.Events;

public class MetaItem : MonoBehaviour
{
    [SerializeField] private int _itemId;

    private Vector3 _startPosition;

    private bool _isOnSlot = false;

    private MetaSlotPanel _slotPanel;
    private MetaSlot _slot;

    public int ItemId => _itemId;

    public UnityAction ItemWasCollected;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        Interact();
    }

    public void Interact()
    {
        if(_isOnSlot)
            _slot.DeleteItem();
        else
            SetToSlot();
    }

    private void SetToSlot()
    {
        _slotPanel.AddItem(this);
    }

    public void AddToSlot(MetaSlot slot)
    {
        _slot = slot;
        _isOnSlot = true;
    }

    public void SetSlotPanel(MetaSlotPanel panel)
    {
        _slotPanel = panel;
    }

    public void ReturnFromSlot()
    {
        transform.position = _startPosition;
        _slot = null;
        _isOnSlot = false;
    }

    public void CollectItem()
    {
        ItemWasCollected?.Invoke();
        _slot.DeleteItem();
        Destroy(gameObject);
    }
}