using UnityEngine;

public class LevelDataProgression : MonoBehaviour
{
    [SerializeField] private string _defaultLevelName = "MainLevel_1";

    public const string Key = "LevelProgression";

    private IStorageService _storageService;
    private LevelProgression _levelData;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<LevelProgression>(Key, data =>
        {
            if (data == default)
                SetDefaultLevel();
        });
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

    public void SetDefaultLevel()
    {
        LevelProgression level = new LevelProgression();
        level.LevelName = _defaultLevelName;

        _storageService.Save(Key, level);
    }
}

public class LevelProgression
{
    public string LevelName;
}