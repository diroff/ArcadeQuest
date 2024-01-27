using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    public const string Key = "LevelProgression";

    [SerializeField] private LevelItems _levelItems;
    [SerializeField] private LevelBonuses _levelBonuses;

    private IStorageService _storageService;
    private LevelProgress _levelData;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<LevelProgress>(Key, data =>
        {
            if (data == default)
                FirstLevelSave(SceneManager.GetActiveScene().name);

            _levelData = data;
        });
    }

    private void OnEnable()
    {
        foreach (var item in _levelItems.Items)
            item.ItemWasCollectedWithName += SaveCollectedItem;

        foreach (var item in _levelBonuses.Bonuses)
            item.BonusTaked += SaveCollectedBonus;
    }

    private void OnDisable()
    {
        foreach (var item in _levelItems.Items)
            item.ItemWasCollectedWithName -= SaveCollectedItem;

        foreach (var item in _levelBonuses.Bonuses)
            item.BonusTaked -= SaveCollectedBonus;
    }

    private void Start()
    {
        if (_levelData.CollectedItems == null || _levelData.CollectedItems.Count == 0)
            return;

        DestroyCollectedItems();
    }

    private void DestroyCollectedItems()
    {
        foreach (var item in _levelData.CollectedItems)
            GameObject.Find(item).GetComponent<Item>().Collect();

        foreach (var item in _levelData.CollectedBonus)
            GameObject.Find(item).GetComponent<Bonus>().Collect();
    }

    public void SaveCollectedItem(string itemName)
    {
        if (!_levelData.CollectedItems.Contains(itemName))
            _levelData.CollectedItems.Add(itemName);

        _storageService.Save(Key, _levelData);
    }

    public void SaveCollectedBonus(string bonusName)
    {
        if(!_levelData.CollectedBonus.Contains(bonusName))
            _levelData.CollectedBonus.Add(bonusName);

        _storageService.Save(Key, _levelData);
    }

    public void Load()
    {
        _storageService.Load<LevelProgress>(Key, data =>
        {
            _levelData = data;
        });
    }

    public void FirstLevelSave(string sceneName)
    {
        CurrentLevel level = new CurrentLevel();

        level.LevelName = sceneName;

        LevelProgress levelProgress = new LevelProgress();
        levelProgress.Level = level;

        _storageService.Save(Key, levelProgress);
    }

    public LevelProgress GetData()
    {
        Load();
        return _levelData;
    }
}

public class LevelProgress
{
    public CurrentLevel Level;
    public List<string> CollectedItems = new List<string>();
    public List<string> CollectedBonus = new List<string>();
}
