using UnityEngine;

public class MainLevelTimeChecker : Timer
{
    [SerializeField] private LevelItems _levelItems;

    private void OnEnable()
    {
        _levelItems.AllItemsCollected += StopTimer;
    }

    private void OnDisable()
    {
        _levelItems.AllItemsCollected -= StopTimer;
    }
}