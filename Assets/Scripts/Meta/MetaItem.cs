using UnityEngine;
using UnityEngine.Events;

public class MetaItem : MonoBehaviour
{
    [SerializeField] private int _itemId;
    [SerializeField] private Sprite _itemIcon;
    [SerializeField] private LayerMask _collisionLayerMask;

    [Header("Animation Settings")]
    [SerializeField] private float _movingTime = 0.8f;
    [SerializeField] private float _maxAngularVelocity = 1f;

    [Header("Position in slot")]
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Quaternion _rotationOnSlot;

    private Vector3 _mousePosition;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _isOnSlot = false;
    private bool _isCollected = false;
    private bool _isRestanding = false;
    private bool _isDraging = false;
    private bool _isNeedClamp = true;
    private bool _wasAddingOnSlot = false;

    private float _startTime;

    private ItemAnimation _animation;
    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    private MetaSlotPanel _slotPanel;
    private MetaSlot _slot;

    public int ItemId => _itemId;
    public Sprite ItemIcon => _itemIcon;

    public Quaternion RotationOnSlot => _rotationOnSlot;

    public bool IsCollected => _isCollected;
    public bool IsRestanding => _isRestanding;
    public bool IsOnSlot => _isOnSlot;

    public ItemAnimation Animation => _animation;
    public Rigidbody Rigidbody => _rigidbody;
    public MeshCollider MeshCollider => _meshCollider;

    public UnityAction ItemWasCollected;
    public UnityAction ItemWasDestroyed;
    public UnityAction<int> ItemWasDestroyedWithId;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        _meshCollider = GetComponent<MeshCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _animation = GetComponent<ItemAnimation>();
        _animation.Initialize(_movingTime, this);
    }

    private void FixedUpdate()
    {
        if (_isNeedClamp)
            _rigidbody.angularVelocity = Vector3.ClampMagnitude(_rigidbody.angularVelocity, _maxAngularVelocity);
    }

    private void OnMouseUp()
    {
        if (_wasAddingOnSlot)
        {
            _wasAddingOnSlot = false;
            return;
        }

        if (!(Time.time - _startTime > 0.1f) || _isOnSlot)
        {
            Interact();
            return;
        }

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

        MoveItem();
    }

    private void MoveItem()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        mouseWorldPosition.y = transform.position.y;

        Vector3 direction = mouseWorldPosition - transform.position;
        float distance = direction.magnitude;

        if (!Physics.Raycast(transform.position, direction.normalized, distance, _collisionLayerMask))
            _rigidbody.MovePosition(transform.position + direction);
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

    public void RestandPosition()
    {
        _isRestanding = false;
    }

    public void UnrestandPosition()
    {
        _isRestanding = true;
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
        
        _isRestanding = true;
        _animation.PlayMoveCoroutine(transform.position, targetPosition, _rotationOnSlot);
    }

    public void SetSlotPanel(MetaSlotPanel panel)
    {
        _slotPanel = panel;
    }

    public void ReturnFromSlot()
    {
        _slot = null;
        _isOnSlot = false;

        _isRestanding = true;
        _animation.PlayMoveCoroutine(transform.position, _startPosition, _startRotation);
    }

    public void CollectItem()
    {
        _isCollected = true;
        ItemWasCollected?.Invoke();
        _slot.DeleteItem();
        _meshCollider.enabled = false;
        _rigidbody.isKinematic = true;
    }

    public void SetupRigidbody()
    {
        _isNeedClamp = !_isOnSlot;

        _meshCollider.isTrigger = _isOnSlot;
        _rigidbody.isKinematic = _isOnSlot;
    }

    public void DestroyItem()
    {
        ItemWasDestroyed?.Invoke();
        ItemWasDestroyedWithId?.Invoke(_itemId);
        Destroy(gameObject);
    }
}