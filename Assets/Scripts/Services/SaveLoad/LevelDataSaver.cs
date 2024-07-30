using UnityEngine;

public class LevelDataSaver : MonoBehaviour
{
    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private CurrentLevelData _levelData;
    [SerializeField] private MainLevel _levelItems;

    private void OnEnable()
    {
        _levelItems.LevelWasCompleted += SaveLevelData;
    }

    private void OnDisable()
    {
        _levelItems.LevelWasCompleted -= SaveLevelData;
    }

    private void SaveLevelData()
    {
        _levelData.Save(_levelLoading.SceneName);
    }
}