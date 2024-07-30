using System.Collections;
using UnityEngine;

public class UIMainItemSlot : UIItemSlot
{
    private UIMainItemList _itemList;

    private Canvas _mainCanvas;

    private void Awake()
    {
        _mainCanvas = GetComponentInParent<Canvas>();
        _itemList = GetComponentInParent<UIMainItemList>();
    }

    public void CompleteIcon()
    {
        StartCoroutine(CollectItemAnimation());
    }

    protected override void SetCurrentItemCountText()
    {
        ItemCountText.text = ItemCount + "/" + MaxItemCount;
    }

    private IEnumerator CollectItemAnimation()
    {
        transform.SetAsFirstSibling();
        //_itemList.LayoutGroup.enabled = false;
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

        //_itemList.LayoutGroup.enabled = true;

        Destroy(gameObject);
    }
}