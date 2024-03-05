using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class MetaItem : MonoBehaviour
{
    [SerializeField] private int _itemId;

    [SerializeField] private float _movingTime = 0.8f;

    [SerializeField] private Vector3 _positionOffset;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _isOnSlot = false;
    private bool _isCollected = false;
    private bool _isRestanding = false;

    private MetaSlotPanel _slotPanel;
    private MetaSlot _slot;

    private BoxCollider _boxCollider;

    public int ItemId => _itemId;

    public MetaSlot MetaSlot => _slot;

    public bool IsCollected => _isCollected;
    public bool IsRestanding => _isRestanding;

    public UnityAction ItemWasCollected;
    public UnityAction ItemWasDestroyed;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        Interact();
    }

    public void Interact()
    {
        if (_isRestanding)
        {
            RestandPosition();
            return;
        }

        if (_isOnSlot)
            _slot.DeleteItem();
        else
            SetToSlot();
    }

    private void RestandPosition()
    {
        _isRestanding = false;
    }

    private void SetToSlot()
    {
        _slotPanel.AddItem(this);
    }

    public void AddToSlot(MetaSlot slot)
    {
        _slot = slot;
        _isOnSlot = true;

        Vector3 targetPosition = _slot.GetPlacementPosition();
        targetPosition += _positionOffset;
        StartCoroutine(MoveObjectCoroutine(transform.position, targetPosition));
    }

    public void SetSlotPanel(MetaSlotPanel panel)
    {
        _slotPanel = panel;
    }

    public void ReturnFromSlot()
    {
        _slot = null;
        _isOnSlot = false;

        StartCoroutine(MoveObjectCoroutine(transform.position, _startPosition));
    }

    public void CollectItem()
    {
        _isCollected = true;
        ItemWasCollected?.Invoke();
        _slot.DeleteItem();
        _boxCollider.enabled = false;
    }

    private IEnumerator MoveObjectCoroutine(Vector3 startPoint, Vector3 endPoint, float timeModificator = 0f)
    {
        float totalTime = _movingTime + timeModificator;
        _isRestanding = true;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime && _isRestanding)
        {
            float t = elapsedTime / totalTime;
            transform.position = Vector3.Lerp(startPoint, endPoint, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = endPoint;
        _isRestanding = false;
    }

    private IEnumerator CollectAnimation()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.z += 5f;

        StartCoroutine(MoveObjectCoroutine(transform.position, targetPosition, 0.5f));

        while (_isRestanding)
            yield return null;

        ItemWasDestroyed?.Invoke();
        Destroy(gameObject);
    }

    public void PlayCollectAnimation()
    {
        StartCoroutine(CollectAnimation());
    }
}