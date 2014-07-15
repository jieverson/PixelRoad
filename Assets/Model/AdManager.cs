using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds.Api;

public static class AdManager
{

    public static event Action<bool> AdsChanged;

    private static BannerView _bannerView;

    public static void ShowAds()
    {
#if UNITY_ANDROID
        CheckBannerExists();
        _bannerView.Show();
        AdRequest request = new AdRequest.Builder().Build();
        _bannerView.LoadAd(request);
#endif

#if UNITY_WP8
        if (AdsChanged != null) AdsChanged.Invoke(true);
#endif
    }

    public static void HideAds()
    {
#if UNITY_ANDROID
        CheckBannerExists();
        _bannerView.Hide();
#endif

#if UNITY_WP8
        if (AdsChanged != null) AdsChanged.Invoke(false);
#endif
    }

    private static void CheckBannerExists()
    {
        if (_bannerView == null)
        {
            _bannerView = new BannerView(
                    "ca-app-pub-9013851829730737/3873391606", AdSize.SmartBanner, AdPosition.Bottom);
        }
    }

}