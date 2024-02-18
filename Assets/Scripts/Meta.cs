using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Meta : MonoBehaviour
{
    [SerializeField] private List<MetaItem> _items;
    [SerializeField] private MetaSlotPanel _slotPanel;

    private int _itemsCollected;

    public UnityAction LevelWasCompleted;

    private void Awake()
    {
        foreach (var item in _items)
            item.SetSlotPanel(_slotPanel);
    }

    private void OnEnable()
    {
        foreach (var item in _items)
            item.ItemWasCollected += OnItemWasCollected;
    }

    private void OnDisable()
    {
        foreach (var item in _items)
            item.ItemWasCollected -= OnItemWasCollected;
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