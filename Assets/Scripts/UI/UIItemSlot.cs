using System.Collections;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour //// Combine with UIMetaItemSlot!
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemCountText;

    private UIItemList _itemList;

    private Canvas _mainCanvas;

    private int _itemCount;
    private int _itemID;

    private int _maxItemCount;

    public int ItemID => _itemID;

    private void Awake()
    {
        _mainCanvas = GetComponentInParent<Canvas>();
        _itemList = GetComponentInParent<UIItemList>();
    }

    public void SetItemID(int id)
    {
        _itemID = id;
    }

    public void SetIcon(Sprite sprite)
    {
        _itemIcon.sprite = sprite;
    }

    public void SetItemCount(int count)
    {
        if (count < 0)
            return;

        _itemCount = count;
        SetCurrentItemCountText();
    }

    public void SetMaxItemCount(int count)
    {
        _maxItemCount = count;
    }

    public void CompleteIcon()
    {
        StartCoroutine(CollectItemAnimation());
    }

    public void RemoveItems(int count)
    {
        _itemCount -= count;

        SetCurrentItemCountText();

        if (_itemCount <= 0)
            Destroy(gameObject);
    }

    private void SetCurrentItemCountText()
    {
        _itemCountText.text = _itemCount + "/" + _maxItemCount;
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