using UnityEngine;

public class LevelTimeChecker : Timer
{
    [SerializeField] private Level _levelItems;

    private void OnEnable()
    {
        _levelItems.AllItemsCollected += StopTimer;
    }

    private void OnDisable()
    {
        _levelItems.AllItemsCollected -= StopTimer;
    }
}