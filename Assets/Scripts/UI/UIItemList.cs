using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIItemList : MonoBehaviour
{
    [SerializeField] protected UIItemSlot _slotPrefab;
    [SerializeField] protected Transform _slotPlacement;
    [SerializeField] protected Level _level;

    [SerializeField] private GridLayoutGroup _gridLayout;

    protected List<UIItemSlot> _slots = new List<UIItemSlot>();
    protected List<Item> _items = new List<Item>();

    public UnityAction<List<UIItemSlot>> ItemListUIWasCreated;
    public UnityAction<UIItemSlot> ItemListChanged;

    public GridLayoutGroup GridLayout => _gridLayout;

    private void Awake()
    {
        foreach (var slot in _level.Items)
            _items.Add(slot);

        CreateItemList();
    }

    private void CreateItemList()
    {
        if (_items.Count == 0)
        {
            ItemListUIWasCreated?.Invoke(_slots);
            return;
        }

        var itemId = _items[0].PrefabID;
        int itemCount = 0;

        List<Item> concreteItemList = new List<Item>();

        foreach (var item in _items)
        {
            if (item.PrefabID != itemId)
                continue;

            itemCount++;
            concreteItemList.Add(item);
        }

        var itemSlot = CreateItemSlot(_slotPrefab);
        itemSlot.Initialize(concreteItemList);

        _slots.Add(itemSlot);

        foreach (var item in concreteItemList)
            _items.Remove(item);

        CreateItemList();
    }

    protected virtual UIItemSlot CreateItemSlot(UIItemSlot slotPrefab)
    {
        var itemSlot = Instantiate(slotPrefab, _slotPlacement);

        return itemSlot;
    }
}