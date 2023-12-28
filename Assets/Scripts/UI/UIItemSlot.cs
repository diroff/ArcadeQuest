using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private GameObject _itemCompletedPlaceholder;

    private Item _item;

    private void OnDisable()
    {
        _item.ItemWasCollected -= CompleteIcon;
    }

    public void SetItem(Item item)
    {
        _item = item;
        _itemIcon.sprite = _item.Icon;
        _item.ItemWasCollected += CompleteIcon;
    }

    private void CompleteIcon()
    {
        _itemCompletedPlaceholder.SetActive(true);
    }
}