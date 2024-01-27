using UnityEngine;

public class LevelDataSaver : MonoBehaviour
{
    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private CurrentLevelData _levelData;
    [SerializeField] private LevelProgression _levelProgression;
    [SerializeField] private LevelItems _levelItems;

    private void OnEnable()
    {
        _levelItems.AllItemsCollected += SaveLevelData;
    }

    private void OnDisable()
    {
        _levelItems.AllItemsCollected -= SaveLevelData;
    }

    private void SaveLevelData()
    {
        _levelData.Save(_levelLoading.SceneName);
        _levelProgression.FirstLevelSave(_levelLoading.SceneName);
    }
}