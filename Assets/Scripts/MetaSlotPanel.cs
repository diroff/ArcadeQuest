using System.Collections.Generic;
using UnityEngine;

public class MetaSlotPanel : MonoBehaviour
{
    [SerializeField] private List<MetaSlot> _slots;
    [SerializeField] private int _countToPaired;

    public void AddItem(MetaItem item)
    {
        if (IsFull())
            return;

        GetFreeSlot().AddItem(item);
        FindPairs(item);
    }

    private void FindPairs(MetaItem item)
    {
        int currentPairCount = 0;
        List<MetaItem> pairs = new List<MetaItem>();    

        foreach (var pair in _slots)
        {
            if (pair.CurrentItem == null)
                continue;

            if (pair.CurrentItem.ItemId != item.ItemId)
                continue;

            currentPairCount++;
            pairs.Add(pair.CurrentItem);
        }

        if (currentPairCount != _countToPaired)
            return;

        foreach (var pair in pairs)
            pair.CollectItem();
    }

    private MetaSlot GetFreeSlot()
    {
        MetaSlot slot = null;

        foreach (var item in _slots)
        {
            if (item.IsFull())
                continue;

            slot = item;
            break;
        }

        return slot;
    }

    private bool IsFull()
    {
        int countSlots = _slots.Count;
        int fullSlots = 0;

        foreach (MetaSlot slot in _slots)
        {
            if(slot.IsFull())
                fullSlots++;
        }

        return fullSlots == countSlots;
    }
}
