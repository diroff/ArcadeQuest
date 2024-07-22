using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfoAppMetrica : MonoBehaviour
{
    [SerializeField] private Timer _levelTimer;

    private string _eventName;
    private string _levelName;

    private void Awake()
    {
        SetLevelName();
    }

    private void OnEnable()
    {
        _levelTimer.TimerEnded += SetEndTime;
    }

    private void OnDisable()
    {
        _levelTimer.TimerEnded -= SetEndTime;
    }

    private void Start()
    {
        SendStartData();
    }

    public void SendStartData()
    {
        _eventName = "LevelStarted";

        string levelStartInfo = $"{_eventName} : {_levelName}";

        Debug.Log(levelStartInfo);
        AppMetrica.Instance.ReportEvent(levelStartInfo);
    }

    public void SetEndTime(float time)
    {
        SendEndData();

        _eventName = "LevelFinishedTime";

        int levelTime = Mathf.RoundToInt(time);

        string levelEndInfo = $"{_eventName} : {_levelName} : {levelTime} seconds";

        Debug.Log(levelEndInfo);
        AppMetrica.Instance.ReportEvent(levelEndInfo);
    }

    public void SendEndData()
    {
        _eventName = "LevelFinished";

        string levelEndInfo = $"{_eventName} : {_levelName}";

        Debug.Log(levelEndInfo);
        AppMetrica.Instance.ReportEvent(levelEndInfo);
    }

    private void SetLevelName()
    {
        _levelName = SceneManager.GetActiveScene().name;
    }
}