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

        Debug.Log("Item was added");
        _currentItem = item;
        _currentItem.AddToSlot(this);
        _currentItem.transform.position = new Vector3(_placement.position.x, _placement.position.y + 0.5f, _placement.position.z);
    }

    public void DeleteItem()
    {
        Debug.Log("Trying to delete item");
        if (!IsFull())
            return;

        Debug.Log("Item was deleted");
        _currentItem.ReturnFromSlot();
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