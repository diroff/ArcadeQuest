using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollectItemAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _movePoint;
    [SerializeField] private UIItemSlot _slot;
    [SerializeField] private OverlayRenderer _overlayRenderer;

    [SerializeField] private float _animationTime;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _animationScalingFactor;

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

        _slot.transform.SetAsFirstSibling();

        Vector3 startPosition = item.transform.position;
        Vector3 startScale = item.transform.localScale;

        Vector3 endScale = startScale * _animationScalingFactor;

        _overlayRenderer.SetObjectToOverlay(item.gameObject);

        Vector3 worldPointOnUI = Vector3.zero;

        while (Time.time < startTime + _animationTime)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / _animationTime;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _movePoint.position);
            float targetZ = Camera.main.nearClipPlane + _zOffset;

            float minDistanceFromCamera = Mathf.Max(targetZ, Camera.main.nearClipPlane + 1f);
            worldPointOnUI = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, minDistanceFromCamera));

            Vector3 newPosition = Vector3.Lerp(startPosition, worldPointOnUI, smoothT);

            float scaleAdjust = Mathf.Lerp(1f, 0.5f, smoothT);
            Vector3 newScale = Vector3.Lerp(startScale, endScale, smoothT) * scaleAdjust;

            item.transform.position = newPosition;
            item.transform.localScale = newScale;

            yield return null;
        }

        item.transform.position = worldPointOnUI;
        item.transform.localScale = endScale;
        item.Destroy();
    }
}