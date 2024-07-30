using System.Collections;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private MainLevel _levelItems;
    [SerializeField] private MobAds _mobAds;

    private void OnEnable()
    {
        _mobAds.AdShowing += ShowItem;
    }

    private void OnDisable()
    {
        _mobAds.AdShowing -= ShowItem;
    }

    private void ShowItem()
    {
        Debug.Log("Hint!");

        var itemToShow = _levelItems.GetNotCollectedItem();

        if (itemToShow == null)
            return;

        StartCoroutine(ShowItemCoroutine(itemToShow.transform));
    }

    private IEnumerator ShowItemCoroutine(Transform item)
    {
        _cameraMovement.ChangePositionSmoothing(0.1f);
        _cameraMovement.ChangeTarget(item);
        yield return new WaitForSeconds(4f);
        _cameraMovement.UndoChangingTarget();
        _cameraMovement.UndoChangingPositionSmoothing();
    }
}