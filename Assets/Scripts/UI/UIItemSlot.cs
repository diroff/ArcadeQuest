using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour //// Combine with UIMetaItemSlot!
{
    [SerializeField] private Image _itemIcon;

    private Item _item;
    private Canvas _mainCanvas;
    private UIItemList _itemList;

    private void Awake()
    {
        _mainCanvas = GetComponentInParent<Canvas>();
        _itemList = GetComponentInParent<UIItemList>();
    }

    private void OnDisable()
    {
        _item.ItemWasCollected -= CompleteIcon;
    }

    public void SetItem(Item item)
    {
        _item = item;
        _itemIcon.sprite = _item.Icon;
        _item.ItemWasCollected += CompleteIcon;
    }

    private void CompleteIcon()
    {
        StartCoroutine(CollectItemAnimation());
    }

    private IEnumerator CollectItemAnimation()
    {
        transform.SetAsFirstSibling();
        _itemList.LayoutGroup.enabled = false;
        transform.SetParent(_mainCanvas.transform);

        yield return new WaitForEndOfFrame();

        Vector3 startScale = transform.localScale;
        Vector3 finalScale = transform.localScale;

        finalScale.x += 0.3f;
        finalScale.y += 0.3f;
        finalScale.z += 0.3f;

        float totalTime = 0.3f;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            float t = elapsedTime / totalTime;
            transform.localScale = Vector3.Lerp(startScale, finalScale, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _itemList.LayoutGroup.enabled = true;

        Destroy(gameObject);
    }
}