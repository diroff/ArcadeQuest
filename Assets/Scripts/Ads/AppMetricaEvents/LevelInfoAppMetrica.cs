using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfoAppMetrica : MonoBehaviour
{
    [SerializeField] private LevelTimer _levelTimer;

    private string _eventName = "LevelStarted";

    private LevelStartData _startData;
    private LevelEndData _endData;

    private void OnEnable()
    {
        _levelTimer.LevelEndedWithTime += SendEndData;
    }

    private void OnDisable()
    {
        _levelTimer.LevelEndedWithTime -= SendEndData;
    }

    private void Start()
    {
        SendStartData();
    }

    public void SendStartData()
    {
        _startData = new LevelStartData();
        _startData.LevelName = SceneManager.GetActiveScene().name;

        string levelStartInfo = $"{_eventName} : {_startData.LevelName}";

        AppMetrica.Instance.ReportEvent(levelStartInfo);
    }

    public void SendEndData(float time)
    {
        _endData = new LevelEndData();
        _endData.LevelName = SceneManager.GetActiveScene().name;

        string levelEndInfo = $"{_eventName} : {_endData.LevelName}";

        AppMetrica.Instance.ReportEvent(levelEndInfo);
    }
}

public class LevelStartData
{
    public string LevelName;
}

public class LevelEndData
{
    public string LevelName;
    public float LevelTime;
}