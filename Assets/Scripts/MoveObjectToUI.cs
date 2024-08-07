using System.Collections;
using UnityEngine;

public class MoveObjectToUI : MonoBehaviour
{
    [SerializeField] private GameObject _3d;
    [SerializeField] private GameObject _ui;

    private Vector3 _defaultPosition;
    private Vector3 _defaultScale;

    private Transform _defaultTransform;

    private void Start()
    {
        _defaultPosition = _3d.transform.position;
        _defaultScale = _3d.transform.localScale;

        _defaultTransform = _3d.transform.parent;
    }

    [ContextMenu("Move into ui")]
    public void MoveObject()
    {
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float startTime = Time.time;

        _3d.transform.SetParent(_ui.transform);

        Vector3 startPosition = _3d.transform.localPosition;
        Vector3 startScale = _3d.transform.localScale;

        Vector3 endPosition = new Vector3(0, 0, -1);
        Vector3 endScale = new Vector3(10, 10, 10);

        float animationTime = 2f;

        while (Time.time < startTime + animationTime)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / animationTime;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, smoothT);
            Vector3 newScale = Vector3.Lerp(startScale, endScale, smoothT);

            _3d.transform.localPosition = newPosition;
            _3d.transform.localScale = newScale;

            yield return null;
        }

        _3d.transform.localPosition = endPosition;
        _3d.transform.localScale = endScale;
    }

    [ContextMenu("Move into start")]
    public void SetDefaultPosition()
    {
        _3d.transform.SetParent(_defaultTransform);
        _3d.transform.position = _defaultPosition;
        _3d.transform.localScale = _defaultScale;
    }
}
