using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMetaTimer : MonoBehaviour
{
    [SerializeField] private MetaTimer _metaTimer;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Slider _timerSlider;

    private void OnEnable()
    {
        _metaTimer.TimerWasUpdated += UpdateTimer;
    }

    private void OnDisable()
    {
        _metaTimer.TimerWasUpdated -= UpdateTimer;
    }

    private void UpdateTimer(float currentTime, float startTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        string formattedTime = string.Format("{0}:{1:00}", minutes, seconds);

        _timerText.text = formattedTime;
        _timerSlider.value = currentTime / startTime;
    }
}