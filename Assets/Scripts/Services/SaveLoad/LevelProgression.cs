using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    public const string Key = "LevelProgression";

    [SerializeField] private LevelItems _levelItems;
    [SerializeField] private LevelBonuses _levelBonuses;

    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;

    private IStorageService _storageService;
    private LevelProgress _levelData;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<LevelProgress>(Key, data =>
        {
            if (data == default)
                FirstLevelSave(SceneManager.GetActiveScene().name);

            Load();
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

        if (_levelData.PlayerPosition != null)
            SetPlayerPositions();

        if (_levelData.CameraPosition != null)
            SetCameraPosition();
    }

    private void DestroyCollectedItems()
    {
        foreach (var item in _levelData.CollectedItems)
            GameObject.Find(item).GetComponent<Item>().Collect();

        foreach (var item in _levelData.CollectedBonus)
            GameObject.Find(item).GetComponent<Bonus>().Collect();
    }

    private void SetPlayerPositions()
    {
        var position = _levelData.PlayerPosition;
        var rotation = _levelData.PlayerRotation;

        _player.transform.position = new Vector3(position.x, position.y, position.z);
        _player.transform.localRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
    }

    private void SetCameraPosition()
    {
        var position = _levelData.CameraPosition;
        var rotation = _levelData.CameraRotation;

        _camera.transform.position = new Vector3(position.x, position.y, position.z);
        _camera.transform.localRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
    }

    public void SaveCollectedItem(string itemName)
    {
        if (_levelData.CollectedItems.Contains(itemName))
            return;

        _levelData.CollectedItems.Add(itemName);

        SavePlayerPlacement();
        SaveCameraPlacement();

        _storageService.Save(Key, _levelData);
    }

    public void SaveCollectedBonus(string bonusName)
    {
        if (_levelData.CollectedBonus.Contains(bonusName))
            return;

        _levelData.CollectedBonus.Add(bonusName);

        SavePlayerPlacement();
        SaveCameraPlacement();

        _storageService.Save(Key, _levelData);
    }

    private void SavePlayerPlacement()
    {
        _levelData.PlayerPosition = new CurrentPosition();
        _levelData.PlayerRotation = new CurrentRotation();

        _levelData.PlayerPosition.x = _player.gameObject.transform.position.x;
        _levelData.PlayerPosition.y = _player.gameObject.transform.position.y;
        _levelData.PlayerPosition.z = _player.gameObject.transform.position.z;

        _levelData.PlayerRotation.x = _player.gameObject.transform.rotation.x;
        _levelData.PlayerRotation.y = _player.gameObject.transform.rotation.y;
        _levelData.PlayerRotation.z = _player.gameObject.transform.rotation.z;
        _levelData.PlayerRotation.w = _player.gameObject.transform.rotation.w;
    }

    private void SaveCameraPlacement()
    {
        _levelData.CameraPosition = new CurrentPosition();

        _levelData.CameraPosition.x = _camera.gameObject.transform.position.x;
        _levelData.CameraPosition.y = _camera.gameObject.transform.position.y;
        _levelData.CameraPosition.z = _camera.gameObject.transform.position.z;

        _levelData.CameraRotation.x = _camera.gameObject.transform.rotation.x;
        _levelData.CameraRotation.y = _camera.gameObject.transform.rotation.y;
        _levelData.CameraRotation.z = _camera.gameObject.transform.rotation.z;
        _levelData.CameraRotation.w = _camera.gameObject.transform.rotation.w;
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

public struct CurrentRotation
{
    public float x;
    public float y;
    public float z;
    public float w;
}