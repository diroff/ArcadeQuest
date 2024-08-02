using System.Collections;
using UnityEngine;

public class UICollectItemAnimation : MonoBehaviour
{
    [SerializeField] private UIItemSlot _slot;

    [SerializeField] private float _animationTime;
    [SerializeField] private float _moveDistance;

    private Item _currentItem;

    private void OnEnable()
    {
        _slot.ItemWasCollected += OnItemListChanged;
    }

    private void OnDisable()
    {
        _slot.ItemWasCollected -= OnItemListChanged;
    }

    public void OnItemListChanged()
    {
        _currentItem = _slot.LastCollectedItem;

        StartCoroutine(CollectAnimation());
    }

    private IEnumerator CollectAnimation()
    {
        float startTime = Time.time;

        Vector3 itemPosition = _currentItem.transform.position;

        Vector3 start = itemPosition;
        Vector3 end = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z - _moveDistance);

        while (Time.time < startTime + _animationTime)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / _animationTime;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            Vector3 newPosition = Vector3.Lerp(start, end, smoothT);

            _currentItem.transform.position = newPosition;

            yield return null;
        }

        _currentItem.transform.position = end;
        _currentItem.Destroy();
    }
}