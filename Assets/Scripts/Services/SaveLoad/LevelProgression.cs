using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    public const string Key = "LevelProgression";

    [SerializeField] private CurrentLevelData _currentLevelData;
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
            else
                _levelData = data;
        });

        if (_levelData.Level != SceneManager.GetActiveScene().name)
        {
            Debug.Log("Was first saved, because other level!");
            FirstLevelSave(SceneManager.GetActiveScene().name);
            Load();
        }
    }

    private void OnEnable()
    {
        foreach (var item in _levelItems.Items)
            item.ItemWasCollectedWithSceneID += SaveCollectedItem;

        foreach (var bonus in _levelBonuses.Bonuses)
            bonus.BonusTaked += SaveCollectedBonus;
    }

    private void OnDisable()
    {
        foreach (var item in _levelItems.Items)
            item.ItemWasCollectedWithSceneID -= SaveCollectedItem;

        foreach (var bonus in _levelBonuses.Bonuses)
            bonus.BonusTaked -= SaveCollectedBonus;
    }

    private void Start()
    {
        DestroyCollectedItems();

        if (_levelData.PlayerPosition != null)
            SetPlayerPositions();

        if (_levelData.CameraPosition != null)
            SetCameraPosition();
    }

    private void DestroyCollectedItems()
    {
        // Удаляем собранные предметы
        foreach (var item in _levelItems.Items)
        {
            if (_levelData.CollectedItems.Contains(item.ID))
                item.Collect();
        }

        // Удаляем собранные бонусы
        foreach (var bonus in _levelBonuses.Bonuses)
        {
            if (_levelData.CollectedBonus.Contains(bonus.ID))
                bonus.Collect();
        }
    }

    private void SetPlayerPositions()
    {
        if (_levelData.PlayerPosition == null)
            return;

        var position = _levelData.PlayerPosition;
        var rotation = _levelData.PlayerRotation;

        _player.transform.position = new Vector3(position.x, position.y, position.z);
        _player.transform.localRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
    }

    private void SetCameraPosition()
    {
        if (_levelData.CameraPosition == null)
            return;

        var position = _levelData.CameraPosition;
        var rotation = _levelData.CameraRotation;

        _camera.transform.position = new Vector3(position.x, position.y, position.z);
        _camera.transform.localRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
    }

    public void SaveCollectedItem(int itemID)
    {
        if (_levelData.CollectedItems.Contains(itemID))
            return;

        _levelData.CollectedItems.Add(itemID);

        SavePlayerPlacement();
        SaveCameraPlacement();

        _storageService.Save(Key, _levelData);
    }

    public void SaveCollectedBonus(int bonusID)
    {
        if (_levelData.CollectedBonus.Contains(bonusID))
            return;

        _levelData.CollectedBonus.Add(bonusID);

        SavePlayerPlacement();
        SaveCameraPlacement();

        _storageService.Save(Key, _levelData);
    }

    private void SavePlayerPlacement()
    {
        _levelData.PlayerPosition = new CurrentPosition
        {
            x = _player.gameObject.transform.position.x,
            y = _player.gameObject.transform.position.y,
            z = _player.gameObject.transform.position.z
        };

        _levelData.PlayerRotation = new CurrentRotation
        {
            x = _player.gameObject.transform.rotation.x,
            y = _player.gameObject.transform.rotation.y,
            z = _player.gameObject.transform.rotation.z,
            w = _player.gameObject.transform.rotation.w
        };
    }

    private void SaveCameraPlacement()
    {
        _levelData.CameraPosition = new CurrentPosition
        {
            x = _camera.gameObject.transform.position.x,
            y = _camera.gameObject.transform.position.y,
            z = _camera.gameObject.transform.position.z
        };

        _levelData.CameraRotation = new CurrentRotation
        {
            x = _camera.gameObject.transform.rotation.x,
            y = _camera.gameObject.transform.rotation.y,
            z = _camera.gameObject.transform.rotation.z,
            w = _camera.gameObject.transform.rotation.w
        };
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
        CurrentLevel level = new CurrentLevel
        {
            LevelName = sceneName
        };

        _currentLevelData.Save(sceneName);

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