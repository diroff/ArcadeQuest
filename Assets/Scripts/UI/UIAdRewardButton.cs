using UnityEngine;

public class UIAdRewardButton : MonoBehaviour
{
    [SerializeField] private LevelItems _levelItems;

    private void OnEnable()
    {
        _levelItems.AllItemsCollected += DisableSelf;
    }

    private void OnDisable()
    {
        _levelItems.AllItemsCollected -= DisableSelf;
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
