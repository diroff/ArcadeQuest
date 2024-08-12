using UnityEngine;

public class UIInputBlocker : MonoBehaviour
{
    [SerializeField] private GameObject _blocker;
    [SerializeField] private Level _level;

    private void OnEnable()
    {
        _level.LevelWasCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _level.LevelWasCompleted -= OnLevelCompleted;
    }

    protected virtual void OnLevelCompleted()
    {
        _blocker.SetActive(true);
    }
}