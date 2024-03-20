using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class MetaItem : MonoBehaviour
{
    [SerializeField] private int _itemId;
    [SerializeField] private Sprite _itemIcon;

    [SerializeField] private float _movingTime = 0.8f;

    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _maxAngularVelocity = 1f;

    private Vector3 _mousePosition;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _isOnSlot = false;
    private bool _isCollected = false;
    private bool _isRestanding = false;
    private bool _isDraging = false;
    private bool _isNeedClamp = true;

    private float _startTime;

    private Rigidbody _rigidbody;

    private MetaSlotPanel _slotPanel;
    private MetaSlot _slot;

    private BoxCollider _boxCollider;

    public int ItemId => _itemId;
    public Sprite ItemIcon => _itemIcon;

    public MetaSlot MetaSlot => _slot;

    public bool IsCollected => _isCollected;
    public bool IsRestanding => _isRestanding;

    public UnityAction ItemWasCollected;
    public UnityAction ItemWasDestroyed;
    public UnityAction<int> ItemWasDestroyedWithId;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_isNeedClamp)
            _rigidbody.angularVelocity = Vector3.ClampMagnitude(_rigidbody.angularVelocity, _maxAngularVelocity);
    }

    private void OnMouseUp()
    {
        if (!(Time.time - _startTime > 0.1f) || _isOnSlot)
            Interact();

        _isDraging = false;
    }

    private void OnMouseDown()
    {
        _mousePosition = Input.mousePosition - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        if (_isOnSlot)
            return;

        if (!_isDraging)
        {
            _startTime = Time.time;
            _isDraging = true;
        }

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        targetPos.y = transform.position.y;

        Vector3 direction = targetPos - transform.position;
        _rigidbody.velocity = direction / Time.fixedDeltaTime;
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

    private Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
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
        _rigidbody.isKinematic = true;
    }

    private IEnumerator MoveObjectCoroutine(Vector3 startPoint, Vector3 endPoint, float timeModificator = 0f)
    {
        float totalTime = _movingTime + timeModificator;
        _isRestanding = true;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime && _isRestanding)
        {
            _isNeedClamp = !_isOnSlot;
            _boxCollider.isTrigger = _isOnSlot;
            _rigidbody.isKinematic = _isOnSlot;

            float t = elapsedTime / totalTime;
            Vector3 newPosition = Vector3.Lerp(startPoint, endPoint, t);

            _rigidbody.MovePosition(newPosition);

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
        ItemWasDestroyedWithId?.Invoke(_itemId);
        Destroy(gameObject);
    }

    public void PlayCollectAnimation()
    {
        StartCoroutine(CollectAnimation());
    }
}