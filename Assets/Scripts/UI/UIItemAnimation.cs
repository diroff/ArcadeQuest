using System.Collections.Generic;
using UnityEngine;

public class UIItemAnimation : MonoBehaviour
{
    [SerializeField] private UIItemList _itemList;

    private List<UIItemSlot> _itemSlots;

    private void OnEnable()
    {
        _itemList.ItemListUIWasCreated += OnItemListWasCreated;

        if (_itemSlots != null)
            SubscribeOnItemList();
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

        SubscribeOnItemList();
    }

    private void OnUIItemWasRemoved(UIItemSlot slot)
    {
        Destroy(slot.gameObject);
    }

    private void SubscribeOnItemList()
    {
        foreach (var item in _itemSlots)
        {
            item.UIItemWasRemoved += OnUIItemWasRemoved;
        }
    }
}
