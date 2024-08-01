using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemAnimation : MonoBehaviour
{
    [SerializeField] private UIItemList _itemList;
    [SerializeField] private Canvas _mainCanvas;

    [Header("Animation Settings")]
    [SerializeField] private Vector3 _scaleFactor;
    [Min(0.01f)] [SerializeField] private float _animationTime;

    private List<UIItemSlot> _itemSlots;

    private void OnEnable()
    {
        _itemList.ItemListUIWasCreated += OnItemListWasCreated;

        if (_itemSlots != null)
            SubscribeOnItemList();
    }

    private void OnDisable()
    {
        _itemList.ItemListUIWasCreated -= OnItemListWasCreated;

        if (_itemSlots == null)
            return;

        foreach (var item in _itemSlots)
        {
            item.UIItemWasRemoved -= OnUIItemWasRemoved;
        }
    }

    private void OnItemListWasCreated(List<UIItemSlot> itemList)
    {
        _itemSlots = new List<UIItemSlot>(itemList);

        SubscribeOnItemList();
    }

    private void OnUIItemWasRemoved(UIItemSlot slot)
    {
        StartCoroutine(CollectItemAnimation(slot));
    }

    private void SubscribeOnItemList()
    {
        foreach (var item in _itemSlots)
        {
            item.UIItemWasRemoved += OnUIItemWasRemoved;
        }
    }

    private IEnumerator CollectItemAnimation(UIItemSlot slot)
    {
        slot.transform.SetAsFirstSibling();
        yield return new WaitForEndOfFrame();
        _itemList.GridLayout.enabled = false;
        slot.transform.SetParent(_mainCanvas.transform);

        yield return new WaitForEndOfFrame();

        Vector3 startScale = slot.transform.localScale;
        Vector3 finalScale = slot.transform.localScale;

        finalScale += _scaleFactor;

        float elapsedTime = 0f;

        while (elapsedTime < _animationTime)
        {
            float t = elapsedTime / _animationTime;
            slot.transform.localScale = Vector3.Lerp(startScale, finalScale, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _itemList.GridLayout.enabled = true;

        Destroy(slot.gameObject);
    }
}