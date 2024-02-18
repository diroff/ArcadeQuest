using UnityEngine;

public class UIMetaLevelOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject _levelOverPanel;
    [SerializeField] private Meta _meta;

    private void Start()
    {
        _meta.LevelWasCompleted += DisplayPanel;
    }

    private void OnDisable()
    {
        _meta.LevelWasCompleted -= DisplayPanel;
    }

    private void DisplayPanel()
    {
        _levelOverPanel.SetActive(true);
    }
}