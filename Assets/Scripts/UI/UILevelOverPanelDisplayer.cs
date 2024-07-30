using UnityEngine;

public class UILevelOverPanelDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _levelOverPanel;
    [SerializeField] private MainLevel _levelItems;

    private void Start()
    {
        _levelItems.AllItemsCollected += DisplayPanel;
    }

    private void OnDisable()
    {
        _levelItems.AllItemsCollected -= DisplayPanel;
    }

    private void DisplayPanel()
    {
        _levelOverPanel.SetActive(true);
    }
}