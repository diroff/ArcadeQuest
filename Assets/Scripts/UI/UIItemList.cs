using UnityEngine;

public class UIItemList : MonoBehaviour
{
    [SerializeField] private UIItemSlot _slotPrefab;
    [SerializeField] private LevelItems _levelItems;

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        foreach (var item in _levelItems.Items)
        {
            var slot = Instantiate(_slotPrefab, transform);
            slot.SetItem(item);
        }
    }
}