public class UIMetaItemList : UIItemList
{
    private UIMetaItemSlot _slot => _slotPrefab as UIMetaItemSlot;

    protected override UIItemSlot CreateItemSlot(UIItemSlot slotPrefab)
    {
        slotPrefab = _slotPrefab;

        return base.CreateItemSlot(slotPrefab);
    }
}