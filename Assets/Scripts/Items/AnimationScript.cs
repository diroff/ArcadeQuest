using System.Collections;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private bool _isRotating = true;
    [SerializeField] private bool _isScaling = true;

    [SerializeField] private float _rotatingSpeed = 75f;
    [SerializeField] private float _scalingSpeed = 0.5f;
    [SerializeField] private float _maxScaleMultiplier = 1.5f;

    private Vector3 _baseScale;
    private float _scalingTime;

    private void OnDestroy()
    {
        if (_isRotating)
            StopCoroutine(RotateObject());

        if (_isScaling)
            StopCoroutine(ScalingObject());
    }

    private void Start()
    {
        _baseScale = transform.localScale;
        _scalingTime = 0f;

        if (_isRotating)
            StartCoroutine(RotateObject());

        if (_isScaling)
            StartCoroutine(ScalingObject());
    }

    private IEnumerator RotateObject()
    {
        while (_isRotating)
        {
            RotateObjectOnceTime();
            yield return null;
        }
    }

    private IEnumerator ScalingObject()
    {
        while (_isScaling)
        {
            ScaleObjectOnceTime();
            yield return null;
        }
    }

    private void RotateObjectOnceTime()
    {
        transform.Rotate(Vector3.up, _rotatingSpeed * Time.deltaTime, Space.Self);
    }

    private void ScaleObjectOnceTime()
    {
        _scalingTime += Time.deltaTime;
        float scale = Mathf.PingPong(_scalingTime * _scalingSpeed, _maxScaleMultiplier - 1f) + 1f;
        transform.localScale = _baseScale * scale;
    }
}