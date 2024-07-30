using System.Collections.Generic;
using UnityEngine;

public class UIMetaLevelItems : MonoBehaviour
{
    [SerializeField] private UIMetaItemSlot _slotPrefab;
    [SerializeField] private Transform _slotPlacement;
    [SerializeField] private MetaLevel _meta;

    private List<UIMetaItemSlot> _slots = new List<UIMetaItemSlot>();
    private List<MetaItem> _items = new List<MetaItem>();

    private void Awake()
    {
        foreach (var slot in _meta.Items)
            _items.Add(slot as MetaItem);
    }

    private void OnEnable()
    {
        foreach (var item in _meta.Items)
            item.ItemWasDestroyedWithPrefabID += ItemPanelRefresh;
    }

    private void OnDisable()
    {
        foreach (var item in _meta.Items)
            item.ItemWasDestroyedWithPrefabID -= ItemPanelRefresh;
    }

    private void Start()
    {
        CreateItemList();
    }

    private void CreateItemList()
    {
        if (_items.Count == 0)
            return;

        var itemId = _items[0].PrefabID;
        int itemCount = 0;

        List<MetaItem> items = new List<MetaItem>();

        foreach (var item in _items)
        {
            if (item.PrefabID != itemId)
                continue;

            itemCount++;
            items.Add(item);
            Debug.Log("Item was added:" + item);
        }

        var itemSlot = Instantiate(_slotPrefab, _slotPlacement);
        itemSlot.SetIcon(_items[0].Icon);
        itemSlot.SetItemID(_items[0].PrefabID);
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
            if (item.ID != id)
                continue;

            item.RemoveItems(1);
        }
    }
}
