using UnityEngine;

public class UILevelOverPanelDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _levelOverPanel;
    [SerializeField] private Level _levelItems;

    private void Start()
    {
        _levelItems.LevelWasCompleted += DisplayPanel;
    }

    private void OnDisable()
    {
        _levelItems.LevelWasCompleted -= DisplayPanel;
    }

    private void DisplayPanel()
    {
        _levelOverPanel.SetActive(true);
    }
}