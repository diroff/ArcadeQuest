public class UIMetaItemList : UIItemList
{
    private UIMetaItemSlot _slot => _slotPrefab as UIMetaItemSlot;

    protected override void PrepareItemsForItemList()
    {
        foreach (var slot in _level.Items)
        {
            if(slot is MetaItem metaItem)
            {
                if(metaItem.IsLevelGoal)
                    _items.Add(slot);
            }
        }
    }

    protected override UIItemSlot CreateItemSlot(UIItemSlot slotPrefab)
    {
        slotPrefab = _slot;

        return base.CreateItemSlot(slotPrefab);
    }
}