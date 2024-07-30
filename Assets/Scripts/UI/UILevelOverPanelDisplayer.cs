using UnityEngine;

public class UILevelOverPanelDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _levelOverPanel;
    [SerializeField] private Level _levelItems;

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