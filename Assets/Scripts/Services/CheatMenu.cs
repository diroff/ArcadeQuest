using UnityEngine;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] private CurrentLevelData _levelData;
    [SerializeField] private LevelProgression _levelProgression;

    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private Level _level;
    [SerializeField] private GameObject _UICheatMenu;

    public void SetMenuState()
    {
        if (_UICheatMenu.activeSelf)
            _UICheatMenu.SetActive(false);
        else
            _UICheatMenu.SetActive(true);
    }

    public void ResetSave()
    {
        _levelData.SetDefaultLevel();
        _levelData.Load();

        _levelProgression.FirstLevelSave(_levelData.GetData().LevelName);

        var nextLevel = _levelData.GetData().LevelName;

        _levelLoading.LoadLevel(nextLevel);
    }

    public void CompleteLevel()
    {
        _level.CompleteLevel();
    }

    public void CollectAllItems()
    {
        _level.CollectItemsImmediately();
    }
}