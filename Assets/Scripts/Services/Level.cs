using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Level : MonoBehaviour
{
    [SerializeField] protected List<Item> LevelItems;

    private int _itemsCollected;

    public UnityAction LevelWasCompleted;
    public UnityAction AllItemsCollected;

    public List<Item> Items => LevelItems;

    protected void OnEnable()
    {
        foreach (var item in LevelItems)
            item.ItemWasDestroyed += OnItemWasCollected;
    }

    protected void OnDisable()
    {
        foreach (var item in LevelItems)
            item.ItemWasDestroyed -= OnItemWasCollected;
    }

    protected void OnItemWasCollected()
    {
        _itemsCollected++;

        ItemCountChecker();
    }

    protected void ItemCountChecker()
    {
        if (_itemsCollected != LevelItems.Count)
            return;

        AllItemsCollected?.Invoke();
        LevelCompleted();
    }

    protected void LevelCompleted()
    {
        LevelWasCompleted?.Invoke();
    }

    public void CollectAllItems()
    {
        foreach (var item in LevelItems)
        {
            if (item == null)
                continue;

            if (item.IsCollected)
                continue;

            item.Collect();
        }
    }

    public void CompleteLevel()
    {
        LevelCompleted();
    }

    public void CollectItemsImmediately()
    {
        foreach(var item in LevelItems)
        {
            if (item == null)
                continue;

            if (item.IsCollected)
                continue;

            item.CollectImmediately();
        }
    }
}