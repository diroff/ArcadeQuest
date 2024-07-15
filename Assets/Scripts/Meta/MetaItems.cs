using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MetaItems : MonoBehaviour
{
    [SerializeField] private List<MetaItem> _items;
    [SerializeField] private MetaSlotPanel _slotPanel;

    private int _itemsCollected;

    public UnityAction LevelWasCompleted;

    public List<MetaItem> Items => _items;

    private void Awake()
    {
        foreach (var item in _items)
            item.SetSlotPanel(_slotPanel);
    }

    private void OnEnable()
    {
        foreach (var item in _items)
            item.ItemWasDestroyed += OnItemWasCollected;
    }

    private void OnDisable()
    {
        foreach (var item in _items)
            item.ItemWasDestroyed -= OnItemWasCollected;
    }

    private void OnItemWasCollected()
    {
        _itemsCollected++;

        ItemCountChecker();
    }

    private void ItemCountChecker()
    {
        if (_itemsCollected != _items.Count)
            return;

        LevelCompleted();
    }

    private void LevelCompleted()
    {
        LevelWasCompleted?.Invoke();
    }
}