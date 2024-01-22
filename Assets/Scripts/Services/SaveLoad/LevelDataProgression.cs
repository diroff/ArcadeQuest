using UnityEngine;

public class LevelDataProgression : MonoBehaviour
{
    public const string Key = "LevelProgression";

    private IStorageService _storageService;
    private LevelProgression _levelData;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();
    }

    [ContextMenu("Save current level")]
    public void Save(string sceneName)
    {
        LevelProgression data = new LevelProgression();

        data.LevelName = sceneName;

        _storageService.Save(Key, data);
    }

    public void Load()
    {
        _storageService.Load<LevelProgression>(Key, data =>
        {
            _levelData = data;
        });
    }

    public LevelProgression GetData()
    {
        Load();
        return _levelData;
    }

    public void SetDefaultSettings()
    {
        Save("MainLevel_1");
    }
}

public class LevelProgression
{
    public string LevelName;
}