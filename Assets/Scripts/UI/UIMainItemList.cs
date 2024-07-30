public class UIMainItemList : UIItemList
{
    private UIMainItemSlot _slot => _slotPrefab as UIMainItemSlot;

    protected override UIItemSlot CreateItemSlot(UIItemSlot slotPrefab)
    {
        slotPrefab = _slotPrefab;

        return base.CreateItemSlot(slotPrefab);
    }
}