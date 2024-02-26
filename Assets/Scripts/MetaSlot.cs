using System.Collections;
using UnityEngine;

public class MetaSlot : MonoBehaviour
{
    [SerializeField] private Transform _placement;
    
    private MetaItem _currentItem;

    public MetaItem CurrentItem => _currentItem;

    public void AddItem(MetaItem item)
    {
        if (IsFull())
            return;

        _currentItem = item;
        _currentItem.AddToSlot(this);
    }

    public Vector3 GetPlacementPosition()
    {
        return new Vector3(_placement.position.x, _placement.position.y + 0.5f, _placement.position.z);
    }

    public void DeleteItem()
    {
        if (!IsFull())
            return;

        if (_currentItem == null)
            return;

        if(!_currentItem.IsCollected)
            _currentItem.ReturnFromSlot();

        ClearItem();
    }

    public void ClearItem()
    {
        _currentItem = null;
    }

    public bool IsFull()
    {
        if(_currentItem == null)
            return false;
        else
            return true;
    }
}