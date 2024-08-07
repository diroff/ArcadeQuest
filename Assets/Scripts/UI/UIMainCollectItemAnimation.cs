using System.Collections;
using UnityEngine;

public class UIMainCollectItemAnimation : UICollectItemAnimation
{
    protected override IEnumerator CollectAnimation(Item item)
    {
        var itemAnimation = item.GetComponent<AnimationScript>();
        var itemParticle = item.GetComponentInChildren<ParticleSystem>();
        var meshRenderer = item.GetComponent<MeshRenderer>();

        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Destroy(itemAnimation);
        Destroy(itemParticle.gameObject);

        return base.CollectAnimation(item);
    }
}