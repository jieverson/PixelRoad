using UnityEngine;
using UnityGoogleAnalytics.Core;

public class TrackScene : MonoBehaviour
{

    public string sceneName;

    void Start()
    {
        GoogleAnalyticsDefaultSettings.Configure();

        string url = Parser.PageView(sceneName);
        WWW www = new WWW(url);
        while (www.isDone == false) { }
    }
}