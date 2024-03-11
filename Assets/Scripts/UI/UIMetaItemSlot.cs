using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMetaItemSlot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemCountText;

    private int _itemCount;
    private int _itemID;

    public int ItemID => _itemID;

    public void SetIcon(Sprite sprite)
    {
        _itemIcon.sprite = sprite;
    }

    public void SetItemCount(int count)
    {
        if (count < 0)
            return;

        _itemCount = count;
        _itemCountText.text = _itemCount.ToString();
    }

    public void SetItemID(int id)
    {
        _itemID = id;
    }

    public void RemoveItems(int count)
    {
        _itemCount -= count;
        _itemCountText.text = _itemCount.ToString();

        if (_itemCount <= 0)
            Destroy(gameObject);
    }
}