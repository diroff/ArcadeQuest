using GoogleMobileAds.Api;
using UnityEngine;

public class MobAdsInitialize : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
}