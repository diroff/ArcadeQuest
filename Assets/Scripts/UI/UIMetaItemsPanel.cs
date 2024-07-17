using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMetaItemsPanel : MonoBehaviour
{
    [SerializeField] private int _maxItemsCount = 6;

    [SerializeField] private RectTransform _transform;
    [SerializeField] private ContentSizeFitter _sizeFilter;

    private int _slotsCount;

    private void OnTransformChildrenChanged()
    {
        AlignPanel();
    }

    private void AlignPanel()
    {
        _slotsCount = GetComponentsInChildren<UIMetaItemSlot>().Length;

        Debug.Log("Slots count:" + _slotsCount);

        if (_slotsCount <= _maxItemsCount)
            CenteringPanel();
        else
            UncenteringPanel();
    }

    [ContextMenu("Center")]
    public void CenteringPanel()
    {
        Debug.Log("Centering");
        _sizeFilter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        _transform.offsetMin = new Vector2(0, _transform.offsetMin.y);
        _transform.offsetMax = new Vector2(0, _transform.offsetMax.y);
    }

    [ContextMenu("UnCenter")]
    public void UncenteringPanel()
    {
        Debug.Log("Uncentering");
        _sizeFilter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
}
