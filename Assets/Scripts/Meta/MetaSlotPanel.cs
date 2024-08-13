using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        var pairs = _slots
                .Select(slot => slot.CurrentItem)
                .Where(currentItem => currentItem != null && currentItem.PrefabID == item.PrefabID)
                .ToList();

        int currentPairCount = pairs.Count;

        if (currentPairCount < _countToPaired)
        {
            if (IsFull())
                StartCoroutine(ClearSlotsAfterAnimation());

            return;
        }

        foreach (var pair in pairs)
            pair.DisablePhysic();

        StartCoroutine(CollectItems(pairs));
    }

    private IEnumerator ClearSlotsAfterAnimation()
    {
        var items = GetItems();

        yield return new WaitUntil(() => items.All(item => item.Animation.IsCollectAnimationFinished));

        ClearSlots();
    }

    private IEnumerator CollectItems(List<MetaItem> items)
    {
        yield return new WaitUntil(() => items.All(item => item.Animation.IsCollectAnimationFinished));

        foreach (var item in items)
        {
            if (!item.IsLevelGoal)
                item.CollectImmediately();
            else
                item.Collect();
        }
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

    private List<MetaItem> GetItems() 
    {
        List<MetaItem> currentItems = new List<MetaItem>();

        foreach (var slot in _slots)
        {
            currentItems.Add(slot.CurrentItem);
        }

        return currentItems;
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

    private void ClearSlots()
    {
        foreach (MetaSlot slot in _slots)
        {
            if (slot.IsFull())
                slot.DeleteItem();
        }
    }
}