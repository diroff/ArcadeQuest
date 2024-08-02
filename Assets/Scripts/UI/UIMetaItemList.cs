public class UIMetaItemList : UIItemList
{
    private UIMetaItemSlot _slot => _slotPrefab as UIMetaItemSlot;

    protected override UIItemSlot CreateItemSlot(UIItemSlot slotPrefab)
    {
        slotPrefab = _slot;

        return base.CreateItemSlot(slotPrefab);
    }
}