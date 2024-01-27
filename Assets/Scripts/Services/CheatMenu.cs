using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] private CurrentLevelData _levelData;
    [SerializeField] private LevelProgression _levelProgression;

    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private LevelItems _levelItems;
    [SerializeField] private GameObject _UICheatMenu;

    public void SetMenuState()
    {
        if(_UICheatMenu.activeSelf)
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
        _levelItems.CompleteLevel();
    }

    public void CollectAllItems()
    {
        _levelItems.CollectAllItems();
    }
}