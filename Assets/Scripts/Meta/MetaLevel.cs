using UnityEngine;

public class MetaLevel : Level
{
    [SerializeField] private MetaSlotPanel _slotPanel;

    private void Awake()
    {
        foreach (var item in LevelItems)
        {
            if(item is MetaItem metaItem)
                metaItem.SetSlotPanel(_slotPanel);
        }
    }
}