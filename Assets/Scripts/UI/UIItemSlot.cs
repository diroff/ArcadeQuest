using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] protected Image ItemIcon;
    [SerializeField] protected TextMeshProUGUI ItemCountText;

    protected int ItemCount;
    protected int ItemID;

    public int ID => ItemID;

    public void SetItemID(int id)
    {
        ItemID = id;
    }

    public void SetIcon(Sprite sprite)
    {
        ItemIcon.sprite = sprite;
    }


    public void SetItemCount(int count)
    {
        if (count < 0)
            return;

        ItemCount = count;
        SetCurrentItemCountText();
    }

    public void RemoveItems(int count)
    {
        ItemCount -= count;

        SetCurrentItemCountText();

        if (ItemCount <= 0)
            Destroy(gameObject);
    }

    protected virtual void SetCurrentItemCountText()
    {
        ItemCountText.text = ItemCount.ToString();
    }
}