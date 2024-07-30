using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    public const string Key = "LevelProgression";

    [SerializeField] protected Level Level;
    [SerializeField] protected CurrentLevelData CurrentLevelData;

    protected IStorageService StorageService;
    protected LevelProgress LevelProgress;

    private void Awake()
    {
        StorageService = new JsonToFileStorageService();

        StorageService.Load<LevelProgress>(Key, data =>
        {
            if (data == default)
                FirstLevelSave(SceneManager.GetActiveScene().name);
            else
                LevelProgress = data;
        });

        if (LevelProgress.Level != SceneManager.GetActiveScene().name)
        {
            Debug.Log("Was first saved, because other level!");
            FirstLevelSave(SceneManager.GetActiveScene().name);
            Load();
        }
    }

    public void Load()
    {
        StorageService.Load<LevelProgress>(Key, data =>
        {
            LevelProgress = data;
        });
    }

    public void FirstLevelSave(string sceneName)
    {
        CurrentLevel level = new CurrentLevel
        {
            LevelName = sceneName
        };

        CurrentLevelData.Save(sceneName);

        LevelProgress levelProgress = new LevelProgress
        {
            Level = sceneName,
            CollectedItems = new List<int>(),
            CollectedBonus = new List<int>(),
            PlayerPosition = null,
            PlayerRotation = null,
            CameraPosition = null,
            CameraRotation = null
        };

        StorageService.Save(Key, levelProgress);
    }

    public LevelProgress GetData()
    {
        Load();
        return LevelProgress;
    }
}

public class LevelProgress
{
    public string Level;
    public List<int> CollectedItems = new List<int>();
    public List<int> CollectedBonus = new List<int>();

    public CurrentPosition PlayerPosition;
    public CurrentRotation PlayerRotation;

    public CurrentPosition CameraPosition;
    public CurrentRotation CameraRotation;
}

public class CurrentPosition
{
    public float x;
    public float y;
    public float z;
}

public class CurrentRotation
{
    public float x;
    public float y;
    public float z;
    public float w;
}