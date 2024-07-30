public class MainLevel : Level
{
    public MainItem GetNotCollectedItem()
    {
        foreach (var item in LevelItems)

            if (!item.IsCollected)
                return item as MainItem;

        return null;
    }
}