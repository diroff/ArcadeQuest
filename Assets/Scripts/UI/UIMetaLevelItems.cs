using System.Collections.Generic;
using UnityEngine;

public class UIMetaLevelItems : MonoBehaviour
{
    [SerializeField] private UIMetaItemSlot _slotPrefab;
    [SerializeField] private MetaItems _meta;

    private List<UIMetaItemSlot> _slots = new List<UIMetaItemSlot>();
    private List<MetaItem> _items = new List<MetaItem>();

    private void Awake()
    {
        foreach (var slot in _meta.Items)
            _items.Add(slot);
    }

    private void OnEnable()
    {
        foreach (var item in _meta.Items)
            item.ItemWasDestroyedWithId += ItemPanelRefresh;
    }

    private void OnDisable()
    {
        foreach (var item in _meta.Items)
            item.ItemWasDestroyedWithId -= ItemPanelRefresh;
    }

    private void Start()
    {
        CreateItemList();
    }

    private void CreateItemList()
    {
        if (_items.Count == 0)
            return;

        var itemId = _items[0].ItemId;
        int itemCount = 0;

        List<MetaItem> items = new List<MetaItem>();

        foreach (var item in _items)
        {
            if (item.ItemId != itemId)
                continue;

            itemCount++;
            items.Add(item);
        }

        var itemSlot = Instantiate(_slotPrefab, transform);
        itemSlot.SetIcon(_items[0].ItemIcon);
        itemSlot.SetItemID(_items[0].ItemId);
        itemSlot.SetItemCount(itemCount);

        _slots.Add(itemSlot);

        foreach (var item in items)
            _items.Remove(item);

        CreateItemList();
    }

    private void ItemPanelRefresh(int id)
    {
        foreach (var item in _slots)
        {
            if (item.ItemID != id)
                continue;

            item.RemoveItems(1);
        }
    }
}
