using UnityEngine;

public class LastLevelLoading : MonoBehaviour
{
    [SerializeField] private LevelLoading _levelLoading;
    [SerializeField] private LevelDataProgression _progression;

    public void LoadLastLevel()
    {
        _progression.Load();
        var levelName = _progression.GetData().LevelName;
        _levelLoading.LoadLevel(levelName);
    }
}