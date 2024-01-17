using UnityEngine;
using GoogleMobileAds.Api;

public class MobAdsBanner : MonoBehaviour
{
    private const string _adUnitId = "ca-app-pub-3940256099942544/6300978111";

    private BannerView _bannerView;

    private void OnEnable()
    {
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest adRequest = new AdRequest();
        _bannerView.LoadAd(adRequest);
    }
}