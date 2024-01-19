using TMPro;
using UnityEngine;

public class UIAdMobErrorLog : MonoBehaviour
{
    [SerializeField] private MobAds _mobAds;
    [SerializeField] private GameObject _logPanel;
    [SerializeField] private TextMeshProUGUI _logText;

    private void OnEnable()
    {
        _mobAds.AdLoadingError += ShowErrorMesage;
        _mobAds.AdShowingError += ShowErrorMesage;
    }

    private void OnDisable()
    {
        _mobAds.AdLoadingError -= ShowErrorMesage;
        _mobAds.AdShowingError -= ShowErrorMesage;
    }

    private void ShowErrorMesage(string message)
    {
        _logPanel.SetActive(true);
        _logText.text = message;
    }
}