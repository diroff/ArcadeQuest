using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Level : MonoBehaviour
{
    [SerializeField] protected List<Item> LevelItems;

    protected int _itemsCollected;

    public UnityAction LevelWasCompleted;
    public UnityAction AllItemsCollected;

    public List<Item> Items => LevelItems;

    protected void OnEnable()
    {
        foreach (var item in LevelItems)
            item.ConcreteItemWasDestroyed += OnItemWasCollected;
    }

    protected void OnDisable()
    {
        foreach (var item in LevelItems)
            item.ConcreteItemWasDestroyed -= OnItemWasCollected;
    }

    protected void OnItemWasCollected(Item item)
    {
        IncreaseCollectedItemCount(item);
        ItemCountChecker();
    }

    protected virtual void IncreaseCollectedItemCount(Item item)
    {
        _itemsCollected++;
    }

    protected void ItemCountChecker()
    {
        if (!IsAllItemsCollected())
            return;

        AllItemsCollected?.Invoke();
        LevelCompleted();
    }

    protected virtual bool IsAllItemsCollected()
    {
        return _itemsCollected == LevelItems.Count;
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