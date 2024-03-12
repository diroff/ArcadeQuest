using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMetaLevelOver : MonoBehaviour
{
    [SerializeField] private MetaTimer _metaTimer;
    [SerializeField] private GameObject _gameoverPanel;
    [SerializeField] private GameObject _blockCollider;

    private void OnEnable()
    {
        _metaTimer.TimeWasOver += ShowPanel;
    }

    private void OnDisable()
    {
        _metaTimer.TimeWasOver -= ShowPanel;
    }

    private void ShowPanel()
    {
        _gameoverPanel.SetActive(true);
        _blockCollider.SetActive(true);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}