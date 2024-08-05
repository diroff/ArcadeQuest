using UnityEngine;

public class LevelProgressionMain : LevelProgression
{
    [SerializeField] private LevelBonuses _levelBonuses;
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;

    private void OnEnable()
    {
        foreach (var item in Level.Items)
            item.ItemWasCollectedWithSceneID += SaveCollectedItem;

        foreach (var bonus in _levelBonuses.Bonuses)
            bonus.BonusTaked += SaveCollectedBonus;
    }

    private void OnDisable()
    {
        foreach (var item in Level.Items)
            item.ItemWasCollectedWithSceneID -= SaveCollectedItem;

        foreach (var bonus in _levelBonuses.Bonuses)
            bonus.BonusTaked -= SaveCollectedBonus;
    }

    private void Start()
    {
        DestroyCollectedItems();

        if (LevelProgress.PlayerPosition != null)
            SetPlayerPositions();

        if (LevelProgress.CameraPosition != null)
            SetCameraPosition();
    }

    private void DestroyCollectedItems()
    {
        foreach (var item in Level.Items)
        {
            if (LevelProgress.CollectedItems.Contains(item.SceneID))
                item.CollectImmediately();
        }

        foreach (var bonus in _levelBonuses.Bonuses)
        {
            if (LevelProgress.CollectedBonus.Contains(bonus.ID))
                bonus.Collect();
        }
    }

    private void SetPlayerPositions()
    {
        if (LevelProgress.PlayerPosition == null)
            return;

        var position = LevelProgress.PlayerPosition;
        var rotation = LevelProgress.PlayerRotation;

        _player.transform.position = new Vector3(position.x, position.y, position.z);
        _player.transform.localRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
    }

    private void SetCameraPosition()
    {
        if (LevelProgress.CameraPosition == null)
            return;

        var position = LevelProgress.CameraPosition;
        var rotation = LevelProgress.CameraRotation;

        _camera.transform.position = new Vector3(position.x, position.y, position.z);
        _camera.transform.localRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
    }

    public void SaveCollectedItem(int itemID)
    {
        if (LevelProgress.CollectedItems.Contains(itemID))
            return;

        LevelProgress.CollectedItems.Add(itemID);

        SavePlayerPlacement();
        SaveCameraPlacement();

        StorageService.Save(Key, LevelProgress);
    }

    public void SaveCollectedBonus(int bonusID)
    {
        if (LevelProgress.CollectedBonus.Contains(bonusID))
            return;

        LevelProgress.CollectedBonus.Add(bonusID);

        SavePlayerPlacement();
        SaveCameraPlacement();

        StorageService.Save(Key, LevelProgress);
    }

    private void SavePlayerPlacement()
    {
        LevelProgress.PlayerPosition = new CurrentPosition
        {
            x = _player.gameObject.transform.position.x,
            y = _player.gameObject.transform.position.y,
            z = _player.gameObject.transform.position.z
        };

        LevelProgress.PlayerRotation = new CurrentRotation
        {
            x = _player.gameObject.transform.rotation.x,
            y = _player.gameObject.transform.rotation.y,
            z = _player.gameObject.transform.rotation.z,
            w = _player.gameObject.transform.rotation.w
        };
    }

    private void SaveCameraPlacement()
    {
        LevelProgress.CameraPosition = new CurrentPosition
        {
            x = _camera.gameObject.transform.position.x,
            y = _camera.gameObject.transform.position.y,
            z = _camera.gameObject.transform.position.z
        };

        LevelProgress.CameraRotation = new CurrentRotation
        {
            x = _camera.gameObject.transform.rotation.x,
            y = _camera.gameObject.transform.rotation.y,
            z = _camera.gameObject.transform.rotation.z,
            w = _camera.gameObject.transform.rotation.w
        };
    }
}