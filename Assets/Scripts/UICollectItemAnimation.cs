using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollectItemAnimation : MonoBehaviour
{
    [SerializeField] private UIItemSlot _slot;

    [SerializeField] private float _animationTime;
    [SerializeField] private float _moveDistance;

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

    private IEnumerator CollectAnimation(Item item)
    {
        float startTime = Time.time;

        Vector3 itemPosition = item.transform.position;

        Vector3 start = itemPosition;
        Vector3 end = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z - _moveDistance);

        while (Time.time < startTime + _animationTime)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / _animationTime;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            Vector3 newPosition = Vector3.Lerp(start, end, smoothT);

            item.transform.position = newPosition;

            yield return null;
        }

        item.transform.position = end;
        item.Destroy();
    }
}