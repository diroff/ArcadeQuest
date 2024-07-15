using UnityEngine;

public class MetaLevelTimer : Timer
{
    [SerializeField] private MetaItems _levelItems;

    private void OnEnable()
    {
        _levelItems.LevelWasCompleted += StopTimer;
    }

    private void OnDisable()
    {
        _levelItems.LevelWasCompleted -= StopTimer;
    }
}