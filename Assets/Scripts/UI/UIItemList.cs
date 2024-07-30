using System.Collections.Generic;
using UnityEngine;

public class UIItemList : MonoBehaviour
{
    [SerializeField] protected UIItemSlot _slotPrefab;
    [SerializeField] protected Transform _slotPlacement;
    [SerializeField] protected Level _level;

    protected List<UIItemSlot> _slots = new List<UIItemSlot>();
    protected List<Item> _items = new List<Item>();

    private void Awake()
    {
        foreach (var slot in _level.Items)
            _items.Add(slot);

        CreateItemList();
    }

    private void OnEnable()
    {
        foreach (var item in _level.Items)
            item.ItemWasDestroyedWithPrefabID += ItemPanelRefresh;
    }

    private void OnDisable()
    {
        foreach (var item in _level.Items)
            item.ItemWasDestroyedWithPrefabID -= ItemPanelRefresh;
    }

    private void ItemPanelRefresh(int id)
    {
        foreach (var item in _slots)
        {
            if (item.ID != id)
                continue;

            item.RemoveItems(1);
        }
    }

    private void CreateItemList()
    {
        if (_items.Count == 0)
            return;

        var itemId = _items[0].PrefabID;
        int itemCount = 0;

        List<Item> items = new List<Item>();

        foreach (var item in _items)
        {
            if (item.PrefabID != itemId)
                continue;

            itemCount++;
            items.Add(item);
        }

        var itemSlot = CreateItemSlot(_slotPrefab);
        itemSlot.SetMaxItemCount(itemCount);
        itemSlot.SetItemCount(itemCount);

        _slots.Add(itemSlot);

        foreach (var item in items)
            _items.Remove(item);

        CreateItemList();
    }

    protected virtual UIItemSlot CreateItemSlot(UIItemSlot slotPrefab)
    {
        var itemSlot = Instantiate(slotPrefab, _slotPlacement);
        itemSlot.SetIcon(_items[0].Icon);
        itemSlot.SetItemID(_items[0].PrefabID);

        return itemSlot;
    }
}