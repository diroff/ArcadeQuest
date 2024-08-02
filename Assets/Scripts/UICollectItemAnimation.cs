using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollectItemAnimation : MonoBehaviour
{
    [SerializeField] private UIItemList _uiItemList;

    private Item _item;
    private UIItemSlot _slot;

    private void OnEnable()
    {
        _uiItemList.ItemListChanged += OnItemListChanged;
    }

    private void OnDisable()
    {
        _uiItemList.ItemListChanged -= OnItemListChanged;
    }

    public void OnItemListChanged(UIItemSlot slot)
    {
        _slot = slot;
        //_item = slot.ID
    }
}