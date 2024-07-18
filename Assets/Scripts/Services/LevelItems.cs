using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelItems : MonoBehaviour
{
    [SerializeField] private List<Item> _levelItems;

    private int _itemsCollectedCount = 0;

    public List<Item> Items => _levelItems;

    public UnityAction AllItemsCollected;

    private void OnEnable()
    {
        foreach (var item in _levelItems)
            item.ItemWasCollected += ItemCountChecker;
    }

    private void OnDisable()
    {
        foreach (var item in _levelItems)
            item.ItemWasCollected -= ItemCountChecker;
    }

    public void CollectAllItems()
    {
        foreach (var item in _levelItems)
        {
            if (item == null)
                continue;

            if (item.IsCollected)
                continue;

            item.Collect();
        }
    }

    private void ItemCountChecker()
    {
        _itemsCollectedCount++;
        Debug.Log($"Items:{_itemsCollectedCount}/{_levelItems.Count}");

        if (_itemsCollectedCount == _levelItems.Count)
            CompleteLevel();
    }

    public void CompleteLevel()
    {
        AllItemsCollected?.Invoke();
    }

    public Item GetNotCollectedItem()
    {
        foreach (var item in _levelItems)

            if (!item.IsCollected)
                return item;

        return null;
    }
}