using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollectItemAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _movePoint;
    [SerializeField] private UIItemSlot _slot;

    [SerializeField] private float _animationTime;
    [SerializeField] private float _zOffset;

    private Queue<Item> _currentItems = new Queue<Item>();

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
        _currentItems.Enqueue(_slot.LastCollectedItem);

        if (_currentItems.Count == 0)
            return;

        var item = _currentItems.Dequeue();

        if (item == null || item.IsImmediatelyCollected)
            return;

        StartCoroutine(CollectAnimation(item));
    }

    protected virtual IEnumerator CollectAnimation(Item item)
    {
        float startTime = Time.time;

        Vector3 startPosition = item.transform.position;

        while (Time.time < startTime + _animationTime)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / _animationTime;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(null, _movePoint.position);
            Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, Camera.main.nearClipPlane + _zOffset));

            Vector3 newPosition = Vector3.Lerp(startPosition, end, smoothT);
            item.transform.position = newPosition;

            yield return null;
        }

        Vector3 finalScreenPoint = RectTransformUtility.WorldToScreenPoint(null, _movePoint.position);
        Vector3 finalEnd = Camera.main.ScreenToWorldPoint(new Vector3(finalScreenPoint.x, finalScreenPoint.y, Camera.main.nearClipPlane + _zOffset));

        item.transform.position = finalEnd;
        item.Destroy();
    }
}