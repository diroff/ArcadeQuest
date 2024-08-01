using System.Collections.Generic;
using UnityEngine;

public class UIItemAnimation : MonoBehaviour
{
    [SerializeField] private UIItemList _itemList;

    private List<UIItemSlot> _itemSlots;

    private void OnEnable()
    {
        _itemList.ItemListUIWasCreated += OnItemListWasCreated;

        if (_itemSlots == null)
            return;

        foreach (var item in _itemSlots)
        {
            item.UIItemWasRemoved += OnUIItemWasRemoved;
        }
    }

    private void OnDisable()
    {
        _itemList.ItemListUIWasCreated -= OnItemListWasCreated;

        if (_itemSlots == null)
            return;

        foreach (var item in _itemSlots)
        {
            item.UIItemWasRemoved -= OnUIItemWasRemoved;
        }
    }

    private void OnItemListWasCreated(List<UIItemSlot> itemList)
    {
        _itemSlots = new List<UIItemSlot>(itemList);

        foreach (var item in _itemSlots)
        {
            item.UIItemWasRemoved += OnUIItemWasRemoved;
        }
    }

    private void OnUIItemWasRemoved(UIItemSlot slot)
    {
        Destroy(slot.gameObject);
    }
}
