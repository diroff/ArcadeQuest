using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemList : MonoBehaviour
{
    [SerializeField] private UIItemSlot _slotPrefab;
    [SerializeField] private Transform _slotPlacement;

    [SerializeField] private LevelItems _levelItems;

    private List<UIItemSlot> _slots = new List<UIItemSlot>();
    private List<MainItem> _items = new List<MainItem>();

    private GridLayoutGroup _layoutGroup;

    public GridLayoutGroup LayoutGroup => _layoutGroup;

    private void Awake()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();

        foreach (var slot in _levelItems.Items)
            _items.Add(slot);
    }

    private void OnEnable()
    {
        foreach (var item in _levelItems.Items)
            item.ItemWasCollectedWithPrefabID += ItemPanelRefresh;
    }

    private void OnDisable()
    {
        foreach (var item in _levelItems.Items)
            item.ItemWasCollectedWithPrefabID -= ItemPanelRefresh;
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

        List<MainItem> items = new List<MainItem>();

        foreach (var item in _items)
        {
            if (item.PrefabID != itemId)
                continue;

            itemCount++;
            items.Add(item);
        }

        var itemSlot = Instantiate(_slotPrefab, _slotPlacement);
        itemSlot.SetIcon(_items[0].Icon);
        itemSlot.SetItemID(_items[0].PrefabID);
        itemSlot.SetMaxItemCount(itemCount);
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