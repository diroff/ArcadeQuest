using UnityEngine;

public class CurrentLevelData : MonoBehaviour
{
    [SerializeField] private string _defaultLevelName = "Level1";

    public const string Key = "CurrentLevel";

    private IStorageService _storageService;
    private CurrentLevel _levelData;

    private void Awake()
    {
        SetupStorageService();
    }

    private void SetupStorageService()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<CurrentLevel>(Key, data =>
        {
            if (data == default)
                SetDefaultLevel();
        });
    }

    [ContextMenu("Save current level")]
    public void Save(string sceneName)
    {
        if (_storageService == null)
            SetupStorageService();

        CurrentLevel data = new CurrentLevel();

        data.LevelName = sceneName;

        _storageService.Save(Key, data);
    }

    public void Load()
    {
        _storageService.Load<CurrentLevel>(Key, data =>
        {
            _levelData = data;
        });
    }

    public CurrentLevel GetData()
    {
        Load();
        return _levelData;
    }

    public void SetDefaultLevel()
    {
        CurrentLevel level = new CurrentLevel();
        level.LevelName = _defaultLevelName;

        _storageService.Save(Key, level);
    }
}

public class CurrentLevel
{
    public string LevelName;
}