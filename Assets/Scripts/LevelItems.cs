using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelItems : MonoBehaviour
{
    [SerializeField] private List<Item> _levelItems;

    private int _itemsCollectedCount;

    public List<Item> Items => _levelItems;

    public UnityAction AllItemsCollected;

    private void OnEnable()
    {
        foreach (var item in _levelItems)
        {
            item.ItemWasCollected += ItemCountChecker;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _levelItems)
        {
            item.ItemWasCollected -= ItemCountChecker;
        }
    }

    private void ItemCountChecker()
    {
        _itemsCollectedCount++;
        Debug.Log($"Items:{_itemsCollectedCount}/{_levelItems.Count}");

        if (_itemsCollectedCount == _levelItems.Count)
            AllItemsCollected?.Invoke();
    }
}