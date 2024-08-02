public class UIMainItemSlot : UIItemSlot
{
    protected override void SetCurrentItemCountText()
    {
        ItemCountText.text = ItemCount + "/" + MaxItemCount;
    }
}