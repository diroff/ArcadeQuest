using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] protected Image ItemIcon;
    [SerializeField] protected TextMeshProUGUI ItemCountText;

    protected List<Item> SlotItems;

    protected int ItemCount;
    protected int MaxItemCount;

    public UnityAction<UIItemSlot> UIItemWasRemoved;

    public List<Item> Item => SlotItems;

    public void Initialize(List<Item> items)
    {
        SlotItems = new List<Item>(items);
        ItemIcon.sprite = SlotItems[0].Icon;
        MaxItemCount = ItemCount = SlotItems.Count;
        SetCurrentItemCountText();

        foreach (var item in SlotItems)
            item.ItemWasDestroyed += OnItemWasDestroyed;
    }

    private void OnEnable()
    {
        if (SlotItems == null)
            return;

        foreach (var item in SlotItems)
            item.ItemWasDestroyed += OnItemWasDestroyed;
    }

    private void OnDisable()
    {
        if (SlotItems == null)
            return;

        foreach (var item in SlotItems)
            item.ItemWasDestroyed -= OnItemWasDestroyed;
    }

    private void OnItemWasDestroyed()
    {
        RemoveItems(1);
    }

    protected void RemoveItems(int count)
    {
        ItemCount -= count;

        SetCurrentItemCountText();

        if (ItemCount <= 0)
        {
            UIItemWasRemoved?.Invoke(this);
            ItemCountText.gameObject.SetActive(false);
        }
    }

    protected virtual void SetCurrentItemCountText()
    {
        ItemCountText.text = ItemCount.ToString();
    }
}