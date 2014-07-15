using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class Score : MonoBehaviour {

    public float timeToScore;

    public static int points;

    private float _timer;

	void Start () {
        points = 0;

        #if UNITY_ANDROID
            // Android AdMob
            BannerView bannerView = new BannerView(
                    "ca-app-pub-9013851829730737/3873391606", AdSize.Banner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
        #endif
    }
	
	void Update () {
        _timer += Time.deltaTime;
        if (_timer >= timeToScore)
        {
            _timer -= timeToScore;
            points += 1;
        }

        var gui = this.gameObject.GetComponent<GUIText>();
        gui.text = points.ToString();
	}
}
