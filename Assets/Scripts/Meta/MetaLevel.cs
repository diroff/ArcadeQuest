using UnityEngine;

public class MetaLevel : Level
{
    [SerializeField] private MetaSlotPanel _slotPanel;

    private int _itemsCount;

    private void Awake()
    {
        foreach (var item in LevelItems)
        {
            if(item is MetaItem metaItem)
            {
                metaItem.SetSlotPanel(_slotPanel);

                if(metaItem.IsLevelGoal)
                    _itemsCount++;
            }
        }
    }

    protected override void IncreaseCollectedItemCount(Item item)
    {
        if(item is MetaItem metaItem)
        {
            if (!metaItem.IsLevelGoal)
                return;
        }

        base.IncreaseCollectedItemCount(item);
    }

    protected override bool IsAllItemsCollected()
    {
        return _itemsCollected == _itemsCount;
    }
}