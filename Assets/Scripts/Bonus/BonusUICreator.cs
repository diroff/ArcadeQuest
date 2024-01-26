using UnityEngine;

public class BonusUICreator : MonoBehaviour
{
    [SerializeField] private Bonus _bonus;
    [SerializeField] private UIBonusIndicator _bonusIndicatorPrefab;
    [SerializeField] private GameObject _uiPlacement;

    private UIBonusIndicator _indicator;

    private void OnEnable()
    {
        _bonus.BonusStarted.AddListener(CreateBonusUI);
        _bonus.BonusEnded.AddListener(() => Destroy(_indicator.gameObject));
        _bonus.BonusTimeChanged.AddListener(OnBonusTimeChanged);
    }

    private void OnDisable()
    {
        _bonus.BonusStarted.RemoveListener(CreateBonusUI);
        _bonus.BonusEnded.RemoveListener(() => Destroy(_indicator.gameObject));
        _bonus.BonusTimeChanged.RemoveListener(OnBonusTimeChanged);
    }

    private void CreateBonusUI()
    {
        _indicator = Instantiate(_bonusIndicatorPrefab, _uiPlacement.transform);
        _indicator.SetIcon(_bonus.BonusIcon);
    }

    private void OnBonusTimeChanged(float timeLeft, float bonusTime)
    {
        _indicator.SetIndicatorValue(timeLeft, bonusTime);
    }
}