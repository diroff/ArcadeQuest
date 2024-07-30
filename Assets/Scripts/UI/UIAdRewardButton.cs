using UnityEngine;

public class UIAdRewardButton : MonoBehaviour
{
    [SerializeField] private MainLevel _levelItems;
    [SerializeField] private MobAds _adMob;
    [SerializeField] private GameObject _button;

    private void OnEnable()
    {
        _levelItems.AllItemsCollected += DisableButton;
    }

    private void OnDisable()
    {
        _levelItems.AllItemsCollected -= DisableButton;
    }

    private void DisableButton()
    {
        _button.SetActive(false);
    }

    private void EnableButton()
    {
        _button.SetActive(true);
    }
}
