using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] protected Image ItemIcon;
    [SerializeField] protected TextMeshProUGUI ItemCountText;

    protected int ItemCount;
    protected int ItemID;
    protected int MaxItemCount;

    public UnityAction<UIItemSlot> UIItemWasRemoved;

    public int ID => ItemID;

    public void SetItemID(int id)
    {
        ItemID = id;
    }

    public void SetIcon(Sprite sprite)
    {
        ItemIcon.sprite = sprite;
    }

    public void SetMaxItemCount(int count)
    {
        MaxItemCount = count;
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
        {
            UIItemWasRemoved?.Invoke(this);
            Debug.Log("Item will be destroyed");
            //Destroy(gameObject);
        }
    }

    protected virtual void SetCurrentItemCountText()
    {
        ItemCountText.text = ItemCount.ToString();
    }
}