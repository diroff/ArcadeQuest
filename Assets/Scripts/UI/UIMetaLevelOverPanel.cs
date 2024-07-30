using UnityEngine;

public class UIMetaLevelOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject _levelOverPanel;
    [SerializeField] private MetaLevel _meta;

    [SerializeField] private UIMetaLevelOver _levelOver;

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
        _levelOver.gameObject.SetActive(false);
    }
}