using UnityEngine;
using GoogleMobileAds.Api;

public class MobAdsRewarded : MonoBehaviour
{
    [SerializeField] private LevelItems _levelItems;

    private const string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
    
    private RewardedAd _rewardedAd;

    private void Start()
    {
        LoadRewardedAd();
    }

    [ContextMenu("Show ad")]
    public void ShowRewardedAd()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) => ShowHint());
        }
    }

    public void ShowHint() 
    {
        _levelItems.ShowItem();
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        var adRequest = new AdRequest();

        RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }
}
