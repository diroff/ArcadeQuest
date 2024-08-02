using System.Collections;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float _movingTime = 0.8f;

    private MetaItem _metaItem;

    private Coroutine _moveCoroutine;

    public void Initialize(MetaItem metaItem)
    {
        _metaItem = metaItem;
    }

    private IEnumerator MoveObjectCoroutine(Vector3 startPoint, Vector3 endPoint, Quaternion endRotation, float timeModificator = 0f)
    {
        float totalTime = _movingTime + timeModificator;
        float elapsedTime = 0f;

        _metaItem.SetupRigidbody();

        while (elapsedTime < totalTime && _metaItem.IsRestanding)
        {
            float t = elapsedTime / totalTime;
            Vector3 newPosition = Vector3.Lerp(startPoint, endPoint, t);
            Quaternion newRotation = Quaternion.Lerp(_metaItem.gameObject.transform.localRotation, endRotation, t);

            _metaItem.Rigidbody.MovePosition(newPosition);
            _metaItem.Rigidbody.MoveRotation(newRotation);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _metaItem.SetupRigidbody();
        _metaItem.gameObject.transform.position = endPoint;
        _metaItem.RestandPosition();
        _moveCoroutine = null;
    }

    private IEnumerator CollectAnimation()
    {
        Vector3 targetPosition = _metaItem.transform.position;
        targetPosition.y += 5f;

        StartCoroutine(MoveObjectCoroutine(transform.position, targetPosition, _metaItem.RotationOnSlot, 0.5f));

        while (_metaItem.IsRestanding)
            yield return null;

        _metaItem.Destroy();
    }

    public void PlayCollectAnimation()
    {
        _metaItem.UnrestandPosition();
        StartCoroutine(CollectAnimation());
    }

    public void PlayMoveCoroutine(Vector3 startPoint, Vector3 endPoint, Quaternion endRotation, float timeModificator = 0f)
    {
        if(_moveCoroutine != null)
        {
            _moveCoroutine = null;
            Debug.Log("Coroutine is not null");
        }

        _moveCoroutine = StartCoroutine(MoveObjectCoroutine(startPoint, endPoint, endRotation, timeModificator));
    }
}
