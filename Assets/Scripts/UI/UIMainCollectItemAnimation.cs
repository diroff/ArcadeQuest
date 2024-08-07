using System.Collections;
using UnityEngine;

public class UIMainCollectItemAnimation : UICollectItemAnimation
{
    [SerializeField] private float _scalingAnimationTime = 1f;
    [SerializeField] private float _scaleFactor = 0.5f;

    protected override IEnumerator CollectAnimation(Item item)
    {
        var itemAnimation = item.GetComponent<AnimationScript>();
        var itemParticle = item.GetComponentInChildren<ParticleSystem>();
        var meshRenderer = item.GetComponent<MeshRenderer>();

        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Destroy(itemAnimation);
        Destroy(itemParticle.gameObject);

        float startTime = Time.time;

        Vector3 startPosition = item.transform.position;
        Vector3 startScale = item.transform.localScale;

        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + 1, startPosition.z);
        Vector3 endScale = new Vector3(startScale.x * _scaleFactor, startScale.y * _scaleFactor, startScale.z * _scaleFactor);

        while (Time.time < startTime + _scalingAnimationTime)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / _scalingAnimationTime;
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, smoothT);
            Vector3 newScale = Vector3.Lerp(startScale, endScale, t);

            item.transform.position = newPosition;
            item.transform.localScale = newScale;

            yield return null;
        }

        yield return base.CollectAnimation(item);
    }
}