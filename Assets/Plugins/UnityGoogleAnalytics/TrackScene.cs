using UnityEngine;
using UnityGoogleAnalytics.Core;

namespace UnityGoogleAnalytics
{
    public class TrackScene : MonoBehaviour
    {

        public string sceneName;

        void Start()
        {
            GoogleAnalyticsDefaultSettings.Check();

            TrackService.PageView(sceneName);
        }
    }
}