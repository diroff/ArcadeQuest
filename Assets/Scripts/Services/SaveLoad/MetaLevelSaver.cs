using UnityEngine;

public class MetaLevelSaver : MonoBehaviour
{
    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private CurrentLevelData _levelData;
    [SerializeField] private MetaLevel _meta;

    private void OnEnable()
    {
        _meta.LevelWasCompleted += SaveLevelData;
    }

    private void OnDisable()
    {
        _meta.LevelWasCompleted -= SaveLevelData;
    }

    private void SaveLevelData()
    {
        _levelData.Save(_levelLoading.SceneName);
    }
}